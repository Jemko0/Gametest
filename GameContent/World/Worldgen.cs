using Gametest;
using System.Numerics;
using Engine.Data;
namespace Gametest.GameContent.World
{
    public class Worldgen
    {
        public static short size = 500;
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
                            if (!GameClient.worldtiles.ContainsKey(new EngineStructs.IntVector2(x * tilesize, y * tilesize)))
                            {
                                GameClient.worldtiles.Add(new EngineStructs.IntVector2(x * tilesize, y * tilesize), "t_dirt");
                            }
                        }
                    }
                }
            }
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