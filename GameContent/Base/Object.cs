using Game.Global.var;
using Gametest;

namespace Object
{
    public class EObject
    {
        public int OBJID;
        public float odelta;
        public bool Rendering;
        public bool ticking;
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

        public virtual void Init()
        {
            return;
        }

        public virtual void Tick(float delta)
        {
            odelta = delta;
            return;
        }

        public virtual void FixedTick(object? sender, EventArgs e)
        {
            return;
        }

        public bool Destroy()
        {
            GameClient.DestroyObject(OBJID);
            return true;
        }
    }
}