using Gametest.Properties;
using System.Runtime.CompilerServices;

namespace Gametest
{
    partial class GameClient
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            RenderThread = new System.ComponentModel.BackgroundWorker();
            imageList1 = new ImageList(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(1103, 545);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(593, 90);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // GameClient
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1280, 720);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            HelpButton = true;
            Name = "GameClient";
            Text = "2D Engine";
            TransparencyKey = Color.Fuchsia;
            Load += Form1_Load;
            Paint += Render;
            MouseDown += GameClient_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        //GAME CONTENT
        public static Graphics UIGraphics;
        public Pen GamePen;
        public Brush GameBrush;
        public Control input;
        private void GameInit()
        {
            sm = new SceneManager(this);
        }

        #endregion

        private Label label1;
        public System.ComponentModel.BackgroundWorker RenderThread;
        private ImageList imageList1;
        public PictureBox pictureBox1;
    }
}
