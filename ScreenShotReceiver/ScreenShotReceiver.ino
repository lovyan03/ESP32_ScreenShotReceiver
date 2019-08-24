#pragma GCC optimize ("O3")

#if defined(ARDUINO_M5Stack_Core_ESP32) || defined(ARDUINO_M5STACK_FIRE)
// M5Stack は設定不要
  #include <M5Stack.h>
  #include <M5StackUpdater.h>     // https://github.com/tobozo/M5Stack-SD-Updater/
  TFT_eSPI &Tft = M5.Lcd;

#elif defined(ARDUINO_M5Stick_C)
// M5Stick-C は設定不要
  #include <M5StickC.h>
  TFT_eSPI &Tft = M5.Lcd;

#else
// M5Stack・M5Stick-C以外の環境の場合
// Bodmer氏のTFT_eSPIをインストールし、LCDに合わせて設定してください。
//
//  https://github.com/Bodmer/TFT_eSPI
//
// TFT_eSPIライブラリの User_Setup.h ファイルを開いて下記の設定を行う。
// Section 1.で使用するLCDに合ったdefine値 を有効にする。
//   ※ バックライト点灯が必要な場合は TFT_BACKLIGHT_ON も設定する。
// Section 2.で使用しているGPIOに対応したdefine値を設定する。
//   ※ TFT_MOSI  TFT_MISO  TFT_SCLK  TFT_CS  TFT_DC  TFT_RST  TFT_BL の各値
// Section 4.でSPIクロック値を設定する。
//   ※ 320x240で32fpsを出すには40MHzが必要ですが、
//      表示が乱れる場合はクロック値を下げてください。

  #include <TFT_eSPI.h>  // https://github.com/Bodmer/TFT_eSPI

  TFT_eSPI Tft;

#endif

#include <WiFi.h>
#include <esp_wifi.h>

#include "src/TCPReceiver.h"
#include "src/DMADrawer.h"

TCPReceiver tcpr;

void setup(void)
{

#if defined(ARDUINO_M5Stack_Core_ESP32) || defined(ARDUINO_M5STACK_FIRE) || defined(ARDUINO_M5Stick_C)
  M5.begin();
  Tft.setRotation(1);
#else
  Serial.begin(115200);
  Serial.flush();
  Tft.begin();
  Tft.setRotation(3);
#endif

  int offset_x = 0;
  int offset_y = 0;
  int width  = Tft.width();
  int height = Tft.height();
  if (width  > 320) width  = 320;
  if (height > 240) height = 240;

#ifdef ARDUINO_M5Stick_C
// M5Stick-CのLCDは実際の画面サイズと内部メモリサイズが異なる
// 内部メモリサイズは162x132のため、x=1 y=26のオフセットが必要
  offset_x = 1;
  offset_y = 26;
#endif

  Tft.setTextFont(2);

#ifdef __M5STACKUPDATER_H
  if(digitalRead(BUTTON_A_PIN) == 0) {
     Serial.println("Will Load menu binary");
     updateFromFS(SD);
     ESP.restart();
  }
#endif

  Serial.println("WiFi begin.");
  Tft.println("WiFi begin.");
  // 記憶しているAPへ接続試行
  WiFi.mode(WIFI_MODE_STA);
  WiFi.begin();

  // 接続できるまで10秒待機
  for (int i = 0; WiFi.status() != WL_CONNECTED && i < 100; i++) { delay(100); }

  // 接続できない場合はSmartConfigを起動
  // https://itunes.apple.com/app/id1071176700
  // https://play.google.com/store/apps/details?id=com.cmmakerclub.iot.esptouch
  if (WiFi.status() != WL_CONNECTED) {
    Serial.print("SmartConfig start.");
    Tft.println("SmartConfig start.");
    WiFi.mode(WIFI_MODE_APSTA);
    WiFi.beginSmartConfig();

    while (WiFi.status() != WL_CONNECTED) {
      delay(100);
    }
    WiFi.stopSmartConfig();
    WiFi.mode(WIFI_MODE_STA);
  }

  Serial.println(String("IP:") + WiFi.localIP().toString());
  Tft.println(WiFi.localIP().toString());

  tcpr.setup( offset_x
            , offset_y
            , width
            , height
            , SPI_FREQUENCY
            , TFT_MOSI
            , TFT_MISO
            , TFT_SCLK
            , TFT_CS
            , TFT_DC
            );
}

void loop(void)
{
  tcpr.loop();
}
