
using Game;

namespace Object.Entity
{
    public class Item : EEntity
    {
        public string itemid;

        public Item(string _id)
        {
            itemid = _id;
            SetDefaults();
        }

        public virtual void SetDefaults()
        {

            collidable = false;
            EDescription.Sprite = null;
            EDescription.HSize.X = 16;
            EDescription.HSize.Y = 16;
        }

        public virtual void Use()
        {

        }
    }
}
