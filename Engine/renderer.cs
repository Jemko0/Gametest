using Engine.Data;
using Object.Entity;
using Object;
using Gametest;
using Engine.Camera;
using Game;
using System.Runtime.CompilerServices;
using System.Numerics;
using Gametest.Properties;
using Gametest.GameContent.World;

namespace Engine
{
    public class Renderer
    {
        public static GameClient g;
        public static PaintEventArgs e;
        public static int ro;
        public static Dictionary<string, Image> SPRTCACHE_TILES = new Dictionary<string, Image>();
        public static Dictionary<EngineStructs.IntVector2, string> TILEBUFFER = new Dictionary<EngineStructs.IntVector2, string>();
        public static Dictionary<int, Image> SPRTCACHE_ENTITY = new Dictionary<int, Image>();
        public static List<DebugDrawing> DBG_BUFFER = new List<DebugDrawing>();
        public Renderer()
        {
        
        }
        public static int Render(object sender, PaintEventArgs ee, GameClient gcl)
        {
            g = gcl;
            e = ee;
            int ro = 0;
            //actual render stuff
            TileBounds();
            TilePass();
            ObjectPass();
            DebugPass();
            UIPass();
            return ro;
        }

        public static void DrawDebugPoint(DebugDrawing drawing)
        {
            if(!DBG_BUFFER.Contains(drawing))
            {
                DBG_BUFFER.Add(drawing);
            }  
        }

        private static void DebugPass()
        {
            if(DBG_BUFFER.Count > 0)
            {
                foreach(var draw in DBG_BUFFER.ToList())
                {
                    switch(draw.drawtype)
                    {
                        default:
                            return;

                        case DebugDrawingType.Point:
                            e.Graphics.FillEllipse(new SolidBrush(draw.color), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(new Vector2(draw.pos.X, draw.pos.Y), GameClient.cam)), new SizeF(10, 10)));

                            break;

                        case DebugDrawingType.Line:
                            e.Graphics.DrawLine(new Pen(draw.color), new PointF(EngineFunctions.GetRenderTranslation(new Vector2(draw.p1.X, draw.p1.Y), GameClient.cam)), new PointF(EngineFunctions.GetRenderTranslation(new Vector2(draw.p2.X, draw.p2.Y), GameClient.cam)));

                            break;

                        case DebugDrawingType.Rect:
                            e.Graphics.FillRectangle(new SolidBrush(draw.color), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(new Vector2(draw.rect.X, draw.rect.Y), GameClient.cam)), new SizeF(draw.rect.Size)));

                            break;
                    }
                    DBG_BUFFER.Remove(draw);
                }
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
                            //e.Graphics.DrawImage(en.EDescription.Sprite, en.GetRect());
                            e.Graphics.DrawImage(en.EDescription.Sprite, new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam)), new SizeF(en.EDescription.HSize.X, en.EDescription.HSize.Y)));
                            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(en.Position, GameClient.cam)), new SizeF(10, 10)));
                        }
                    }
                }
            }
        }

        private static void TileBounds()
        {
            TILEBUFFER.Clear();
            foreach (var tile in GameClient.worldtiles)
            {
                if (GameClient.cam.PosInCamBounds(new System.Numerics.Vector2(tile.Key.x, tile.Key.y)))
                {
                    TILEBUFFER.Add(new EngineStructs.IntVector2(tile.Key.x, tile.Key.y), tile.Value);
                }
            }
        }

        private static void TilePass()
        {
            ro++;
            foreach (var tile in TILEBUFFER)
            {
                if(SPRTCACHE_TILES.ContainsKey(tile.Value))
                {
                        e.Graphics.DrawImage(SPRTCACHE_TILES[tile.Value], new RectangleF(new PointF(EngineFunctions.GetRenderTranslation(new System.Numerics.Vector2((float)(tile.Key.x - Worldgen.tilesize / 3f), (float)(tile.Key.y - Worldgen.tilesize / 3f)), GameClient.cam)), new SizeF(32, 32)));
                }
                else
                {
                    SPRTCACHE_TILES.Add(tile.Value, ID.TileID.GetTile(tile.Value).sprite);
                }
            }
        }

        private static void UIPass()
        {
            UInv();
        }

        private static void UInv()
        {
            var items = GameClient.player.inv.items;
            
            for(int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                e.Graphics.DrawImage(ID.ItemID.GetItem(item.id).sprite, 32 + (i * 64), 32, 48, 48);
            }
        }
    }
}
