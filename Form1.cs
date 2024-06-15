using Object.Entity;
using Object;
using Engine.Camera;
using Engine.Data;
using Gametest.GameContent.World;
using Engine;
using Gametest.GameContent.Gameplay;
using Game;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Gametest
{
    public partial class GameClient : Form
    {
        //globals
        public static Dictionary<int, EObject> objs = new Dictionary<int, EObject>();
        public static Dictionary<EngineStructs.IntVector2, string> worldtiles = new Dictionary<EngineStructs.IntVector2, string>();
        public static Camera cam;
        public static EPlayer player;
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
            player = new EPlayer();
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
                if (dtpos1 != Vector2.Zero && dtpos2 != Vector2.Zero)
                {
                    Vector2 cp;
                    Vector2 cn;
                    float t;

                    Renderer.DrawDebugPoint(new DebugDrawing(DebugDrawingType.Line, dtpos1, dtpos2, Color.Black));

                    foreach( var tile in worldtiles)
                    {
                        if (CollisionDetections.RayVRect(new Ray(dtpos1, dtpos2 - dtpos1), new RectangleF(tile.Key.x, tile.Key.y, 32, 32), out cp, out cn, out t) && t < 1.0f)
                        {
                            Renderer.DrawDebugPoint(new DebugDrawing(DebugDrawingType.Rect, new RectangleF(tile.Key.x, tile.Key.y, 32, 32), Color.Red));
                        }
                    }
                    
                }
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
            TickObjects();
            cam.Update();
        }

        public void TickObjects()
        {
            foreach(var obj in objs.Values.ToList()) 
            {
                obj.ticking = cam.PosInCamBounds((obj as EEntity).Position);

                if (obj.ticking)
                {
                    obj.Tick(delta);
                }
            }
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
                + "PPOS:" + player.Position + "\n"
                + "PVEL:" + player.velocity;

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
            rob = Renderer.Render(sender, e, this);
            EndFPSMeasure(rob);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        public static Vector2 dtpos1;
        public static Vector2 dtpos2;
        private void GameClient_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                dtpos1 = EngineFunctions.ScreenToWorldCoordinates(e.Location);
            }

            if (e.Button == MouseButtons.Right)
            {
                dtpos2 = EngineFunctions.ScreenToWorldCoordinates(e.Location);
            }
        }
    }
}
