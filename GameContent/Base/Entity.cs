using Object;
using System.Numerics;

namespace Entity
{
    public class EEntity : EObject
    {

        public Game.ID.ID.EntityID.EntityDescription EDescription;
        public Vector2 Position;

        public EEntity(Vector2 Pos, string EntityID)
        {
            Rendering = true;
            EDescription = Game.ID.ID.EntityID.GetEntity(EntityID);
            Position = Pos;
        }

    }
}