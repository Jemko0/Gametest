using Engine.Camera;
using Gametest;
using Object;
using Object.Entity;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Engine.Data
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

    public static class EngineStructs
    {

        public struct IntVector2(int x, int y)
        {
            public int x = x;
            public int y = y;
        }

        public struct ECollisionResult()
        {
            public bool collision = false;
            public EObject hitobject = null;
        }

        public struct WTile(short inx, short iny)
        {
            public string id;
            public short x = inx;
            public short y = iny;
        }
        public struct SceneObject()
        {
            public EObject obj;
            public Vector2 pos;
            public bool tick;
            public bool render;
        }

        public struct SceneEntity()
        {
            public EEntity entity;
        }
        public struct Scene()
        {
            public SceneObject[] objects;
            public SceneEntity[] entities;
        }
    }

    public class EngineFunctions
    {
        public static Vector2 GetRenderTranslation(Vector2 enitityposition, Engine.Camera.Camera cam, GameClient gc)
        {
            Vector2 result = new Vector2();
            result = new Vector2((int)enitityposition.X - cam.position.X + 550, (int)(enitityposition.Y - cam.position.Y + 400));

            return result;
        }

        public static float Lerp(float a, float b, float f)
        {
            float min;
            float max;

            min = Math.Min(a, b);
            max = Math.Max(a, b);
            return Math.Clamp((a * (1.0f - f)) + (b * f), min, max);
        }
    }
}