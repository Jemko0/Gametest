
using Engine.Data;
using Game;

namespace Object.Entity
{
    public class Item : EEntity
    {
        public string itemid;
        public ID.ItemID.ItemData itemdata;

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

            EngineStructs.ECollisionResult collisionResult = CheckCollisions();
            if (!collisionResult.collision)
            {
                velocity.Y += Game.GameProperties.gravity * odelta;
                return;
            }
            else
            {
                velocity.Y = 0;
            }
        }

        public virtual void Use()
        {

        }
    }
}
