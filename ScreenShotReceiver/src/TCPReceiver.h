#ifndef _TCPRECEIVER_H_
#define _TCPRECEIVER_H_

#pragma GCC optimize ("O3")

//#include <vector>
//#include <algorithm>

#include <WiFi.h>
#include <WiFiServer.h>
#include <driver/spi_master.h>
#include "tjpgdClass.h"
#include "DMADrawer.h"

#undef min

class TCPReceiver
{
public:
  bool setup(uint16_t offset_x, uint16_t offset_y, uint16_t width, uint16_t height, uint32_t spi_freq, int spi_mosi, int spi_miso, int spi_sclk, int tft_cs, int tft_dc)
  {
    Serial.println("TCPReceiver setup.");

    _tft_offset_x = offset_x;
    _tft_offset_y = offset_y;
    _tft_width = width;
    _tft_height = height;

    _tcp.setNoDelay(true);
    _tcp.begin(63333);

    DMADrawer::setup(DMA_BUF_LEN, spi_freq, spi_mosi, spi_miso, spi_sclk, tft_cs, tft_dc);

    _jdec.multitask_begin();

    Serial.println("TCPReceiver setup done.");
    return true;
  }

  void close()
  {
    _client = _tcp.available();
    if (_client.connected()) _client.stop();
    _jdec.multitask_end();
    delay(100);
    _tcp.stop();
    DMADrawer::close();
  }

  bool loop()
  {
    static uint16_t waitCount = 0;

    if (SENDER_PREFIX_SIZE <= _client.available()
     && SENDER_PREFIX_SIZE == _client.read(_tcpBuf, SENDER_PREFIX_SIZE)) {
      waitCount = 0;
      if (_tcpBuf[0] == 'J'
       && _tcpBuf[1] == 'P'
       && _tcpBuf[2] == 'G') {
        _recv_remain = *(int32_t*)&_tcpBuf[3];
        if (_recv_remain > 600) {
          if (drawJpg()) {
            ++_drawCount;
          }
        } else {
          Serial.println("jpg too short");
        }
        if (0 < _recv_remain) {
          Serial.printf("clear remain data:%d\r\n", _recv_remain);
          int r;
          for (uint16_t retry = 1000; retry; --retry) {
            r = _client.read(_tcpBuf, _recv_remain < TCP_BUF_LEN ? _recv_remain : TCP_BUF_LEN);
            if (r > 0) {
              _recv_remain -= r;
              if (!_recv_remain) break;
            } else {
              delay(1);
              ++_delayCount;
            }
          }
        }
      } else {
        Serial.println("broken data");
        delay(10);
        while (0 < _client.read(_tcpBuf, TCP_BUF_LEN)) delay(10);
      }
    } else
    if (!_client.connected()) {
      _client = _tcp.available();
      _recv_requested = false;
      waitCount = 0;
    } else {
      if (++waitCount > 1000) {
        _recv_requested = false;
      } else {
        delay(1);
        ++_delayCount;
      }
      if (!_recv_requested)   {
        while (0 < _client.read(_tcpBuf, TCP_BUF_LEN));
        _recv_requested = true;
        waitCount = 0;
        Serial.println("data request");
        _client.print("JPG\n");
      }
    }

    if (_sec != millis() / 1000) {
      _sec = millis() / 1000;
      Serial.printf("%2d fps", _drawCount);
      if (_delayCount) Serial.printf(" / delay%3d", _delayCount);
      Serial.print("\r\n");
      _drawCount = 0;
      _delayCount = 0;
    }

    return true;
  }
private:
  enum
  { DMA_BUF_LEN = 320 * 48 * 2 // 320x48 16bit color
  , TCP_BUF_LEN = 512
  , SENDER_PREFIX_SIZE = 7
  };

  WiFiServer _tcp;
  WiFiClient _client;
  TJpgD _jdec;
  int32_t _recv_remain = 0;
  uint32_t _sec = 0;
  uint16_t _tft_offset_x;
  uint16_t _tft_offset_y;
  uint16_t _tft_width;
  uint16_t _tft_height;
  int16_t _jpg_x;
  int16_t _jpg_y;
  uint16_t _drawCount = 0;
  uint16_t _delayCount = 0;
  uint8_t _lineskip = 2;
  uint8_t _tcpBuf[TCP_BUF_LEN];
  bool _recv_requested = false;

