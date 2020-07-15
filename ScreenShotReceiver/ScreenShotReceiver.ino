/*----------------------------------------------------------------------------/
  ESP32 ScreenShotReceiver  

Original Source:  
 https://github.com/lovyan03/ESP32_ScreenShotReceiver/  

Licence:  
 [MIT](https://github.com/lovyan03/ESP32_ScreenShotReceiver/blob/master/LICENSE)  

Author:  
 [lovyan03](https://twitter.com/lovyan03)  

/----------------------------------------------------------------------------*/

#if defined(ARDUINO_M5Stack_Core_ESP32) || defined(ARDUINO_M5STACK_FIRE)
#include <M5StackUpdater.h>     // https://github.com/tobozo/M5Stack-SD-Updater/
#endif

#include <esp_wifi.h>

#include "src/TCPReceiver.h"

static LGFX lcd;
static TCPReceiver recv;

void setup(void)
{
  Serial.begin(115200);
  Serial.flush();

#if defined ( __M5STACKUPDATER_H )
  M5.begin();
  #ifdef __M5STACKUPDATER_H
    if(digitalRead(BUTTON_A_PIN) == 0) {
       Serial.println("Will Load menu binary");
       updateFromFS(SD);
       ESP.restart();
    }
  #endif
#endif

  lcd.begin();
//lcd.setColorDepth(24);
  lcd.setRotation(0);
  if (lcd.width() < lcd.height())
    lcd.setRotation(1);

  lcd.setFont(&fonts::Font2);

  Serial.println("WiFi begin.");
  lcd.println("WiFi begin.");
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
    lcd.println("SmartConfig start.");
    WiFi.mode(WIFI_MODE_APSTA);
    WiFi.beginSmartConfig();

    while (WiFi.status() != WL_CONNECTED) {
      delay(100);
    }
    WiFi.stopSmartConfig();
    WiFi.mode(WIFI_MODE_STA);
  }

  Serial.println(String("IP:") + WiFi.localIP().toString());
  lcd.println(WiFi.localIP().toString());

  recv.setup( &lcd );
}

void loop(void)
{
  recv.loop();
}
