using Engine.Camera;
using Object;
using Object.Entity;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Engine
{
    public class EngineWin32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);
    }

    public class EngineStructs
    {
        public struct ECollisionResult()
        {
            public bool collision = false;
            public EObject hitobject = null;
        }
    }

    public class EngineFunctions
    {
        public static Vector2 GetRenderTranslation(EEntity e, Engine.Camera.Camera cam)
        {
            Vector2 result = new Vector2();
            result = new Vector2((int)e.Position.X - cam.position.X + 550, (int)e.Position.Y - cam.position.Y + 400);

            return result;
        }

        public static float Lerp(float a, float b, float f)
        {
            return (a * (1.0f - f)) + (b * f);
        }
    }
}