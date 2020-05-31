using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPerson : MonoBehaviour {
    public GameObject Head, Hair, Eyes, Nose, Body, Arms, Pants, Belt, Feet;
    public Color[] SkinColor;
    public Color[] HairColor;
    public Color[] EyeColor;
    public Color[] NoseColor;
    public Color[] PantsColor;
    public Color[] ShoesColor;
    public bool beltLess;
    public bool bareFoot;
    public bool Shirtless;
    public bool Pantsless;
    public bool hairless;

    private bool ChanceFunc (float chance){
        if (Random.Range(0f, 1f) < chance)
        {
            return true;
        }
        return false;
    }

    private Color colorMe(Color[] col)
    {
        float rand = Random.Range(0f, 1f);
        return new Color((col[0].r * (1f - rand)) + (col[1].r * rand), (col[0].g * (1f - rand)) + (col[1].g * rand), (col[0].b * (1f - rand)) + (col[1].b * rand));
    }

    private void setColor (GameObject bodyPart, Color col)
    {
        bodyPart.GetComponent<SpriteRenderer>().color = col;
    }

    private void colorSprites ()
    {
        if (beltLess){ Belt.SetActive(false); }

        Color skincol = colorMe(SkinColor);
        Eyes.GetComponent<blink>().eyesClosed = skincol;
        Head.GetComponent<SpriteRenderer>().color = skincol;
        Arms.GetComponent<SpriteRenderer>().color = skincol;

        if (bareFoot){
            setColor(Feet, skincol);
        }
        else {
            setColor(Feet, colorMe(ShoesColor));
        }

        if (Shirtless)
        {
            setColor(Body, skincol);
        }
        else
        {
            setColor(Body, new Color(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f)));
        }

        if (Pantsless)
        {
            setColor(Pants, skincol);
        }
        else
        {
            setColor(Pants, colorMe(PantsColor));
        }

        if (hairless)
        {
            setColor(Hair, new Color(0, 0, 0, 0));
        }
        else
        {
            setColor(Hair, colorMe(HairColor));
        }

        setColor(Nose, colorMe(NoseColor));

        Color eyescol = colorMe(EyeColor);
        setColor(Eyes, eyescol);
        Eyes.GetComponent<blink>().eyesOpen = eyescol;
    }

    void Start () {
        //clothe
        if (ChanceFunc(0.1f)){ hairless = true; };
        if (ChanceFunc(0.2f)) { bareFoot = true; };
        if (ChanceFunc(0.2f)){ beltLess = true; };
        if (ChanceFunc(0.01f))
        {
            Shirtless = true;
            if (ChanceFunc(0.2f))
            {
                Pantsless = true;
                beltLess = true;
                bareFoot = true;
            }
        }
        colorSprites();
    }
}
