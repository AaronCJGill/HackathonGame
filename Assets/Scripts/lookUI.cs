using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lookUI : MonoBehaviour
{
    Image myImageComponent;

    //characters
    public GameObject phoneOff;
    public List<Sprite> lookSprites = new List<Sprite>();
    //warning
    public GameObject womanNPC;
    public GameObject warnLeft;
    public GameObject dogNPC;
    public GameObject warnRight;

    void Start()
    {
        myImageComponent = GetComponent<Image>();
    }

    void Update()
    {
        /// DIRECTION
        //up
        if(Input.GetKeyDown(KeyCode.W))
        {
            myImageComponent.sprite  = lookSprites[0];
        }
        //left
        else if(Input.GetKeyDown(KeyCode.A))
        {
            myImageComponent.sprite  = lookSprites[1];
        }
        //right
        else if(Input.GetKeyDown(KeyCode.D))
        {
            myImageComponent.sprite  = lookSprites[2];
        }
        //down (default)
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            myImageComponent.sprite = lookSprites[3];
            //if phone off
            if(phoneOff.activeSelf == true)
            {
                myImageComponent.sprite = lookSprites[4];
            }
        }

        ///WARNING
        //Debug.Log(womanNPC.transform.localScale.x);
        //woman(L)
        if(womanNPC.activeSelf && (womanNPC.transform.localScale.x > 1.3))
        {
            warnLeft.SetActive(true);
        }
        else 
        {
            warnLeft.SetActive(false);
        }

        //dog(R)
        if(dogNPC.activeSelf && (dogNPC.transform.localScale.x > 1.3))
        {
            warnRight.SetActive(true);
        }
        else 
        {
            warnRight.SetActive(false);
        }
    }
}
