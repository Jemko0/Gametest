using Engine.Data;
using System.Data;
using System.Numerics;
using System.Runtime.Versioning;

namespace Game
{
    public class ID
    {
        public partial class TileID
        {
            public struct TileData()
            {
                public byte hardness;
                public byte rarity;
                public byte pickpower;
                public string dropItemID;
            }
        }


        public partial class ItemID
        {
            public struct ItemData()
            {
                public string id;
                public string name;
                public string tooltip;
                public Image sprite;
            }

            public static ItemData GetItem(string ItemID)
            {
                ItemData i = new ItemData();

                i.id = ItemID;

                switch (ItemID)
                {
                    default:
                        return i;

                    case "base":
                        i.name = "base";
                        i.tooltip = "base";
                        i.sprite = null;
                        return i;
                }
            }
        }
        public partial class EntityID
        {
            public struct EntityDescription()
            { 
                public string Name;
                public bool InitActive;
                public Vector2 HSize;
                public Image Sprite;
            }

            public static EntityDescription GetEntity(string EntityID)
            {
                EntityDescription e = new EntityDescription();
                switch (EntityID)
                {
                    default:
                        return e;

                    case "base":
                        e.Name = "BaseEntity";
                        e.Sprite = null;
                        e.HSize = new Vector2(100, 40);
                        return e;

                    case "wall":
                        e.Name = "Wall";
                        e.Sprite = null;
                        e.HSize = new Vector2(40, 350);
                        return e;

                    case "player":
                        e.Name = "Player";
                        e.Sprite = Gametest.Properties.Resources.Entity;
                        e.HSize = new Vector2(25, 50);
                        return e;
                }
            }
        }

        //next class
    }
}