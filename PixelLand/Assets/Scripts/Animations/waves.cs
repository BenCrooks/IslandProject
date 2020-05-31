using UnityEngine;

public class waves : MonoBehaviour {
    public float timer;
    public GameObject wave2, wave3;

    private float waveFallback = 0.15f;
	
    void TimerTick ()
    {
        timer += Time.deltaTime / 3;

        if (timer > 1)
        {
            timer = 0;
        }
    }


    private float checkWaveValue (float val, float speed)
    {
        if (timer < val)
        {
           return val * 2 - timer * speed;
        }
           return waveFallback - timer;
    }

	void Update () {
        TimerTick();

        GetComponent<Renderer>().material.color = new Color (1,1,1, checkWaveValue(0.75f, 3));
        wave2.GetComponent<Renderer>().material.color = new Color(1,1,1, checkWaveValue(0.5f, 2));
        wave3.GetComponent<Renderer>().material.color = new Color(1,1,1, checkWaveValue(0.25f, 1));
    }
}
