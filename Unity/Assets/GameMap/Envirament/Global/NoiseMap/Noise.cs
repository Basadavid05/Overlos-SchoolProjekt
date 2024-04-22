using UnityEngine;
using System.Collections;

public static class Noise 
{
    public static float[,] GeneratiNoiiseMap(int mapWith, int Mapheight,float scale)
    {
        float[,] noisemap = new float[mapWith, Mapheight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int x = 0; x < Mapheight; x++)
        {
            for (int y = 0; y < mapWith; y++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue=Mathf.PerlinNoise(sampleX, sampleY);
                noisemap[x,y]=perlinValue;
            }
        }
        return noisemap;
    }
}
