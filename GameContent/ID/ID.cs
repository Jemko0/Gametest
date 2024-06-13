using Gametest.Properties;
using Object.Entity;
using System.Numerics;

namespace Game
{
    public class ID
    {
        public partial class TileID
        {
            public struct TileData()
            {
                public bool solid;
                public byte hardness;
                public byte rarity;
                public byte pickpower;
                public string dropItemID;
                public bool valid = true;
                public Image sprite;
            }

            public static TileData GetTile(string TileID)
            {
                TileData t = new TileData();
                switch (TileID)
                {
                    default:
                        t.valid = false;
                        return t;
                    case "t_dirt":
                        t.solid = true;
                        t.dropItemID = "i_dirt";
                        t.sprite = Resources.tiledirt;
                        t.hardness = 15;
                        t.pickpower = 1;
                        return t;
                }
            }
        }


        public partial class ItemID
        {
            public struct InvItemData()
            {
                public string id;
                public int amount;
                public string add_data;
            }

            public struct ItemData()
            {
                public string id;
                public Item _class;
                public string name;
                public byte pickpower;
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

                    case "basepick":
                        i.name = "basepick";
                        i.tooltip = "base";
                        i.sprite = Resources.itempickbase;
                        i._class = new Item("basepick", i);
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