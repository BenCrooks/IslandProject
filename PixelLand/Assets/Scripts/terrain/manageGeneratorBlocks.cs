using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageGeneratorBlocks : MonoBehaviour {
	public GameObject originalGenerator;
	public GameObject generator;
	public bool GenerateLeft, GenerateRight, GenerateUp, GenerateDown;

	public int xPos, yPos;
	public float amountToMove;
	public float timer;

    public GameObject movement;

	void Start () {
		amountToMove =10* originalGenerator.transform.localScale.x;
	}

    private void generate()
    {
        GameObject tempNewLand = Instantiate(generator, new Vector3(-xPos, -yPos, 10), Quaternion.Euler(-90, 0, 0));
        tempNewLand.GetComponent<FinalTerrain>().globalPos = new Vector2(xPos / (int)amountToMove * originalGenerator.GetComponent<FinalTerrain>().terrainsize, yPos / (int)amountToMove * originalGenerator.GetComponent<FinalTerrain>().terrainsize);
        tempNewLand.GetComponent<FinalTerrain>().seed = originalGenerator.GetComponent<FinalTerrain>().seed;
        tempNewLand.GetComponent<FinalTerrain>().seed2 = originalGenerator.GetComponent<FinalTerrain>().seed2;
        tempNewLand.transform.GetChild(0).GetComponent<waves>().timer = timer;
    }

    private void manageTimer()
    {
        timer += Time.deltaTime / 3;
        if (timer > 1)
        {
            timer = 0;
        }
    }

    void Update () {
        if (GenerateLeft || GenerateDown || GenerateUp || GenerateRight)
        {
            manageTimer();
            xPos = (int)-movement.transform.position.x;
            yPos = (int)-movement.transform.position.y;
            if (GenerateLeft == true)
            {
                xPos += (int)amountToMove;
                GenerateLeft = false;
                generate();
            }
            else
            if (GenerateRight == true)
            {
                xPos -= (int)amountToMove;
                GenerateRight = false;
                generate();
            }
            else
            if (GenerateUp == true)
            {
                yPos -= (int)amountToMove;
                GenerateUp = false;
                generate();
            }
            else
            if (GenerateDown == true)
            {
                yPos += (int)amountToMove;
                GenerateDown = false;
                generate();
            }
        }
	}
}
