using Engine.Data;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;

namespace Engine
{
    public struct Ray(Vector2 ori, Vector2 dir)
    {
        public Vector2 origin = ori;
        public Vector2 direction = dir;
    }
    public class CollisionDetections
    {

        public static bool PointVRect(Point p, RectangleF r)
        {
            return (p.X >= r.X && p.Y >= r.Y && p.X < r.Right && p.Y < r.Top);
        }

        public static bool RectVRect(RectangleF r1, RectangleF r2)
        {
            //luckily this exists
            return (r1.IntersectsWith(r2));
        }

        public static bool RayVRect(Ray ray, RectangleF target,
            out Vector2 contact_point, out Vector2 contact_normal, out float t_hit_near)
        {
            
            //t_near    = intersection point
            //t_far     = penetration point

            Vector2 t_near = (new Vector2(target.X, target.Y) - ray.origin) / ray.direction;
            Vector2 t_far = ((new Vector2(target.X, target.Y) + new Vector2(target.Size.Width, target.Size.Height)) - ray.origin) / ray.direction;

            contact_point = new Vector2(float.NaN, float.NaN);
            contact_normal = new Vector2(float.NaN, float.NaN);
            t_hit_near = float.NaN;

            if (float.IsNaN(t_near.X) || float.IsNaN(t_near.Y)) { return false; }
            if (float.IsNaN(t_far.X) || float.IsNaN(t_far.Y)) { return false; }

            //Renderer.DrawDebugPoint(new DebugDrawing(DebugDrawingType.Rect, target, Color.Red));
            //Renderer.DrawDebugPoint(new DebugDrawing(DebugDrawingType.Line, new PointF(ray.origin), new PointF(target.X, target.Y), Color.Blue));

            if (t_near.X > t_far.X)
            { EngineFunctions.swap(ref t_near.X, ref t_far.X); }

            if (t_near.Y > t_far.Y)
            { EngineFunctions.swap(ref t_near.Y, ref t_far.Y); }


            if(t_near.X > t_far.Y || t_near.Y > t_far.X)
            {  return false; }

                  t_hit_near = Math.Max(t_near.X, t_near.Y);
            float t_hit_far = Math.Min(t_far.X, t_far.Y);

            if(t_hit_near < -float.Epsilon)
            { return false; }

            contact_point = ray.origin + t_hit_near * ray.direction;

            if (t_near.X > t_near.Y)
            {
                if (ray.direction.X < 0)
                {
                    contact_normal.X = 1;
                    contact_normal.Y = 0;
                }
                else
                {
                    contact_normal.X = -1;
                    contact_normal.Y = 0;
                }
            }
            else
            {
                if (ray.direction.Y < 0)
                {
                    contact_normal.X = 0;
                    contact_normal.Y = 1;
                }
                else
                {
                    contact_normal.X = 0;
                    contact_normal.Y = -1;
                }
            }

            return true;
        }

        public static bool DynamicRectVRect(RectangleF In, Vector2 Invel, RectangleF target, float elapsedtime,
            out Vector2 contact_point, out Vector2 contact_normal, out float contact_time)
        {
            contact_point = new Vector2(float.NaN, float.NaN);
            contact_normal = new Vector2(float.NaN, float.NaN);
            contact_time = float.NaN;

            if (Invel.X == 0 && Invel.Y == 0)
            {
                return false;
            }

            RectangleF target_expanded = new RectangleF();
            target_expanded.X = target.X - In.Size.Width / 2;
            target_expanded.Y = target.Y - In.Size.Height / 2;
            target_expanded.Size = target.Size + In.Size;

            //Renderer.DrawDebugPoint(new DebugDrawing(DebugDrawingType.Point, new PointF(new Vector2(In.X + In.Width / 2, In.Y + In.Height / 2)), new SizeF(16, 16), Color.Yellow));
            
            if (RayVRect(new Ray(new Vector2(In.X + In.Size.Width / 2, In.Y + In.Size.Height / 2), Invel * elapsedtime), target_expanded, out contact_point, out contact_normal, out contact_time))
            {
                if (contact_time <= 1.0f)
                {
                    return true;
                }
            }
            return false;
        }
    }
}