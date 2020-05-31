using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTerrain : MonoBehaviour
{
    [Header("Island size and shape")]
    public int terrainsize;
    public float detail;
    public float heightScale;
    public float sink;
    public float sinMultiplyer;
    [Header("Island height")]
    public float StoneLevel;
    public float TreeLevel;
    public float GrassLevel;
    public float SandLevel;
    [Header("Island Color Palette")]
    public Color[] RockColors;
    public Color[]  SandColors, TreeColors, GrassColors, WaterColors;

    private float y;
    private float yPure;
    private float newSeedy;
    private float tempStartHeight;

    [Header("Scripts")]
    public Shader shade;
    public TerrainGenericFunctions func;
    private Material globalMat;
    private Texture2D texture;

    private int x = 0;
    private int xLocal;
    private int yLocal;
    private Color colorA = Color.black;

    [HideInInspector]
    public Vector2 globalPos;
    [HideInInspector]
    public float seed;
    [HideInInspector]
    public float seed2;

    private bool stop = false;

    public WaveTiles WaveScript;
    private Texture2D wavesTexture1, wavesTexture2, wavesTexture3;

    //functions
    private void ColorAndTexture(int z, Color baseColor, float range, float rangeStart, float rangeEnd)
    {
        float chosenCol = -yPure * range + Random.Range(rangeStart, rangeEnd);
        Color color = baseColor + new Color(chosenCol, chosenCol, chosenCol);
        texture.SetPixel(x, z, color);
    }



    // Use this for initialization
    private void Start()
    {
        //declare new materials
        GetComponent<Renderer>().material = new Material(shade);
        globalMat = GetComponent<Renderer>().material;
        texture = new Texture2D(terrainsize, terrainsize);
        globalMat.mainTexture = texture;

        //for each terrain tile, make sure you arent the original tile. Get seed from original
        if (seed==0)
        {
            seed = (float)(100000 * Random.Range(1f, 10f));
            seed2 = (float)(100000 * Random.Range(1f, 10f));
        }

        //alpha color
        colorA.a = 0;
        xLocal = (int) globalPos.x;
        yLocal = (int) globalPos.y;

        //waves for shore
        wavesTexture1 = new Texture2D(terrainsize, terrainsize);
        wavesTexture2 = new Texture2D(terrainsize, terrainsize);
        wavesTexture3 = new Texture2D(terrainsize, terrainsize);
        WaveScript.Initialize(terrainsize, wavesTexture1, wavesTexture2, wavesTexture3);

        generateWorld();
    }

    private void GenerateOcean(int z) {                    
        if ((SandLevel + 1 - yPure * 4.5f) < 6)
        {
            int tempColNo = (int)(SandLevel + 1 - (yPure * 12f + Random.Range(-0.03f, 0.03f)));
            if (tempColNo< 0)
            {
                tempColNo = 0;
            }
            else
            if (tempColNo > WaterColors.Length - 1)
            {
                tempColNo = WaterColors.Length - 1;
            }
            Color color = WaterColors[tempColNo];
            texture.SetPixel(x, z, color);

            //____________add waves
            WaveScript.UpdateWaves(tempColNo,x,z, wavesTexture1, wavesTexture2, wavesTexture3);
        }
        else
        {
            //__________deep ocean
            Color color = WaterColors[11] + new Color(yPure / 45, yPure / 45, yPure / 45);
            texture.SetPixel(x, z, color);
        }
    }

    private void GenerateMountains(float xused, int z, float nx)
    {
        //look at the blocks above to check what color it should be (these are the ypure2 etc)
        float nxtemp = (xused - 1 + seed) / detail;
        float nytemp = (z - 1 + seed) / detail;
        float yPure2 = func.Perlinise(nx, nytemp) * heightScale;
        float yPure3 = func.Perlinise(nxtemp, nytemp) * heightScale;
        // change the color depending on the surrounding pixels (shaddow)
        int tempColNo = (int)((yPure + Random.Range(-0.3f, 0.3f) - StoneLevel));
        if (tempColNo < -1)
        {
            tempColNo = -1;
        }
        //create shaddow
        if (yPure3 < y)
        {
            if (yPure2 < y)
            {
                if (tempColNo + 1 > RockColors.Length - 1)
                {
                    tempColNo = RockColors.Length - 1 - 1;
                }
                ColorAndTexture(z, RockColors[tempColNo + 1], 0, -0.02f, 0.02f);
            }
            else
            {
                if (tempColNo + 3 > RockColors.Length - 1)
                {
                    tempColNo = RockColors.Length - 1 - 3;
                }
                ColorAndTexture(z, RockColors[2 + tempColNo + 1], 0, -0.02f, 0.02f);
            }
        }
        //create highlights
        else
        {
            if (yPure2 < y)
            {
                if (tempColNo + 6 > RockColors.Length - 1)
                {
                    tempColNo = RockColors.Length - 1 - 6;
                }
                ColorAndTexture(z, RockColors[5 + tempColNo + 1], 0, -0.02f, 0.02f);
            }
            else
            {
                if (6 + tempColNo + 1 > RockColors.Length - 1)
                {
                    ColorAndTexture(z, Color.white, 0.2f, -0.04f, 0f);
                }
                else
                {
                    ColorAndTexture(z, RockColors[6 + tempColNo + 1], 0, -0.02f, 0.02f);
                }
            }
        }
    }


    //______________________Stone Coloring_______________________
    private void ColorBeachRocks(int x, int z, float nx)
    {
        //beach rocks
        //look at the blocks above to check what color it should be (these are the ypure2 etc)
        float nxtemp = (x - 1 + seed) / detail;
        float nytemp = (z - 1 + seed) / detail;
        float yPure2 = func.Perlinise(nx, nytemp) * heightScale;
        float yPure3 = func.Perlinise(nxtemp, nytemp) * heightScale;
        // change the color depending on the surrounding pixels
        int tempColNo = (int)((newSeedy + Random.Range(-0.3f, 0.3f) - 17));
        if (tempColNo < 0)
        {
            tempColNo = 0;
        }
        else
        if (tempColNo > RockColors.Length - 1 + 6)
        {
            tempColNo = RockColors.Length - 1 + 6;
        }

        if (yPure3 < y)
        {
            if (yPure2 < y)
            {
                ColorAndTexture(z, RockColors[tempColNo + 1], 0.025f, -0.015f, 0.025f);
            }
            else
            {
                ColorAndTexture(z, RockColors[2 + tempColNo + 1], 0.025f, -0.015f, 0.025f);
            }
        }
        else
        {
            if (yPure2 < y)
            {
                ColorAndTexture(z, RockColors[5 + tempColNo + 1], 0.025f, -0.015f, 0.025f);
            }
            else
            {
                if (6 + tempColNo + 1 > RockColors.Length - 1)
                {
                    if (6 + tempColNo + 2 > RockColors.Length - 1)
                    {
                        tempColNo = tempColNo - 2;
                    }
                    else
                    {
                        tempColNo = tempColNo - 1;
                    }
                }
                ColorAndTexture(z, RockColors[6 + tempColNo + 1], 0.025f, -0.015f, 0.025f);
            }
        }
    }
    
    public void GenerateSand (int z, float nx)
    {
        if (newSeedy < 10 && newSeedy > 5.5f) //______________________Coast stone (replaces sand)_______________________
        {
            ColorBeachRocks(xLocal, z, nx);
        }
        else //_____________________________________________Beach Sand__________________________________________________
        {
            if (yPure < SandLevel + 0.05f) //______________________Sand edge
            {
                ColorAndTexture(z, SandColors[1], 0, -0.02f, 0.025f);
            }
            else
            {
                //look at surrounding levels (setup for all sand types)
                float nxtemp = (xLocal - 1 + seed) / detail;
                float nytemp = (z - 1 + seed) / detail;
                float yPure2 = func.Perlinise(nx, nytemp) * heightScale;
                float yPure3 = func.Perlinise(nxtemp, nytemp) * heightScale;
                // make the bottom sand darker
                float levelDiff = (float)(GrassLevel) - (float)(SandLevel);
                if (newSeedy < 11 && newSeedy > 4.5f) //______________________Rocky sand
                {
                    // make the bottom sand darker
                    int tempColNo = (int)((yPure) * 3.8f * (levelDiff));
                    if (tempColNo <= 0)
                    {
                        if (yPure3 > y)
                        {
                            tempColNo = 0;
                        }
                        else
                        {
                            if (yPure2 > y)
                            {
                                tempColNo = 1;
                            }
                            else
                            {
                                tempColNo = 2;
                            }
                        }
                    }
                    else
                    if (tempColNo > SandColors.Length - 1)
                    {
                        tempColNo = SandColors.Length - 1;
                    }
                    //set color
                    ColorAndTexture(z, SandColors[tempColNo], 0.025f, -0.015f, 0.025f);
                }
                else //______________________Smooth sand
                {
                    Color darkenss = new Color(0, 0, 0);
                    if (yPure3 > y)
                    {
                        darkenss = SandColors[0] / 4;
                    }
                    else
                    {
                        if (yPure2 > y)
                        {
                            darkenss = SandColors[0] / 5;
                        }
                        else
                        {
                            darkenss = SandColors[0] / 6;
                        }
                    }
                    //set color
                    float chosenCol = ((SandLevel * yPure) - GrassLevel * yPure) * 0.8f + Random.Range(-0.015f, 0.025f);
                    Color color = SandColors[2] + new Color(chosenCol, chosenCol, chosenCol) + darkenss;
                    texture.SetPixel(x, z, color);
                }
            }
        }
    }


    void generateWorld()
    {
        //check if tile has finished generating
        if (stop == false)
        {
            for (int z = yLocal; z < terrainsize + yLocal; z++)
            {
                //set where there are NO waves invisable
                WaveScript.NoWaves( x, z, wavesTexture1, wavesTexture2, wavesTexture3);

                float nx = (xLocal + seed) / detail;
                float ny = (z + seed) / detail;

                y = func.Perlinise(nx, ny) * heightScale;

                yPure = y
                    - sink
                    - 0.3f * (Mathf.Sqrt(((x - (terrainsize / 2)) * (x - (terrainsize / 2))) * sinMultiplyer + ((z - (terrainsize / 2)) * (z - (terrainsize / 2))) * sinMultiplyer) / 1.2f);
                
                //biome seed 
                float nx2 = (xLocal + seed2) / detail;
                float ny2 = (z + seed2) / detail;
                newSeedy = func.Perlinise(nx2, ny2) * heightScale;

                if (yPure < SandLevel) //__________________________OCEAN___________________________________
                {
                    GenerateOcean(z);
                }
                else 
                {
                    if (yPure > StoneLevel) //______________________________STONE(MOUNTAINS)___________________________________
                    {
                        GenerateMountains(xLocal,z,nx);
                    }
                    else if (yPure > GrassLevel) //______________________Dark Tree_________________________
                    {
                        if (newSeedy > 17.4f)
                        {
                            ColorAndTexture(z,TreeColors[1], 0.07f, - 0.015f, 0.025f);
                        }
                        else
                        {
                            if (newSeedy > 15) //______________________LightTree_______________________
                            {
                                ColorAndTexture(z, GrassColors[2], 0.07f, - 0.015f, 0.025f);
                            }
                            else
                            {
                                if (newSeedy < 4 && yPure < 4f) //______________________Coast stone (coming into grass layer)_______________________
                                {
                                        ColorBeachRocks(xLocal, z, nx);
                                }
                                else
                                {
                                    if (func.Checker(x,z)) //______________________Grass (checkers)_______________________
                                    {
                                        ColorAndTexture(z, GrassColors[0], 0.07f, - 0.015f, 0.025f);
                                    }
                                    else
                                    {
                                       ColorAndTexture(z, GrassColors[1], 0.07f ,- 0.015f, 0.025f);
                                    }
                                }
                            }
                        }
                    }
                    else
                    if (yPure > SandLevel)
                    {
                        GenerateSand(z, nx);
                    }
                }
            }
            if (x < terrainsize-1)  //______________________LOOP IT ALL (or stop)_______________________
            {
                x++;
                xLocal++;
                generateWorld();
            }
            else
            {
                stop = true;
                x=0;
                xLocal -= terrainsize;
                //______________________apply all textures_______________________
                texture.filterMode = FilterMode.Point;
                WaveScript.ApplyWaves(wavesTexture1, wavesTexture2, wavesTexture3);
                texture.Apply();
            }
        }
    }
}
