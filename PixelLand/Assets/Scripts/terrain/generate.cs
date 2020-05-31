using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour {
    public GameObject spawnGenerateManager;
    private Vector2 pos;
    private float needToMoveThisMuchBeforeGen;
	
	void Update () {
		needToMoveThisMuchBeforeGen = spawnGenerateManager.GetComponent<manageGeneratorBlocks>().amountToMove;
		if(transform.position.x>pos.x+needToMoveThisMuchBeforeGen)
		{
			spawnGenerateManager.GetComponent<manageGeneratorBlocks>().GenerateRight = true;
			pos = new Vector2(pos.x+needToMoveThisMuchBeforeGen,pos.y);
		}
		else
		if(transform.position.x<pos.x-needToMoveThisMuchBeforeGen)
		{
			spawnGenerateManager.GetComponent<manageGeneratorBlocks>().GenerateLeft = true;
			pos = new Vector2(pos.x-needToMoveThisMuchBeforeGen,pos.y);
		}

		if(transform.position.y<pos.y-needToMoveThisMuchBeforeGen)
		{
			spawnGenerateManager.GetComponent<manageGeneratorBlocks>().GenerateDown = true;
			pos = new Vector2(pos.x,pos.y-needToMoveThisMuchBeforeGen);
		}
		else
		if(transform.position.y>pos.y+needToMoveThisMuchBeforeGen)
		{
			spawnGenerateManager.GetComponent<manageGeneratorBlocks>().GenerateUp = true;
			pos = new Vector2(pos.x,pos.y+needToMoveThisMuchBeforeGen);
		}
	}
}
