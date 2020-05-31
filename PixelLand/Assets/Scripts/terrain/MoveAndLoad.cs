using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndLoad : MonoBehaviour {
    public GameObject generatorManager;

    public bool left, right, up, down;
    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).
    public bool load = true;

    void setViewPosition ()
    {
        generatorManager.GetComponent<manageGeneratorBlocks>().xPos = (int)transform.position.x;
        generatorManager.GetComponent<manageGeneratorBlocks>().yPos = (int)transform.position.y;
    }

    void checkSurroundings()
    {
        // Cast a ray straight down.
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position + new Vector3(0, -10, 0), -Vector2.up);
        if (hitdown.collider == null || hitdown.distance > 4.0f)
        {
            setViewPosition();
            //there is no image, generate
            generatorManager.GetComponent<manageGeneratorBlocks>().GenerateDown = true;
        }

        // Cast a ray straight up.
        RaycastHit2D hitup = Physics2D.Raycast(transform.position + new Vector3(0, 10, 0), Vector2.up);
        if (hitup.collider == null || hitup.distance > 4.0f)
        {
            setViewPosition();
            //there is no image, generate
            generatorManager.GetComponent<manageGeneratorBlocks>().GenerateUp = true;
        }

        // Cast a ray straight left.
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position + new Vector3(-10, 0, 0), Vector2.left);
        if (hitleft.collider == null || hitleft.distance > 4.0f)
        {
            setViewPosition();
            //there is no image, generate
            generatorManager.GetComponent<manageGeneratorBlocks>().GenerateLeft = true;
        }

        // Cast a ray straight right.
        RaycastHit2D hitright = Physics2D.Raycast(transform.position + new Vector3(10, 0, 0), -Vector2.left);
        if (hitright.collider == null || hitright.distance > 4.0f)
        {
            setViewPosition();
            //there is no image, generate
            generatorManager.GetComponent<manageGeneratorBlocks>().GenerateRight = true;
        }
        load = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            checkSurroundings();
        }
    }
}
