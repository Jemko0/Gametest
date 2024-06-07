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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(1172, 629);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // GameClient
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(1280, 720);
            Controls.Add(label1);
            Name = "GameClient";
            Text = "2D Engine";
            TransparencyKey = Color.Fuchsia;
            Load += Form1_Load;
            Paint += Render;
            ResumeLayout(false);
            PerformLayout();
        }

        //GAME CONTENT
        public Graphics GameGraphics;
        public Pen GamePen;
        public Brush GameBrush;
        public Control input;
        private void GameInit()
        {
            sm = new SceneManager(this);
        }

        #endregion

        private Label label1;
    }
}
