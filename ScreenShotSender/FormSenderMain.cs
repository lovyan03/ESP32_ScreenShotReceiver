using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenShotSender
{
    public partial class FormSenderMain : Form
    {
        FormCaptureBox formCaptureBox = new FormCaptureBox();
        ImageCodecInfo _jpgEncoder = null;
        Bitmap _resizeBmp;
        Graphics _gResizeBmp;
        EncoderParameters _encParams = new EncoderParameters(1);
        int _jpgQuality = 60;
        int _port = 63333;
        TCPSender _tcp;
        System.Diagnostics.Stopwatch _sw;

        public FormSenderMain()
        {
            InitializeComponent();
        }

        private void FormSenderMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop();
        }


        [DllImport("user32.dll")]
        extern static IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("gdi32.dll")]
        extern static int GetDeviceCaps(IntPtr hdc, int index);
        [DllImport("user32.dll")]
        extern static int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        private void FormSenderMain_Shown(object sender, EventArgs e)
        {
            const int LOGPIXELSX = 88;
            const int LOGPIXELSY = 90;
            var dc = GetWindowDC(IntPtr.Zero);
            var dpiX = GetDeviceCaps(dc, LOGPIXELSX);
            var dpiY = GetDeviceCaps(dc, LOGPIXELSY);
            ReleaseDC(IntPtr.Zero, dc);

            _sw = new System.Diagnostics.Stopwatch();
            _tcp = new TCPSender();

            foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageEncoders()) {
                if (ici.FormatID == ImageFormat.Jpeg.Guid) {
                    _jpgEncoder = ici;
                    break;
                }
            }

            formCaptureBox.Location = new Point(Location.X + 10, Location.Y + Height);
            FrameVisible(true);

            formCaptureBox.Width = 320 * dpiX / 96;
            formCaptureBox.Height = 240 * dpiY/ 96;
            timer1.Enabled = true;
        }

        private void FrameVisible(bool flg)
        {
            formCaptureBox.Visible = flg;
            btnShowFrame.Text = flg ? "HideBox" : "ShowBox";
        }
        private void btnShowFrame_Click(object sender, EventArgs e)
        {
            FrameVisible(!formCaptureBox.Visible);
            if (formCaptureBox.Visible) formCaptureBox.Focus();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_sw.IsRunning) stop();
            else start();
            FrameVisible(!_sw.IsRunning);
        }

        void start()
        {
            _tcp.start(tbHost.Text, _port);
            _sw.Start();
            btnStartStopr.Text = "Disconnect";
        }

        void stop()
        {
            _sw?.Stop();
            _tcp?.stop();
            btnStartStopr.Text = "Connect";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (_resizeBmp == null || _resizeBmp.Width != nudWidth.Value || _resizeBmp.Height != nudHeight.Value)
            {
                _gResizeBmp?.Dispose();
                _resizeBmp?.Dispose();
                _resizeBmp = new Bitmap((int)nudWidth.Value, (int)nudHeight.Value, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                _gResizeBmp = Graphics.FromImage(_resizeBmp);
                _gResizeBmp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            }
            
            var bmp = formCaptureBox.CaptureFrame();
            _gResizeBmp.DrawImage(bmp, 0, 0, _resizeBmp.Width, _resizeBmp.Height);
            pbPreview.Image = _resizeBmp;
            pbPreview.Invalidate();

            if (_sw.IsRunning) tcpJPGPrepare();
            timer1.Enabled = true;
        }

        private void tcpJPGPrepare()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] rgbValues = { 0 };
                _jpgQuality = (int)nudQuality.Value;
                _encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, _jpgQuality);
                ms.WriteByte(0x4A); // prefix "JPG" 3Byte
                ms.WriteByte(0x50);
                ms.WriteByte(0x47);
                ms.WriteByte(0);    // data len 4Byte
                ms.WriteByte(0);
                ms.WriteByte(0);
                ms.WriteByte(0);
                _resizeBmp.Save(ms, _jpgEncoder, _encParams);
                ms.Capacity = (int)ms.Length;
                rgbValues = ms.GetBuffer();
                {
                    UInt32 len = (UInt32)(ms.Length - 7);
                    rgbValues[3] = (byte)(len & 0xFF);
                    rgbValues[4] = (byte)((len >> 8) & 0xFF);
                    rgbValues[5] = (byte)((len >> 16) & 0xFF);
                    rgbValues[6] = (byte)((len >> 24) & 0xFF);
                    _tcp.setData(rgbValues);
                }
            }
        }
    }
}
