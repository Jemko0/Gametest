using Game;
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
            Update.Tick += Tick;
            return;
        }

        public virtual void Tick(object? sender, EventArgs e)
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