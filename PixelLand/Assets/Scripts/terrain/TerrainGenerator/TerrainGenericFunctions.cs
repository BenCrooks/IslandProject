using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenericFunctions : MonoBehaviour
{
    public float Perlinise(float nx, float ny)
    {
        return (Mathf.PerlinNoise(1 * nx, 1 * ny)
            - Mathf.PerlinNoise(2f * nx, 2f * ny) / 2
            + Mathf.PerlinNoise(4f * nx, 4f * ny) / 3
            - Mathf.PerlinNoise(8f * nx, 8f * ny) / 5
            + Mathf.PerlinNoise(16 * nx, 16 * ny) / 8
            - Mathf.PerlinNoise(32 * nx, 32 * ny) / 12
            + Mathf.PerlinNoise(64 * nx, 64 * ny) / 18
            - Mathf.PerlinNoise(128 * nx, 128 * ny) / 40
            + Mathf.PerlinNoise(256 * nx, 256 * ny) / 80
            + 2 * Mathf.PerlinNoise(0.0001f * nx * 3500, 0.0001f * ny * 3500)
            );
    }

    public bool Checker(int x, int z)
    {
        if (Mathf.Abs(x) % 2 == 0 && Mathf.Abs(z) % 2 == 0 || Mathf.Abs(x) % 2 == 1 && Mathf.Abs(z) % 2 == 1)
        {
            return true;
        }
        return false;
    }

}
