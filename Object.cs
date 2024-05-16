using Gametest;

namespace Object
{
    public class EObject
    {
        Form1 f = new Form1();
        public int OBJID;
        public bool Rendering;
        public EObject()
        {
            OBJID = f.CreateObject(this);
            return;
        }

        public bool Destroy()
        {
            f.DestroyObject(OBJID);
            return true;
        }
    }
}