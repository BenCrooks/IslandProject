using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWithMouseMove : MonoBehaviour {
    public float moveAmount;
    public GameObject movement;

	void Update () {
        Vector3 par = movement.transform.position;
        transform.position = new Vector3(par.x + ((Input.mousePosition.x - par.x)*moveAmount), par.y + ((Input.mousePosition.y - par.y) * moveAmount), transform.position.z);
	}
}
