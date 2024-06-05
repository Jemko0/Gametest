using Object.Entity;
using Object.Entity.Character;
using Object;
using System.Numerics;
using Entity;
using Engine.Camera;
using Engine;
using System.Diagnostics;

namespace Gametest
{
    public partial class Form1 : Form
    {
        //globals
        public static Dictionary<int, EObject> objs = new Dictionary<int, EObject>();
        public EPlayer player;
        public Camera cam;
        public Form1()
        {
            InitializeComponent();
            GameInit();
            CreateLevel();
            cam = new Camera();
            CreatePlayer();
            cam.trackedEntity = player;
            Application.Idle += HandleApplicationIdle;
        }

        public void CreateLevel()
        {
           for (int i = 0; i < 1; i++)
            {
                EEntity o = new EEntity();
                int rnd = new Random().Next(5000);
                o.InitializeEntity(new Vector2(0, 600), "base");
                RegisterObject(o);
                Thread.Sleep(1);
            }
        }

        //ENGINE UPDATE LOOP
        public static string debugtxt;
        public static float delta;
        public static DateTime endTime = DateTime.Now, startTime = DateTime.Now;
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            while (IsApplicationIdle())
            {
                cam.Update();
                Invalidate(); //render
            }
        }

        void EndFPSMeasure()
        {
            float elapsedsec = (float)((TimeSpan)(endTime - startTime)).TotalSeconds;
            delta = elapsedsec;
            label1.Text = "FPS: " + Math.Floor(1 / delta).ToString() + "\n" + "DELTA: " + (delta * 1000).ToString() + "\n" + "OBJ:" + objs.Count.ToString();
            endTime = DateTime.Now;
            return;
        }

        bool IsApplicationIdle()
        {
            EngineWin32.NativeMessage result;
            return EngineWin32.PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        public static int RegisterObject(EObject NewObject)
        {
            Random rnd = new Random();
            int num = rnd.Next(1000000);

            if(!objs.ContainsKey(num))
            {
                objs.Add(num, NewObject);
            }
            num = -1;
            NewObject = null; 
            return num;
        }

        public static void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }


        public void CreatePlayer()
        {
            player = new EPlayer();
            RegisterObject(player);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                EObject obj = objs.ElementAt(i).Value;

                if (obj.Rendering)
                {
                    //Tick Object
                    obj.Tick(delta);

                    EEntity en = obj as EEntity;
                    if (en != null && en.active)
                    {
                        if (en.EDescription.Sprite != null)
                        {
                            e.Graphics.DrawImage(en.EDescription.Sprite, new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                            return;
                        }

                        e.Graphics.FillRectangle(new SolidBrush(Color.Black), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                        e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam)), new SizeF(10, 10)));

                        //e.Graphics.FillRectangle(new SolidBrush(Color.Aqua), new RectangleF(((int)en.Position.X) - (int)cam.position.X, ((int)en.Position.Y) - (int)cam.position.Y, (int)en.EDescription.HSize.X, (int)en.EDescription.HSize.Y));
                    }
                }
                EndFPSMeasure();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
