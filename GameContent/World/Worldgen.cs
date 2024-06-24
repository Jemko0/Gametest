
using Engine.Data;
using Object.Entity;
using System.Numerics;
namespace Gametest.GameContent.World
{
    public class Worldgen
    {
        public static short size = 128;
        public static int tilesize = 32;
        public static FastNoiseLite? noise;
        public static void Generate()
        {
            Setupnoise();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if(noise != null)
                    {
                        if(noise.GetNoise(x * tilesize, y * tilesize) > 0f)
                        {
                            if (!GameClient.worldtiles.ContainsKey(new EngineStructs.IntVector2(x * tilesize, y * tilesize + 500)))
                            {
                                GameClient.worldtiles.Add(new EngineStructs.IntVector2(x * tilesize, y * tilesize + 500), "t_dirt");
                            }
                        }
                    }
                }
            }
        }

        public static void MineTile(Vector2 WorldPos)
        {
            string val;
            GameClient.worldtiles.TryGetValue(new EngineStructs.IntVector2(WorldPos), out val);
            System.Diagnostics.Debug.WriteLine(val);
            GameClient.worldtiles.Remove(new EngineStructs.IntVector2((int)WorldPos.X, (int)WorldPos.Y + 52));
        }

        public static void Setupnoise()
        {
            noise = new FastNoiseLite();
            noise.SetSeed(69);
            noise.SetFractalType(FastNoiseLite.FractalType.FBm);
            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noise.SetFrequency(0.001f);
        }
    }
}