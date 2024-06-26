
using Engine;
using Engine.Data;
using Game;
using Gametest;
using System.Numerics;

namespace Object.Entity
{
    public class Item : EEntity
    {
        public string itemid;
        public bool pickedup = false;
        public ID.ItemID.ItemData itemdata;

        public Item(string _id, ID.ItemID.ItemData data, Vector2 pos)
        {
            if (pos != Vector2.Zero)
            {
                Position = (Vector2)pos;
            }

            itemid = _id;
            itemdata = data;

            SetDefaults();
        }

        public Item(string _id, ID.ItemID.ItemData data)
        {
            itemid = _id;
            itemdata = data;
            
            SetDefaults();
        }

        public virtual void SetDefaults()
        {
            collidable = false;
            EDescription.Sprite = itemdata.sprite;
            EDescription.HSize.X = 42;
            EDescription.HSize.Y = 42;
        }

        public override void Tick(float delta)
        {
            base.Tick(delta);

            if (!pickedup)
            {
                if (CollisionDetections.RectVRect(GetRect(), GameClient.player.GetRect()))
                {
                    GameClient.player.inv.AddItem(itemid, 1, "nulldata");
                    GameClient.DestroyObject(OBJID);
                    return;
                }

                velocity.X /= 1 + odelta * 6;
                EngineStructs.ECollisionResult collisionResult = CheckCollisions();
                if (!collisionResult.collided)
                {
                    velocity.Y += Game.GameProperties.gravity * odelta;
                    return;
                }
                else
                {
                    velocity.Y = -1;
                }
            } 
        }

        public virtual void Use()
        {

        }
    }
}
