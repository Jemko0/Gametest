using Entity;
using Microsoft.VisualBasic;
using Object;
using System.Diagnostics;
using System.Numerics;
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

            EEntity ent = new EEntity(new Vector2(0, 0), "lol");
            CreateObject(ent);
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1;   // milliseconds
            tmr.Tick += Tmr_Tick;
            tmr.Start();
        }

        private void Tmr_Tick(object? sender, EventArgs e)
        {
            GEUpdate();
        }

        //GameEngine Update
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
                EObject obj = objs[i];
                if (obj.Rendering)
                {
                    EEntity e = obj as EEntity;
                    if(e != null)
                    {
                    GameGraphics.DrawRectangle(new Pen(Color.White), new Rectangle((int)e.Position.X, (int)e.Position.Y, (int)e.sInfo.Size.X, (int)e.sInfo.Size.Y));
                    }
                }
            }
        }

        public int CreateObject(EObject NewObject)
        {
            Random rnd = new Random();
            int num = rnd.Next(10000);

            objs.Add(num, NewObject);
            return num;
        }

        public void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }
    }
}
