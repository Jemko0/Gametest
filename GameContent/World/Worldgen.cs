using Gametest;
using System.Numerics;
using Engine.Data;
namespace Gametest.GameContent.World
{
    public class Worldgen
    {
        public static short size = 16;
        public static int scale;
        public static int tilesize = 64;
        public static FastNoiseLite? noise;
        public static List<EngineStructs.WTile> tiles = new List<EngineStructs.WTile>();
        public static void Generate()
        {
            for (short x = 0; x < size; x++)
            {
                for (short y = 0; y < size; y++)
                {
                    if(noise != null)
                    {
                        if(true)
                        {
                            tiles.Add(new EngineStructs.WTile((short)(x * tilesize), (short)(y * tilesize)));
                        }
                    }
                    else
                    {
                        Setupnoise();
                    }
                }
            }
            FinishGen();
        }

        public static void Setupnoise()
        {
            noise = new FastNoiseLite();
            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noise.SetFrequency(0.001f);
            Generate();
        }

        public static void FinishGen()
        {
            foreach(var tile in tiles)
            {
                GameClient.RegisterObject(new Object.Entity.ETile("base", tile.x, tile.y));
            }
        }
    }
}