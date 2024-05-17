using Game.Global.var;
using Gametest;

namespace Object
{
    public class EObject
    {
        public int OBJID;
        public bool Rendering;

        public EObject()
        {
            OBJID = Form1.RegisterObject(this);
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

        public virtual void FixedTick(object? sender, EventArgs e)
        {
            return;
        }

        public bool Destroy()
        {
            Form1.DestroyObject(OBJID);
            return true;
        }
    }
}