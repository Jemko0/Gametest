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
            //Position.X += velocity.X;
            //Position.Y += velocity.Y;
        }

        public virtual EngineStructs.ECollisionResult CheckCollisions()
        {
            
            EngineStructs.ECollisionResult result = new EngineStructs.ECollisionResult();

            for (int i = 0; i < Form1.objs.Count; i++)
            {
                EObject obj = Form1.objs.ElementAt(i).Value;
                if (obj != this)
                {
                EEntity e = obj as EEntity;

                
                result = SweptAABB(e);
                    //Response
                    Form1.debugtxt = "NX=" + (result.normalx).ToString() + "   NY=" + (result.normaly).ToString() + "  CT=" + (result.collisiontime).ToString();


                Position.X += velocity.X * result.collisiontime;
                Position.Y += velocity.Y * result.collisiontime;
                float remainingtime = 1.0f - result.collisiontime;

                //SLIDE
                float dot = (velocity.X * result.normaly + velocity.Y * result.normalx) * remainingtime;
                velocity.X += dot * result.normaly;
                velocity.Y += dot * result.normalx;
                }
            }


            return result;
        }

        public virtual EngineStructs.ECollisionResult SweptAABB(EEntity e)
        {
            EngineStructs.ECollisionResult c = new EngineStructs.ECollisionResult();

            float xInvEntry, yInvEntry;
            float xInvExit, yInvExit;
            float xEntry, yEntry;
            float xExit, yExit;

            if (velocity.X > 0.0f)
            {
                xInvEntry = e.Position.X - (Position.X + EDescription.HSize.X);
                xInvExit = (e.Position.X + e.EDescription.HSize.X) - Position.X;
            }
            else
            {
                xInvEntry = (e.Position.X + e.EDescription.HSize.X) - Position.X;
                xInvExit = e.Position.X - (Position.X + EDescription.HSize.X);
            }

            //up down
            if (velocity.Y > 0.0f)
            {
                yInvEntry = e.Position.Y - (Position.Y + EDescription.HSize.Y);
                yInvExit = (e.Position.Y + e.EDescription.HSize.Y) - Position.Y;
            }
            else
            {
                yInvEntry = (e.Position.Y + e.EDescription.HSize.Y) - Position.Y;
                yInvExit = e.Position.Y - (Position.Y + EDescription.HSize.Y);
            }

            //collision time, if is for div/0
            if (velocity.X == 0.0f)
            {
                xEntry = float.NegativeInfinity;
                xExit = float.PositiveInfinity;
            }
            else
            {
                xEntry = xInvEntry / velocity.X;
                xExit = xInvExit / velocity.X;
            }

            if (velocity.Y == 0.0f)
            {
                yEntry = float.NegativeInfinity;
                yExit = float.PositiveInfinity;
            }
            else
            {
                yEntry = yInvEntry / velocity.Y;
                yExit = yInvExit / velocity.Y;
            }

            //earliest/latest collision time
            float entryTime = Math.Max(xEntry, yEntry); //when did the first collision occur
            float exitTime = Math.Min(xExit, yExit); //when did the object exit from the other side


            //if no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f)
            {
                c.normalx = 0.0f;
                c.normaly = 0.0f;
                c.collisiontime = 1.0f;
                c.hitobject = null;
                return c;
            }

            //if collision
            else
            {
                //calc normal
                if (xEntry > yEntry)
                {
                    if(xInvEntry < 0.0f)
                    {
                        c.normalx = 1.0f;
                        c.normaly = 0.0f;
                    }
                    else
                    {
                        c.normalx = -1.0f;
                        c.normaly = 0.0f;
                    }
                }
                else
                {
                    if(yInvEntry < 0.0f)
                    {
                        c.normalx = 0.0f;
                        c.normaly = 1.0f;
                    }
                    else
                    {
                        c.normalx = 0.0f;
                        c.normaly = -1.0f;
                    }
                }
                                c.collisiontime = entryTime;
                c.hitobject = e;
                return c;
            }
        }
    }
}