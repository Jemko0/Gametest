using Object.Entity;
using Game;

namespace Object.Entity
{
    public class ETile : EEntity
    {
        public ID.TileID.TileData tiledata;
        public ETile(string id)
        {
            SetTileDefaults(id);
        }

        public ETile(string id, int x, int y)
        {
            SetTileDefaults(id);
            Position = new System.Numerics.Vector2((int)x, (int)y);
        }

        public void SetTileDefaults(string _id)
        {
            tiledata = ID.TileID.GetTile(_id);
            Rendering = true;
            ticking = false;
            EDescription.HSize = new System.Numerics.Vector2(32, 32);
            EDescription.Sprite = tiledata.sprite;
        }
    }
}
