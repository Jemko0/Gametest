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

        public override void FixedTick(object? sender, EventArgs e)
        {
            base.FixedTick(sender, e);

            //default move for entities
            Position.X += velocity.X;
            Position.Y += velocity.Y;
        }

        public virtual EngineStructs.ECollisionResult CheckCollisions()
        {
            EngineStructs.ECollisionResult result = new EngineStructs.ECollisionResult();

            for (int i = 0; i < Form1.objs.Count; i++)
            {
                EObject obj = Form1.objs.ElementAt(i).Value;
                EEntity e = obj as EEntity;

                    if(e.active && e != this)
                    {
                        if (e.Position.X < Position.X + EDescription.HSize.X &&
                            e.Position.X + e.EDescription.HSize.X > Position.X &&

                            e.Position.Y < Position.Y + EDescription.HSize.Y &&
                            e.Position.Y + e.EDescription.HSize.Y > Position.Y
                            )
                        {
                            //Collision Detected
                            result.collision = true;
                            result.hitobject = obj;

                            return result;
                        }
                    }
            }
            return result;
        }
    }
}