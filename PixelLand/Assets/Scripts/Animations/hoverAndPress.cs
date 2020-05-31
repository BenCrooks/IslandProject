using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverAndPress : MonoBehaviour {
    private bool shrink = true;
    public float growthAmount;
    private GameObject movement;

    private void Start()
    {
        movement = GameObject.FindWithTag("movement");
    }

    void OnMouseEnter() {
        shrink = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        shrink = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        movement.transform.position = transform.parent.transform.position;
        movement.GetComponent<MoveAndLoad>().load = true;
    }

    void ShrinkFunc()
    {
        Vector3 scale = transform.parent.transform.localScale;
        scale = new Vector3(Mathf.Lerp(scale.x, 1, 0.4f), 1, Mathf.Lerp(scale.z, 1, 0.4f));
        transform.parent.transform.localScale = scale;
        Vector3 pos = transform.parent.transform.position;
        transform.parent.transform.position = new Vector3(pos.x, pos.y, 1);
    }

    void GrowFunc()
    {
        Vector3 scale = transform.parent.transform.localScale;
        scale = new Vector3(Mathf.Lerp(scale.x, 1 + growthAmount, 0.1f), 1, Mathf.Lerp(scale.z, 1 + growthAmount, 0.1f));
        transform.parent.transform.localScale = scale;
        Vector3 pos = transform.parent.transform.position;
        transform.parent.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    void Update()
    {
        if(shrink)
        {
            ShrinkFunc();
        }
        else
        {
            GrowFunc();
        }
    }
}
