# ESP32_ScreenShotReceiver
  
[![movie](http://img.youtube.com/vi/-bT72JhCF5o/0.jpg)](http://www.youtube.com/watch?v=-bT72JhCF5o "movie")
  
PCの画面をJPEGにしてTCPで送信し、ESP32で受信して表示します。  
Send the screen of your PC as a JPEG, send it by TCP, receive it by ESP32, and display it It is.  
  

## 対応環境 Environment
  
LCDの設定に [LovyanGFX](https://github.com/lovyan03/LovyanGFX/) を使用します。LovyanGFXが動作するESP32環境に対応しています。  
Use [LovyanGFX](https://github.com/lovyan03/LovyanGFX/) to set up the LCD. It is compatible with the ESP32 environment in which LovyanGFX runs.  
  
M5Stack / M5StickC / TTGO T-Watch / ODROID-GO / ESP-WROVER-KIT / and more...  
  
## 使い方 Usage  
### 受信側 Receiver : ESP32 ScreenShotReceiver  
起動すると、WiFi接続を試行し、接続できない場合はSmartConfigが起動します。  
お手持ちのスマートフォンにSmartConfigアプリを入れて設定を行ってください。  
WiFi接続に成功すると、シリアルモニタとLCDにIPアドレスを表示してWindowsアプリからの接続を待ちます。  
  
When launched, will attempt to connect to WiFi, and if it can't connect, SmartConfig will start.  
Put the SmartConfig app on your smartphone and set it up.  
If the WiFi connection is successful, the serial monitor and LCD will display the IP address and wait for the connection from the Windows app.  
  
### 送信側 Sender : Windows ScreenShotSender  
受信側のIPアドレスを入力して接続すると、Windowsの画面の一部がESP32のLCDに表示されます。  
画像の幅・高さ・画質の各パラメータとJPEG化する範囲が設定できます  
受信側が複数ある場合は、Col・Row で列数・行数を増やして画像を分割することができます。  
  
When you enter the IP address of the receiver and connect, a portion of the Windows screen appears on the ESP32's LCD.  
Allows you to set the width, height, quality of the image and the area to be JPEG'd  
If there is more than one receiver, you can split the image by increasing the number of columns and rows.  
  
  
キャプチャウィンドウはマウス操作とキーボードのカーソルキーで移動およびサイズ調整ができます。  
Ctrlキー ＋ カーソルキー操作 : ウィンドウの移動距離が増えます。  
Shiftキー ＋ カーソルキー操作 : ウィンドウの幅と高さが調整できます。  
  
The capture window can be moved and resized using the mouse and the keyboard cursor keys. 
hold down the Ctrl key and use the cursor keys : Increase the move distance.  
Hold down the Shift key and use the cursor keys : Adjust the width and height.  

## 必要なライブラリ Requirement library

* https://github.com/lovyan03/LovyanGFX/  
* https://github.com/tobozo/M5Stack-SD-Updater/  (for M5Stack)  



## 含まれているライブラリ Included library
[TJpgDec](http://elm-chan.org/fsw/tjpgd/00index.html) を改造して使用しています。  


## Licence

[MIT](https://github.com/lovyan03/ESP32_ScreenShotReceiver/blob/master/LICENSE)  

## Author

[lovyan03](https://twitter.com/lovyan03)  
