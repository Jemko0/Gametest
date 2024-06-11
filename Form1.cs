using Object.Entity;
using Object;
using Engine.Camera;
using Engine.Data;
using Gametest.GameContent.World;
using Engine;
using System.Runtime.Intrinsics;
using System.ComponentModel;

namespace Gametest
{
    public partial class GameClient : Form
    {
        //globals
        public static Dictionary<int, EObject> objs = new Dictionary<int, EObject>();
        public static Dictionary<EngineStructs.IntVector2, string> worldtiles = new Dictionary<EngineStructs.IntVector2, string>();
        public static Camera cam;
        private SceneManager sm;
        public GameClient()
        {
            InitializeComponent();
            GameInit();
            CreatePlyAndCam();
            Application.Idle += HandleApplicationIdle;

            Worldgen.Generate();
        }

        public void CreatePlyAndCam()
        {
            EPlayer player = new EPlayer();
            player.InitializeEntity(new System.Numerics.Vector2(400, -800), "player");

            cam = new Camera(player.Position);
            cam.trackedEntity = player;
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
                EngineLoop();
                Invalidate(); //render
            }
        }

        bool IsApplicationIdle()
        {
            EngineWin32.NativeMessage result;
            return EngineWin32.PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        //engine loop
        void EngineLoop()
        {
            cam.Update();
        }

        void EndFPSMeasure(int rendobj)
        {
            endTime = DateTime.Now;
            float elapsedsec = (float)((TimeSpan)(endTime - startTime)).TotalSeconds;
            delta = elapsedsec;
            label1.Text =

                "FPS: " + Math.Floor(1 / delta).ToString() + "\n"
                + "DELTA: " + (delta * 1000).ToString() + "\n"
                + "OBJ:" + objs.Count.ToString() + "\n"
                + "RO:" + rendobj.ToString();

            return;
        }

        public static int RegisterObject(EObject? NewObject)
        {
            Random rnd = new Random();

        rand:
            int num = rnd.Next(1000000);
            if (!objs.ContainsKey(num))
            {
                objs.Add(num, NewObject);
                return num;
            }
            goto rand;
        }

        public static void DestroyObject(int ObjectID)
        {
            objs.Remove(ObjectID);
        }
        private void Render(object sender, PaintEventArgs e)
        {
            int rob;
            RenderThread.RunWorkerAsync();
            rob = Renderer.Render(sender, e, this);
            EndFPSMeasure(rob);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
