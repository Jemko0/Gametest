using Gametest;
using Game.Global;
using System.Numerics;
using Engine;

namespace Object.Entity
{
    public class EEntity : EObject
    {
        public bool active;
        public Game.ID.EntityID.EntityDescription EDescription;
        public Vector2 Position;
        public Vector2 velocity;
        public bool collidable = true;
        public EEntity()
        {
            active = true;
            Rendering = true;
        }

        public void InitializeEntity(Vector2 Pos, string EntityID)
        {
            EDescription = Game.ID.EntityID.GetEntity(EntityID);
            Position = Pos;
        }

        public override void Tick(float delta)
        {
            base.Tick(delta);
            Position.X += velocity.X * delta;
            Position.Y += velocity.Y * delta;
        }
        public virtual EngineStructs.ECollisionResult CheckCollisions()
        {
            EngineStructs.ECollisionResult e = Collision();
            return e;
        }

        public EngineStructs.ECollisionResult Collision()
        {
            EngineStructs.ECollisionResult cr = new EngineStructs.ECollisionResult();
            for (int i = 0; i < Form1.objs.Count; i++)
            {
                EObject obj = Form1.objs.ElementAt(i).Value;
                {
                    EEntity e = obj as EEntity;
                    if (e != this && e.collidable)
                    {
                        if (Position.Y + EDescription.HSize.Y > e.Position.Y
                            && Position.Y < e.Position.Y + e.EDescription.HSize.Y
                            && Position.X + EDescription.HSize.X > e.Position.X
                            && Position.X < e.Position.X + e.EDescription.HSize.X
                            )
                        {
                            cr.collision = true;
                            return cr;
                        }
                        
                    }
                }
            }
            return cr;
        }
    }
}