using System.Data;
using System.Numerics;

namespace Game.ID
{
    public class ID
    {
        public partial class EntityID
        {
            public struct EntityDescription()
            { 
                public string Name;
                public Vector2 HSize;
                public Bitmap Sprite;
            }

            public static EntityDescription GetEntity(string EntityID)
            {
                EntityDescription e = new EntityDescription();
                switch (EntityID)
                {
                    case "Base":
                        e.Name = "BaseEntity";
                        e.HSize = new Vector2(20, 20);
                        return e;
                }
                return e;
            }
        }

        //next class
    }
}