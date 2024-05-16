using Entity;
using Microsoft.VisualBasic;
using Object;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Timers;

namespace Gametest
{
    public partial class Form1 : Form
    {
        //globals
        private Dictionary<int, EObject> objs = new Dictionary<int, EObject>();

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
            Render();
        }

        private void Render()
        {
            //CLEAR SCREEN
            GameGraphics.Clear(Color.Black);
            EntityPass();
        }

        private void EntityPass()
        {
            for(int i = 0; i < objs.Count; i++)
            {
                var obj = objs[i];
                if (obj.Rendering)
                {
                    var e = obj as EEntity;
                    GameGraphics.DrawRectangle(GamePen, new Rectangle(e.Position.X, e.Position.Y, e.sInfo.Size.X));
                }
                
            }
        }

        public int CreateObject(EObject NewObject)
        {
            Random rnd = new Random();
            int num = rnd.Next(10000);

            objs.Add(num ,NewObject);
            return num;
        }

        public void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }
    }
}
