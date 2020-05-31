using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    [Header("Directions")]

    public bool up, down, left, right;
    private bool lastwasup, lastwasdown, lastwasleft, lastwasright,lastWasDiagUpLeft, lastWasDiagUpRight, lastWasDiagDownLeft, lastWasDiagDownRight;

    [Header("children")]
    public GameObject Head;
    public GameObject Hair, Eyes, Nose, Body, Arms, Pants, Belt, Feet;

    [Header("sprites")]
    //up
    [Header("up")]
    public Sprite upHead;
    public Sprite upNose;
    public Sprite upEyes;
    public Sprite upHair;
    public Sprite[] upbody;
    public Sprite[] upArms;
    public Sprite upBelt;
    public Sprite[] upLegs;
    public Sprite[] upShoes;
    //down
    [Header("down")]
    public Sprite downHead;
    public Sprite[] downBody;
    public Sprite[] downArms;
    public Sprite downBelt;
    public Sprite[] downPants;
    public Sprite[] downShoes;

    //side
    [Header("side")]
    public Sprite sideHead;
    public Sprite sideNose;
    public Sprite sideEyes;
    public Sprite sideHair;
    public Sprite[] sideBody;
    public Sprite[] sideArms;
    public Sprite sideBelt;
    public Sprite[] sidePants;
    public Sprite[] sideShoes;

    //diagonal up
    [Header("diagonal up")]
    public Sprite DiagUpHair;
    public Sprite[] DiagUpBody;
    public Sprite[] DiagUpArms;
    public Sprite[] DiagUpBelt;
    public Sprite[] DiagUpPants;
    public Sprite[] DiagUpShoes;

    //diagonal down
    [Header("diagonal down")]
    public Sprite DiagDownHead;
    public Sprite DiagDownNose;
    public Sprite DiagDownEyes;
    public Sprite DiagDownHair;
    public Sprite[] DiagDownBody;
    public Sprite[] DiagDownArms;
    public Sprite[] DiagDownBelt;
    public Sprite[] DiagDownPants;
    public Sprite[] DiagDownShoes;

    private float countTo = 0.3f;
    private float counter;
    private int spritecount;
	
    private void setSprite(GameObject bodypart, Sprite sprDirection)
    {
        bodypart.GetComponent<SpriteRenderer>().sprite = sprDirection;
    }

    private void AnimateInDirection(Sprite headLocal, Sprite beltLocal, Sprite noseLocal, Sprite eyesLocal, Sprite hairLocal, Sprite bodyLocal, Sprite armsLocal, Sprite legsLocal, Sprite feetLocal)
    {
        setSprite(Head, headLocal);
        setSprite(Belt, beltLocal);
        setSprite(Nose, noseLocal);
        setSprite(Eyes, eyesLocal);
        setSprite(Hair, hairLocal);
        setSprite(Body, bodyLocal);
        setSprite(Arms, armsLocal);
        setSprite(Pants, legsLocal);
        setSprite(Feet, feetLocal);
    }

    private void ManageStillDirection(int direction)
    { //0 up, 1 upRight, so on clockwise
        lastWasDiagUpLeft = direction == 7 ? true : false;
        lastWasDiagUpRight = direction == 1 ? true : false;
        lastWasDiagDownLeft = direction == 5 ? true : false;
        lastWasDiagDownRight = direction == 3 ? true : false;
        lastwasdown = direction == 0 ? true : false;
        lastwasleft = direction == 6 ? true : false;
        lastwasright = direction == 2 ? true : false;
        lastwasup = direction==4?true:false;
    }

    private void manageInputs()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            right = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            down = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            down = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            up = true;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            up = false;
        }
    }


    void Update () {
        manageInputs();
        if (((!left && !right) || (left && right)) && ((!up && !down) || (up && down)))
        {
            //_____________________________standing still_____________________________
            if (lastwasdown)
            {
                AnimateInDirection(
                    GetComponent<ColorPerson>().hairless == true ? downHead : null,
                    downBelt,
                    null,
                    null,
                    GetComponent<ColorPerson>().hairless == true ? null : downHead,
                    downBody[spritecount],
                    downArms[spritecount],
                    downPants[spritecount], //lel
                    downShoes[spritecount]
                );
            }
            else
            if (lastwasleft || lastwasright)
            {
                AnimateInDirection(
                    sideHead,
                    sideBelt,
                    sideNose,
                    sideEyes,
                    sideHair,
                    sideBody[0],
                    sideArms[0],
                    sidePants[0],
                    sideShoes[0]
                );
            }
            else
            if (lastwasup)
            {
                AnimateInDirection(
                    upHead,
                    upBelt,
                    upNose,
                    upEyes,
                    upHair,
                    upbody[spritecount],
                    upArms[spritecount],
                    upLegs[spritecount],
                    upShoes[spritecount]
                );
            }
            else
            if(lastWasDiagDownLeft|| lastWasDiagDownRight)
            {
                AnimateInDirection(
                    DiagDownHead,
                    DiagDownBelt[0],
                    DiagDownNose,
                    DiagDownEyes,
                    DiagDownHair,
                    DiagDownBody[0],
                    DiagDownArms[0],
                    DiagDownPants[0],
                    DiagDownShoes[0]
                );
            }
            else
            if (lastWasDiagUpLeft || lastWasDiagUpRight)
            {
                AnimateInDirection(
                    GetComponent<ColorPerson>().hairless == true ? DiagUpHair : null,
                    DiagUpBelt[0],
                    null,
                    null,
                    GetComponent<ColorPerson>().hairless == true ? null : DiagUpHair,
                    DiagUpBody[0],
                    DiagUpArms[0],
                    DiagUpPants[0],
                    DiagUpShoes[0]
                );
            }

            counter = 10; //reset high so that immidiatly starts animating
            spritecount = 0;
        }
        else
        {
            //_____________________________walking_____________________________
            counter += Time.deltaTime;
            if (counter > countTo)
            {
                if(up && (right||left))
                {
                    if(left)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        ManageStillDirection(7);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        ManageStillDirection(1);
                    }

                    AnimateInDirection(GetComponent<ColorPerson>().hairless==true?DiagUpHair:null,DiagUpBelt[spritecount],null,null,GetComponent<ColorPerson>().hairless==true?null:DiagUpHair,DiagUpBody[spritecount],DiagUpArms[spritecount],DiagUpPants[spritecount],DiagUpShoes[spritecount]);
                }
                else
                if ((right||left) && down)
                {
                    if (left)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        ManageStillDirection(5);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        ManageStillDirection(3);
                    }

                    AnimateInDirection(DiagDownHead,DiagDownBelt[spritecount],DiagDownNose,DiagDownEyes,DiagDownHair,DiagDownBody[spritecount],DiagDownArms[spritecount],DiagDownPants[spritecount],DiagDownShoes[spritecount]);
                }
                else
                {
                    if (down)
                    {
                        ManageStillDirection(4);

                        AnimateInDirection(upHead,upBelt,upNose,upEyes,upHair,upbody[spritecount],upArms[spritecount],upLegs[spritecount],upShoes[spritecount]);
                    }
                    if (up)
                    {
                        ManageStillDirection(0);

                       AnimateInDirection(GetComponent<ColorPerson>().hairless==true?downHead:null,downBelt,null,null,GetComponent<ColorPerson>().hairless==true?null:downHead,downBody[spritecount],downArms[spritecount],downPants[spritecount],downShoes[spritecount]);
                    }
                    if (left)
                    {
                        ManageStillDirection(6);
                        //flip
                        transform.localScale = new Vector3(-1, 1, 1);

                        AnimateInDirection(sideHead,sideBelt,sideNose,sideEyes,sideHair,sideBody[spritecount],sideArms[spritecount],sidePants[spritecount],sideShoes[spritecount]);
                    }
                    if (right)
                    {
                        ManageStillDirection(2);
                        //flip to original
                        transform.localScale = new Vector3(1, 1, 1);

                        AnimateInDirection(sideHead,sideBelt,sideNose,sideEyes,sideHair,sideBody[spritecount],sideArms[spritecount],sidePants[spritecount],sideShoes[spritecount]);
                    }
                }
                spritecount += 1;
                if (spritecount > upbody.Length - 1)
                {
                    spritecount = 0;
                }
                counter = 0;
            }
        }
	}
}
