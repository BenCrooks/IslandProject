using UnityEngine;

public class blink : MonoBehaviour {
    public Color eyesClosed;
    public Color eyesOpen;
    private float timer;
    private float countTo;

    void setCountTo ()
    {
        countTo = Random.Range(1f, 6f);
    }

	void Start () {
        setCountTo();
    }

	void Update () {
        timer += Time.deltaTime;
        if(timer>countTo)
        {
            GetComponent<SpriteRenderer>().color = eyesClosed + new Color(-0.12f,-0.12f,-0.12f);
            if (timer > countTo + 0.2f)
            {
                GetComponent<SpriteRenderer>().color = eyesOpen;
                timer = 0;
                setCountTo();
            }
        }
	}
}
