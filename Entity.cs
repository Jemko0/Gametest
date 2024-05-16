using Object;
using System.Numerics;

namespace Entity
{
    public class EEntity : EObject
    {
        public struct EntityInfo
        {
            public Image Sprite;
            public Vector2 Size;
        }

        public EntityInfo sInfo;
        public Vector2 Position;

        public EEntity()
        {
        
        }

    }
}