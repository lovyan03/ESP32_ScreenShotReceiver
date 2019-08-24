namespace ScreenShotSender
{
    partial class FormSenderMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnShowFrame = new System.Windows.Forms.Button();
            this.btnStartStopr = new System.Windows.Forms.Button();
            this.pnlTop1 = new System.Windows.Forms.Panel();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlAll = new System.Windows.Forms.Panel();
            this.pnlTop2 = new System.Windows.Forms.Panel();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.pnlTop1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.pnlAll.SuspendLayout();
            this.pnlTop2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowFrame
            // 
            this.btnShowFrame.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnShowFrame.Location = new System.Drawing.Point(0, 0);
            this.btnShowFrame.Name = "btnShowFrame";
            this.btnShowFrame.Size = new System.Drawing.Size(81, 19);
            this.btnShowFrame.TabIndex = 0;
            this.btnShowFrame.Text = "ShowBox";
            this.btnShowFrame.UseVisualStyleBackColor = true;
            this.btnShowFrame.Click += new System.EventHandler(this.btnShowFrame_Click);
            // 
            // btnStartStopr
            // 
            this.btnStartStopr.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartStopr.Location = new System.Drawing.Point(81, 0);
            this.btnStartStopr.Name = "btnStartStopr";
            this.btnStartStopr.Size = new System.Drawing.Size(81, 19);
            this.btnStartStopr.TabIndex = 1;
            this.btnStartStopr.Text = "Connect";
            this.btnStartStopr.UseVisualStyleBackColor = true;
            this.btnStartStopr.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // pnlTop1
            // 
            this.pnlTop1.Controls.Add(this.tbHost);
            this.pnlTop1.Controls.Add(this.btnStartStopr);
            this.pnlTop1.Controls.Add(this.btnShowFrame);
            this.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop1.Location = new System.Drawing.Point(0, 0);
            this.pnlTop1.Name = "pnlTop1";
            this.pnlTop1.Size = new System.Drawing.Size(334, 19);
            this.pnlTop1.TabIndex = 2;
            // 
            // tbHost
            // 
            this.tbHost.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbHost.Location = new System.Drawing.Point(162, 0);
            this.tbHost.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(77, 19);
            this.tbHost.TabIndex = 2;
            this.tbHost.Text = "192.168.1.1";
            // 
            // nudQuality
            // 
            this.nudQuality.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudQuality.Location = new System.Drawing.Point(195, 0);
            this.nudQuality.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(43, 19);
            this.nudQuality.TabIndex = 14;
            this.nudQuality.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(154, 0);
            this.label2.MinimumSize = new System.Drawing.Size(0, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Quality";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudHeight
            // 
            this.nudHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudHeight.Location = new System.Drawing.Point(111, 0);
            this.nudHeight.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.nudHeight.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(43, 19);
            this.nudHeight.TabIndex = 4;
            this.nudHeight.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // nudWidth
            // 
            this.nudWidth.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudWidth.Location = new System.Drawing.Point(32, 0);
            this.nudWidth.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.nudWidth.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(43, 19);
            this.nudWidth.TabIndex = 3;
            this.nudWidth.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // pbPreview
            // 
            this.pbPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(334, 242);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 3;
            this.pbPreview.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlAll
            // 
            this.pnlAll.Controls.Add(this.pbPreview);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAll.Location = new System.Drawing.Point(0, 36);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(334, 242);
            this.pnlAll.TabIndex = 4;
            // 
            // pnlTop2
            // 
            this.pnlTop2.Controls.Add(this.nudQuality);
            this.pnlTop2.Controls.Add(this.label2);
            this.pnlTop2.Controls.Add(this.nudHeight);
            this.pnlTop2.Controls.Add(this.lblHeight);
            this.pnlTop2.Controls.Add(this.nudWidth);
            this.pnlTop2.Controls.Add(this.lblWidth);
            this.pnlTop2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop2.Location = new System.Drawing.Point(0, 19);
            this.pnlTop2.Name = "pnlTop2";
            this.pnlTop2.Size = new System.Drawing.Size(334, 17);
            this.pnlTop2.TabIndex = 5;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeight.Location = new System.Drawing.Point(75, 0);
            this.lblHeight.MinimumSize = new System.Drawing.Size(0, 19);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(36, 19);
            this.lblHeight.TabIndex = 6;
            this.lblHeight.Text = "height";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWidth.Location = new System.Drawing.Point(0, 0);
            this.lblWidth.MinimumSize = new System.Drawing.Size(0, 19);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(32, 19);
            this.lblWidth.TabIndex = 5;
            this.lblWidth.Text = "width";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormSenderMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 278);
            this.Controls.Add(this.pnlAll);
            this.Controls.Add(this.pnlTop2);
            this.Controls.Add(this.pnlTop1);
            this.Name = "FormSenderMain";
            this.Text = "ScreenShotSender";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSenderMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormSenderMain_Shown);
            this.pnlTop1.ResumeLayout(false);
            this.pnlTop1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.pnlAll.ResumeLayout(false);
            this.pnlTop2.ResumeLayout(false);
            this.pnlTop2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowFrame;
        private System.Windows.Forms.Button btnStartStopr;
        private System.Windows.Forms.Panel pnlTop1;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Panel pnlAll;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Panel pnlTop2;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.Label label2;
    }
}

