using Game;
using Gametest;
using Object;
using Object.Entity;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Engine.Data
{
    public struct TraverseResult
    {
        public bool traversable;
        public ID.TileID.TileData tile;
    }

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
            public Vector2 collisionlocation;
            public Vector2 normal;
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

        public static Vector2 ScreenToWorldCoordinates(Point ScreenPosition)
        {
            return ScreenToWorldCoordinates(new Vector2(ScreenPosition.X, ScreenPosition.Y));
        }

        public static Vector2 ScreenToWorldCoordinates(Vector2 ScreenPosition)
        {
            return new Vector2(ScreenPosition.X + GameClient.cam.position.X - 570, ScreenPosition.Y + GameClient.cam.position.Y - 450);
        }

        public static int TileSnap(float a)
        {
            int gridsize = 32;
            return (int)(Math.Round(a / gridsize) * gridsize);
        }

        public static Vector2 TileSnap(Vector2 a)
        {
            int gridsize = 32;
            return new Vector2((int)(Math.Round(a.X / gridsize) * gridsize), (int)(Math.Round(a.Y / gridsize) * gridsize));
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