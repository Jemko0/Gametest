using System.Data;
using System.Numerics;
using System.Runtime.Versioning;

namespace Game
{
    public class ID
    {
        
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
                    case "base":
                        e.Name = "BaseEntity";
                        e.Sprite = null;
                        e.HSize = new Vector2(350, 40);
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
                return e;
            }
        }

        //next class
    }
}