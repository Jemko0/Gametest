using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Timers;

namespace Gametest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameInit();

            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1;   // milliseconds
            tmr.Tick += Tmr_Tick; ;  // set handler
            tmr.Start();
        }

        private void Tmr_Tick(object? sender, EventArgs e)
        {
            GEUpdate();
        }

        private void GEUpdate()
        {
                GameGraphics.Clear(Color.White);
                GameGraphics.FillRectangle(GameBrush, new Rectangle(0, 0, DateAndTime.Now.Millisecond * 1, 150));
        }
    }
}
