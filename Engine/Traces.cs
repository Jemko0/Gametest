using Engine.Data;
using Game;
using Gametest.GameContent.World;
using Object.Entity;
using Engine;
using System.Numerics;

namespace Gametest
{
    public struct THitResult
    {
        public bool Hit;
        public Vector2 normal;
        public ID.TileID.TileData tile;
        public Vector2 StartLoc;
        public Vector2 EndLoc;
        public Vector2 HitLocation;
        
    }
    public class Traces
    {
        /// <summary>
        /// TILE ONLY
        /// </summary>
        /// <returns>true if the TILE at the current location is solid</returns>
        public static TraverseResult isTraversible(int x, int y)
        {
            TraverseResult tr = new TraverseResult();

            tr.tile = ID.TileID.GetTile(ETile.GetTileAt(new Engine.Data.EngineStructs.IntVector2(x, y)));
            tr.traversable = !tr.tile.solid;
            return tr;
        }

        public static bool Inrange<T>(T value, T min, T max) where T : System.IComparable<T>
        {
            return (value.CompareTo(min) > 0) && (value.CompareTo(max) < 0);
        }

        public static TraverseResult isTraversible(Vector2 pos)
        {
            return isTraversible(EngineFunctions.TileSnap((int)pos.X), EngineFunctions.TileSnap((int)pos.Y));
        }

        public static Vector2 GetNormalFromTileLocals(float tilelocalx, float tilelocaly)
        {
            Vector2 normal = new Vector2();

            tilelocalx = Math.Abs(tilelocalx);
            tilelocaly = Math.Abs(tilelocaly);
            
            if(tilelocalx > 24 && tilelocalx > tilelocaly)
            {
                normal.X = 1;
                normal.Y = 0;
            }
            if(tilelocaly < 8)
            {
                normal.X = 0;
                normal.Y = 1;
            }

            if (tilelocalx < 8 && tilelocalx < tilelocaly)
            {
                normal.X = -1;
                normal.Y = 0;
            }
            if((tilelocaly > 24))
            {
                normal.X = 0;
                normal.Y = -1;
            }

            return normal;
        }

        public static THitResult TLineTrace_naive(Vector2 startpos,  Vector2 dir, float raylength, bool dbg = false)
        {
            int _stepsz = 8;
            float step = 1;

            THitResult hit = new THitResult();

            hit.StartLoc = startpos;
            hit.EndLoc = startpos + Vector2.Normalize(dir) * raylength * _stepsz;

            while (step < raylength)
            {
                Vector2 hitpos = startpos + (Vector2.Normalize(dir) * (_stepsz * step));

                TraverseResult tresult = isTraversible(EngineFunctions.TileSnap(hitpos));

                if (!tresult.traversable)
                {
                    hit.Hit = true;
                    hit.tile = tresult.tile;
                    hit.HitLocation = hitpos;

                    float lx = (hitpos.X - 16) % Worldgen.tilesize;
                    float ly = (hitpos.Y - 16) % Worldgen.tilesize;

                    hit.normal = GetNormalFromTileLocals(lx, ly);

                    //log for debug tings

                    break;
                }
                
#if DEBUG
                if (dbg)
                {

                }
#endif
                step++;
            }
            return hit;
        }
    }
}
