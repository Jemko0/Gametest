using Game;
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

        public bool Destroy()
        {
            GameClient.DestroyObject(OBJID);
            GC.Collect();
            return true;
        }
    }
}