using Game.Global.var;
using Gametest;

namespace Object
{
    /// <summary>
    /// Base class for all in-engine classes, Objects register themselves
    /// </summary>
    public class EObject
    {
        public int OBJID;
        public float odelta;
        public bool Rendering;
        public bool ticking;

        //Collision Properties
        public float restitution = 0.1f;
        public EObject()
        {
            OBJID = GameClient.RegisterObject(this);
            System.Windows.Forms.Timer Update = new System.Windows.Forms.Timer();
            Update.Interval = GameProperties.ms_tickrate;
            Update.Tick += FixedTick;
            Update.Start();

            Init();
            return;
        }


        /// <summary>
        /// Init runs once at game start or when the object is spawned (called by constructor)
        /// </summary>
        public virtual void Init()
        {
            return;
        }

        /// <summary>
        /// Tick runs every frame
        /// </summary>
        public virtual void Tick(float delta)
        {
            odelta = delta;
            return;
        }

        /// <summary>
        /// FixedTick runs at 60 Ticks per second and will wait if the frame is completed faster than 60 times a second
        /// </summary>
        public virtual void FixedTick(object? sender, EventArgs e)
        {
            return;
        }

        public bool Destroy()
        {
            GameClient.DestroyObject(OBJID);
            GC.Collect();
            return true;
        }
    }
}