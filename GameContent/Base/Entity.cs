using Gametest;
using System.Numerics;
using Engine.Data;
using Engine;
using System;
using Gametest.GameContent.World;
using Engine.Camera;

namespace Object.Entity
{
    /// <summary>
    /// Entity is an EObject that gets rendered to the screen, its used for most game objects
    /// </summary>
    public class EEntity : EObject
    {
        public bool active;
        public EEntity parent;
        public Game.ID.EntityID.EntityDescription EDescription;
        public Vector2 Position;
        public Vector2 oldPosition;
        public Vector2 velocity;
        public bool collidable = true;

        public EEntity()
        {
            Init();
            return;
        }

        public override void Init()
        {
            base.Init();
            active = true;
            Rendering = true;
            ticking = true;
        }

        public static void SetPosition(Vector2 newpos, EEntity e)
        {
            e.Position = newpos;
        }

        public RectangleF GetRect()
        {
            return new RectangleF(Position.X, Position.Y, EDescription.HSize.X, EDescription.HSize.Y);
        }

        public void InitializeEntity(Vector2 Pos, string EntityID)
        {
            EDescription = Game.ID.EntityID.GetEntity(EntityID);
            Position = Pos;
            //Renderer.SPRTCACHE_ENTITY.Add(OBJID, EDescription.Sprite);
        }

        public override void Tick(float delta)
        {
            base.Tick(delta);
            active = GameClient.cam.PosInCamBounds(Position);
            ETickMovement();
        }

        public virtual void ETickMovement()
        {
            if(parent != null)
            {
                Position = parent.Position;
            }
            else
            {
                Position.X += velocity.X * odelta;
                Position.Y += velocity.Y * odelta;
            }
            
        }
        public virtual EngineStructs.ECollisionResult CheckCollisions()
        {
            EngineStructs.ECollisionResult e = Collision();
            return e;
        }

        public EngineStructs.ECollisionResult Collision()
        {
            EngineStructs.ECollisionResult cr = new EngineStructs.ECollisionResult();
            cr.collided = false;
            cr.time = 0;

            //entities
            //for (int i = 0; i < GameClient.objs.Count; i++)
            //{
            //    EObject obj = GameClient.objs.ElementAt(i).Value;
            //    {
            //        EEntity e = obj as EEntity;
            //        if (e != this && e.collidable)
            //        {
            //            if (Position.Y + EDescription.HSize.Y > e.Position.Y
            //                && Position.Y < e.Position.Y + e.EDescription.HSize.Y
            //                && Position.X + EDescription.HSize.X > e.Position.X
            //                && Position.X < e.Position.X + e.EDescription.HSize.X
            //                )
            //            {
            //                cr.hitobject = e;
            //                cr.collision = true;
            //                return cr;
            //            }
            //        }
            //    }
            //}

            // tile offsets if necessary

            //tile.Key.y - Worldgen.tilesize / 3f
            //tile.Key.y + 16 - Worldgen.tilesize / 3f

            //tile.Key.x - Worldgen.tilesize / 3f
            //tile.Key.x + 32 - Worldgen.tilesize / 3f

            //tile collision
            Vector2 cp, cn;
            float ct = 0;

            foreach (var tile in GameClient.worldtiles)
            {
                if(GameClient.cam.PosInCamBounds(new Vector2(tile.Key.x, tile.Key.y)))
                {
                    if (CollisionDetections.DynamicRectVRect(new RectangleF(new PointF(Position.X, Position.Y), new SizeF(EDescription.HSize)), velocity, new RectangleF(new PointF(tile.Key.x, tile.Key.y), new SizeF(16, 16)), odelta, out cp, out cn, out ct) && ct < 1.0f)
                    {
                        cr.collided = true;
                        cr.normal = cn;
                        cr.location = cp;
                        cr.time = ct;
                        return cr;
                    }
                }
               
            }
            return cr;
        }
    }
}