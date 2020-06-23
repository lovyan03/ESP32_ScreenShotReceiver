# ESP32_ScreenShotReceiver
  
[![movie](http://img.youtube.com/vi/-bT72JhCF5o/0.jpg)](http://www.youtube.com/watch?v=-bT72JhCF5o "movie")
  
PCの画面をJPEGにしてTCPで送信し、M5Stack/ESP32で受信して表示します。
  
## Usage  
受信側 ESP32 ScreenShotReceiver  
起動すると、WiFi接続を試行し、接続できない場合はSmartConfigが起動します。  
お手持ちのスマートフォンにSmartConfigアプリを入れて設定を行ってください。  
WiFi接続に成功すると、コンソールとLCDにIPアドレスを表示してWindowsアプリからの接続を待ちます。  
  
送信側 Windows ScreenShotSender  
画像の幅・高さ・画質の各パラメータとJPEG化する範囲が設定できます  
受信側のIPアドレスを入力して接続すると、Windowsの画面の一部がLCDに表示されます。  
受信側が複数ある場合は、Col/Row で列数・行数を増やして画像を分割することができます。  
キャプチャウィンドウはマウス操作とキーボードのカーソルキーで移動できます。  
Ctrlキーを押しながらカーソルキーを操作すると、移動量が大きくなります。  
Shiftキーを押しながらカーソルキーを操作することで幅と高さが調整できます。  

## Requirement
動作には以下のライブラリが必要です。  

* https://github.com/lovyan03/LovyanGFX/

## Licence

[MIT](https://github.com/lovyan03/ESP32_ScreenShotReceiver/blob/master/LICENSE)  

## Author

[lovyan03](https://twitter.com/lovyan03)  
