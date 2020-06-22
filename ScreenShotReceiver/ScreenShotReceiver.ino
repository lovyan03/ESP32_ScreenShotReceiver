#pragma GCC optimize ("O3")

#include <LovyanGFX.hpp>  // https://github.com/lovyan03/LovyanGFX/

#include <WiFi.h>
#include <esp_wifi.h>

#include "src/TCPReceiver.h"

static LGFX lcd;
static TCPReceiver recv;

void setup(void)
{
  Serial.begin(115200);
  Serial.flush();
  lcd.begin();

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
