using Engine.Data;
using Object.Entity;
using Object;
using Gametest;
using Engine.Camera;
using Game;
using System.Runtime.CompilerServices;
using System.Numerics;
using Gametest.Properties;

namespace Engine
{
    public class Renderer
    {
        public static GameClient g;
        public static PaintEventArgs e;
        public static int ro;
        public static Dictionary<string, Image> SPRTCACHE_TILES = new Dictionary<string, Image>();
        public static Dictionary<int, Image> SPRTCACHE_ENTITY = new Dictionary<int, Image>();
        public static List<Vector2> DBG_BUFFER = new List<Vector2>();
        public Renderer()
        {
        
        }
        public static int Render(object sender, PaintEventArgs ee, GameClient gcl)
        {
            g = gcl;
            e = ee;
            int ro = 0;
            //actual render stuff
            TilePass();
            ObjectPass();
            DebugPass();
            return ro;
        }

        public static void DrawDebugPoint(Vector2 worldpos)
        {
            DBG_BUFFER.Add(worldpos);
        }

        private static void DebugPass()
        {
            foreach(var draw in DBG_BUFFER.ToList())
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(draw, GameClient.cam, g)), new SizeF(10, 10)));
                DBG_BUFFER.Remove(draw);
            }
        }


        private static void ObjectPass()
        {
            for (int i = 0; i < GameClient.objs.Count; i++)
            {
                EObject obj = GameClient.objs.ElementAt(i).Value;

                if (obj.Rendering && obj.ticking)
                {
                    EEntity en = obj as EEntity;
                    if (en != null)
                    {
                        ro++;
                        if (en.EDescription.Sprite != null)
                        {
                            e.Graphics.DrawImage(en.EDescription.Sprite, new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam, g)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam, g)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam, g)), new SizeF(10, 10)));
                        }
                    }
                }
            }
        }

        private static void TilePass()
        {
            ro++;
            foreach (var tile in GameClient.worldtiles)
            {
                if(GameClient.cam.PosInCamBounds(new System.Numerics.Vector2(tile.Key.x, tile.Key.y)))
                {
                    if(SPRTCACHE_TILES.ContainsKey(tile.Value))
                    {
                        e.Graphics.DrawImage(SPRTCACHE_TILES[tile.Value], new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(new System.Numerics.Vector2(tile.Key.x, tile.Key.y), GameClient.cam, g)), new SizeF(32, 32)));
                    }
                    else
                    {
                        SPRTCACHE_TILES.Add(tile.Value, ID.TileID.GetTile(tile.Value).sprite);
                    }
                }
            }
        }
    }
}
