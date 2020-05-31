using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTiles : MonoBehaviour
{
    public Material subMat1, subMat2, subMat3;

    public FinalTerrain ft;

    public void Initialize(int terrainsize, Texture2D wavesTexture1, Texture2D wavesTexture2, Texture2D wavesTexture3)
    {
        subMat1.mainTexture = wavesTexture1;
        subMat2.mainTexture = wavesTexture2;
        subMat3.mainTexture = wavesTexture3;
    }

    public void UpdateWaves(int tempColNo, int x,int z, Texture2D wavesTexture1, Texture2D wavesTexture2, Texture2D wavesTexture3)
    {
        //____________add waves
        if (tempColNo <= 1)
        {
            wavesTexture1.SetPixel(x, z, Color.white);
        }
        else
        if (tempColNo == 2)
        {
            wavesTexture2.SetPixel(x, z, Color.white);
        }
        else
        if (tempColNo == 3)
        {
            wavesTexture3.SetPixel(x, z, Color.white);
        }
    }

    public void NoWaves(int x,int z, Texture2D wavesTexture1, Texture2D wavesTexture2, Texture2D wavesTexture3)
    {
        Color colorA = new Color(0, 0, 0, 0);
        wavesTexture1.SetPixel(x, z, colorA);
        wavesTexture2.SetPixel(x, z, colorA);
        wavesTexture3.SetPixel(x, z, colorA);
    }

    public void ApplyWaves(Texture2D wavesTexture1, Texture2D wavesTexture2, Texture2D wavesTexture3)
    {
        wavesTexture1.filterMode = FilterMode.Point;
        wavesTexture1.Apply();
        wavesTexture2.filterMode = FilterMode.Point;
        wavesTexture2.Apply();
        wavesTexture3.filterMode = FilterMode.Point;
        wavesTexture3.Apply();
    }
}
