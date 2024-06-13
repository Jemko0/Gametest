

using Game;
using Gametest.GameContent.World;
using Object.Entity;

namespace Gametest
{
    public class Traces
    {
        public static bool isTraversible(int x, int y)
        {
            if (!GameClient.cam.PosInCamBounds(new System.Numerics.Vector2(x, y)))
            {
                return true;
            }

            if (ETile.GetTileAt(new Engine.Data.EngineStructs.IntVector2(x, y)) != null)
            {
                return true;
            }
            int lx = x % Worldgen.tilesize;
            int ly = y % Worldgen.tilesize;

            var tile = ID.TileID.GetTile(ETile.GetTileAt(new Engine.Data.EngineStructs.IntVector2(x, y)));
            return !tile.solid && tile.valid;
        }
    }
}
