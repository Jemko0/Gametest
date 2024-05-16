using Entity;
using Object;
using System.Numerics;

namespace Gametest
{
    public partial class Form1 : Form
    {
        //globals
        private static Dictionary<int, EObject> objs = new Dictionary<int, EObject>();

        public Form1()
        {
            InitializeComponent();
            GameInit();
            var objec = new EEntity(new Vector2(10, 10), "Base");
            var objec1 = new EEntity(new Vector2(160, 25), "Base");
            var objec2 = new EEntity(new Vector2(310, 10), "Base");
            var objec3 = new EEntity(new Vector2(80, 60), "Base");
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
            GameGraphics.Clear(Color.White);
            EntityPass();
        }

        private void EntityPass()
        {
            for(int i = 0; i < objs.Count; i++)
            {
                EObject obj = objs.ElementAt(i).Value;

                if (obj.Rendering)
                {
                    EEntity e = obj as EEntity;
                    GameGraphics.FillRectangle(new SolidBrush(Color.Aqua), new Rectangle((int)e.Position.X, (int)e.Position.Y, (int)e.EDescription.HSize.X, (int)e.EDescription.HSize.Y));
                }
            }
        }

        public static int RegisterObject(EObject NewObject)
        {
            Random rnd = new Random();
            int num = rnd.Next(100000);

            objs.Add(num, NewObject);
            return num;
        }

        public static void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }
    }
}
