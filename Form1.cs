using Object.Entity;
using Object;
using Engine.Camera;
using Engine;
using Microsoft.VisualBasic.ApplicationServices;

namespace Gametest
{
    public partial class GameClient : Form
    {
        //globals
        public static Dictionary<int, EObject> objs = new Dictionary<int, EObject>();
        public Camera cam;
        private SceneManager sm;
        public GameClient()
        {
            InitializeComponent();
            GameInit();
            cam = new Camera();
            CreatePlayer();
            Application.Idle += HandleApplicationIdle;
        }

        public EPlayer CreatePlayer()
        {
            EPlayer player = new EPlayer();
            player.InitializeEntity(new System.Numerics.Vector2(400, -800), "player");

            cam.trackedEntity = player;
            return player;
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
            endTime = DateTime.Now;
            float elapsedsec = (float)((TimeSpan)(endTime - startTime)).TotalSeconds;
            delta = elapsedsec;
            label1.Text = "FPS: " + Math.Floor(1 / delta).ToString() + "\n"
                + "DELTA: " + (delta * 1000).ToString() + "\n"
                + "OBJ:" + objs.Count.ToString() + "\n";
            return;
        }

        bool IsApplicationIdle()
        {
            EngineWin32.NativeMessage result;
            return EngineWin32.PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        public static int RegisterObject(EObject? NewObject)
        {
            Random rnd = new Random();
            int num = rnd.Next(1000000);
                objs.Add(num, NewObject);
            NewObject = null; 
            return num;
        }

        public static void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }


<<<<<<< Updated upstream
        public void CreatePlayer()
        {
            player = new EPlayer();
            RegisterObject(player);
        }

        private void Render(object sender, PaintEventArgs e)
=======
        private void Form1_Paint(object sender, PaintEventArgs e)
>>>>>>> Stashed changes
        {
            for (int i = 0; i < objs.Count; i++)
            {
                EObject obj = objs.ElementAt(i).Value;

                if (obj.ticking)
                {
                    obj.Tick(delta);
                }

                if(obj.Rendering)
                {
                    EEntity en = obj as EEntity;
                    if (en != null && en.active)
                    {
                        if (en.EDescription.Sprite != null)
                        {
                            e.Graphics.DrawImage(en.EDescription.Sprite, new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam, this)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                            EndFPSMeasure();
                            return;
                        }

                        e.Graphics.FillRectangle(new SolidBrush(Color.Black), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam, this)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                        e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en, cam, this)), new SizeF(10, 10)));

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