  static uint16_t jpgRead(TJpgD *jdec, uint8_t *buf, uint16_t len) {
    TCPReceiver* me = (TCPReceiver*)jdec->device;
    WiFiClient* client = &me->_client;
    uint16_t retry;
    if (!client->connected()) {
      Serial.println("jpgRead fail: disconnected.");
      return 0;
    }

    if (len == TJPGD_SZBUF) {
      if (me->_recv_requested) {
        me->_recv_requested = false;
      } else
      if (me->_recv_remain < TJPGD_SZBUF*2 && TJPGD_SZBUF < me->_recv_remain) { // dataend read tweak
        len = me->_recv_remain - len;
      }
    } else if (client->available() < len) {
      for (retry = 1000; client->available() < len && retry; --retry) {
        delay(1);
        ++me->_delayCount;
      }
    }

    int l = client->read(buf ? buf : me->_tcpBuf, len);
    if (l <= 0) {
      for (retry = 1000; retry; --retry) {
        delay(1);
        ++me->_delayCount;
        l = client->read(buf ? buf : me->_tcpBuf, len);
        if (l > 0) break;
      }
    }
    if (l <= 0) {
      Serial.printf("jpgRead error:%d:%d:%d\r\n", l, len, client->available());
      return 0;
    }
    me->_recv_remain -= l;
    if (!me->_recv_requested && me->_recv_remain < 3) {
      if (me->_recv_remain >= client->available()) {
        client->print("JPG\n"); // request the next image from the client
        me->_recv_requested = true;
      } else {
        Serial.println("excessive request");
        me->_recv_requested = true;
      }
    }
    return l;
  }

  static uint16_t jpgWrite(TJpgD *jdec, void *bitmap, JRECT *rect) {
    TCPReceiver* me = (TCPReceiver*)jdec->device;
    uint16_t *dst = DMADrawer::getNextBuffer();
    uint16_t *data = (uint16_t*)bitmap;
    uint16_t width = std::min(jdec->width, me->_tft_width);
    int16_t x = rect->left;
    int16_t y = rect->top;
    uint8_t w = rect->right + 1 - x;
    uint8_t h = rect->bottom + 1 - y;
    uint8_t line;

    dst += x + width * (y % ((1 + me->_lineskip) * jdec->msy << 3));
    while (h--) {
      line = w;
      while (line--) {
        *dst++ = *data++;
      }
      dst += width - w;
    }

    return 1;
  }

  static uint16_t jpgWriteRow(TJpgD *jdec, uint16_t y, uint8_t h) {
    TCPReceiver* me = (TCPReceiver*)jdec->device;
    DMADrawer::draw( me->_tft_offset_x + (0 > me->_jpg_x ? 0 : me->_jpg_x)
                   , me->_tft_offset_y +  me->_jpg_y + y
                   , std::min(jdec->width, me->_tft_width)
                   , h
                   );
    return 1;
  }


//  uint32_t _micros0 = 0;
//  std::vector<uint32_t> micros1, micros2, micros3;

  bool drawJpg() {
//    uint32_t ms1 = micros();
    JRESULT jres = _jdec.prepare(jpgRead, this);
    if (jres != JDR_OK) {
      Serial.printf("prepare failed! %d\r\n", jres);
      return false;
    }
//    uint32_t ms2 = micros();
    _jpg_x = (_tft_width - _jdec.width) / 2;
    _jpg_y = (_tft_height- _jdec.height) / 2;

//  jres = _jdec.decomp(jpgWrite, jpgWriteRow, _lineskip);
    jres = _jdec.decomp_multitask(jpgWrite, jpgWriteRow, _lineskip);
//  uint32_t ms3 = micros();
    if (jres != JDR_OK) {
      Serial.printf("decomp failed! %d\r\n", jres);
      return false;
    }
/*
    if (true) {
    //Serial.printf("micros: %d:%d:%d\r\n", ms2 - ms1, ms3 - ms2, ms1 - _ms);
      micros1.push_back(ms2 - ms1);
      micros2.push_back(ms3 - ms2);
      micros3.push_back(ms1 - _micros0);
      if (micros1.size() == 31) {
        std::nth_element(micros1.begin(), micros1.begin() + 15, micros1.end());
        std::nth_element(micros2.begin(), micros2.begin() + 15, micros2.end());
        std::nth_element(micros3.begin(), micros3.begin() + 15, micros3.end());
        Serial.printf( "micros: %d:%d:%d\r\n"
                     , micros1[15]
                     , micros2[15]
                     , micros3[15]);
        micros1.clear();
        micros2.clear();
        micros3.clear();
      }
    }
    _micros0 = micros();
//*/
    return true;
  }
};

#endif
