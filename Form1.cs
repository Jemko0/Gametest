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
            var o = new EEntity();
            o.InitializeEntity(new Vector2(400, 500), "base");
        }

        //ENGINE UPDATE LOOP
        public static string debugtxt;
        public static float delta;
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            DateTime startTime, endTime;
            startTime = DateTime.Now;

            while (IsApplicationIdle())
            {
                OnMapUpdated();
                cam.Update();
                Invalidate();
                //Thread.Sleep(10);
                
                label2.Text = debugtxt;
            }

            endTime = DateTime.Now;
            Double elapsedMillisecs = ((TimeSpan)(endTime - startTime)).TotalMilliseconds;
            delta = (float)elapsedMillisecs / 1000;

            label1.Text = delta.ToString();
        }

        bool IsApplicationIdle()
        {
            EngineWin32.NativeMessage result;
            return EngineWin32.PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
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

                if(obj.ticking)
                {
                    obj.Tick(delta);
                }
 
                if (obj.Rendering)
                {
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
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            //FPS Counter
            System.Windows.Forms.Timer FPSTimer = new System.Windows.Forms.Timer();
            FPSTimer.Interval = 500;
            FPSTimer.Start();
            FPSTimer.Tick += UpdateFPS;
        }

      

        DateTime _lastCheckTime = DateTime.Now;
        long _frameCount = 0;

        // called whenever a map is updated
        void OnMapUpdated()
        {
            Interlocked.Increment(ref _frameCount);
        }

        // called every once in a while
        double GetFps()
        {
            double secondsElapsed = (DateTime.Now - _lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref _frameCount, 0);
            double fps = count / secondsElapsed;
            _lastCheckTime = DateTime.Now;
            return fps;
        }

        private void UpdateFPS(object? sender, EventArgs e)
        {
            //label1.Text = GetFps().ToString();
        }
    }
}
