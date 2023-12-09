using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum npcState { offScreen, wait, getCloser, handAproaches, pause, deboard, handRetreatBack }
public class NPC_Manager : MonoBehaviour
{
    public npcState state = npcState.offScreen;
    public bool isInScreen = false; //when is in screen, show the Sprite
    public GameObject NPC;
    public GameObject NPC_Face;
    // get its pos
    public GameObject NPC_Hand;
    float initialHandX;
    float initialHandY;
    public float handIncrement;
    private float potatoModeHandIncrement;
    int waitTime = 60; //static var, maybe make it an array and put both wait times here?
    int timeWaited; // incremented var
    // bool startGettingCloser = false;
    float scale = 1;
    float finalScale;
    int hitCount;

    float handIncrementWebGL;//use if have to hard code webgl differences

    //heavy breathing sound
    public AudioSource heavyBreathing;

    void Start()
    {
        //set handincrement if in webgl
        handIncrementWebGL = handIncrement * 3;
        
        //get the initial pos of the hand
        initialHandX = NPC_Hand.transform.position.x;
        initialHandY = NPC_Hand.transform.position.y;
        //isInScreen = true;
        Debug.Log(initialHandX);
        Debug.Log(initialHandY);
        playerHand.SetActive(false);
        if (isInScreen == false)
            NPC_Face.SetActive(false);
    }

    
    void Update()
    {
        Debug.Log(state) ;
        switch (state)
        {
            case npcState.offScreen:
                if (isInScreen == true)
                {
                    NPC_Face.SetActive(true);
                    NPC_Hand.SetActive(true);
                    state = npcState.wait;
                }      
                break;

            case npcState.wait:
                if (!GameManager.PotatoMode)
                {
                    timeWaited++;
                }
                else if (GameManager.PotatoMode)
                {
                    timeWaited += 3;//compensate to match, everything is 3 times as fast now
                }

                if (waitTime == timeWaited)
                {
                    timeWaited = 0;
                    state = npcState.getCloser;
                }
                break;

            case npcState.getCloser:
                //if heavy breathing is playing close it -------------------------------
                if(heavyBreathing.isPlaying == true)
                    heavyBreathing.Stop();
                
                //determine if potatomode is active first
                if (!GameManager.PotatoMode)
                {
                    //no potato mode
                    if (phoneScreenButton.instance.phoneOpen == true)
                        scale += .0005f;
                    else
                        scale -= .0005f;
                }
                else if(GameManager.PotatoMode)
                {
                    //yes potato mode
                    if (phoneScreenButton.instance.phoneOpen == true)
                    {
                        scale += .0005f * 3;
                        Debug.Log(scale);
                    }
                    else
                    {
                        scale -= .0005f * 3;
                    }
                }

                NPC_Face.transform.localScale = new Vector2(scale,scale);
                if (scale >= 2)
                {
                    //start playing heavy breathing -------------------------------
                    heavyBreathing.Play();

                    //got too close, send the hand
                    state = npcState.pause;
                }
                else if (scale <= 1)
                {
                    state = npcState.deboard;
                }

                break;

            case npcState.handAproaches:
                //if in potato mode change the value
                if (GameManager.PotatoMode)
                {
                    NPC_Hand.GetComponent<RectTransform>().position += new Vector3(potatoModeHandIncrement, 0, 0);
                }
                else if(!GameManager.PotatoMode)
                {
                    NPC_Hand.GetComponent<RectTransform>().position += new Vector3(handIncrement, 0, 0);
                }

                if (handIncrement <= 0) // can be deleted perhaps?
                {
                    NPC_Hand.transform.GetChild(0).GetComponent<RectTransform>().position = NPC_Hand.GetComponent<RectTransform>().position;
                    NPC_Hand.transform.GetChild(1).GetComponent<RectTransform>().position = NPC_Hand.GetComponent<RectTransform>().position;
                }

                if (handIncrement > 0 && NPC_Hand.GetComponent<RectTransform>().position.x >= 0) //can check if hand is on the phone
                {
                    heavyBreathing.Stop();
                    GameManager.instance.GameEnd(false);
                    state = npcState.deboard;
                }
                else if (handIncrement <= 0 && NPC_Hand.GetComponent<RectTransform>().position.x <= 0)
                {
                    heavyBreathing.Stop();
                    GameManager.instance.GameEnd(false);
                    state = npcState.deboard;
                }
                break;
            case npcState.pause:
                if (phoneScreenButton.instance.phoneOpen == false)
                {
                    timeWaited = 0;
                    state = npcState.getCloser;
                }

                //if wait time is over, go to handAproaches
                if (!GameManager.PotatoMode)
                {
                    timeWaited++;
                }
                else
                {
                    timeWaited += 3;
                }


                if (waitTime >= timeWaited)
                {
                    timeWaited = 0;
                    state = npcState.handAproaches;
                }
                break;

            case npcState.deboard:
                // call from the timer and make it sleep and go to off screen
                break;

            case npcState.handRetreatBack: // hand goes back
                //wait until the animation is over, animate
                if (!GameManager.PotatoMode)
                {
                    if (playerHand.GetComponent<RectTransform>().position.y > GameManager.instance.mainYCoordinate - 1)
                        playerHand.GetComponent<RectTransform>().position -= new Vector3(0, .05f, 0);
                    else
                    {
                        //close the player hand if still open
                        if (playerHand.activeSelf)
                            playerHand.SetActive(false);
                        //move the  NPC hand back
                        NPC_Hand.GetComponent<RectTransform>().position += new Vector3(-(handIncrement * 2), 0, 0);
                        if (handIncrement > 0 && NPC_Hand.GetComponent<RectTransform>().position.x < initialHandX)
                        {
                            //stop playing heavy breathing ---------------------------------------------------------------------------------------------
                            heavyBreathing.Stop();
                            NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX, initialHandY, 0);
                            scale = 1;
                            NPC_Face.transform.localScale = new Vector2(scale, scale);
                            timeWaited = 0;
                            if (hitCount == 2)
                                state = npcState.deboard;
                            else
                                state = npcState.wait; // go back
                        }
                        else if (handIncrement < 0 && NPC_Hand.GetComponent<RectTransform>().position.x > initialHandX)
                        {
                            //stop playing heavy breathing ---------------------------------------------------------------------------------------------
                            heavyBreathing.Stop();
                            NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX, initialHandY, 0);
                            scale = 1;
                            NPC_Face.transform.localScale = new Vector2(scale, scale);
                            timeWaited = 0;
                            if (hitCount == 2)
                                state = npcState.deboard;
                            else
                                state = npcState.wait; // go back
                        }
                    }
                }
                else if(GameManager.PotatoMode)//POTATO MODE - values are multiplied by 3
                {
                    if (playerHand.GetComponent<RectTransform>().position.y > GameManager.instance.mainYCoordinate - 1)
                        playerHand.GetComponent<RectTransform>().position -= new Vector3(0, .05f * 3, 0);
                    else
                    {
                        //close the player hand if still open
                        if (playerHand.activeSelf)
                            playerHand.SetActive(false);
                        //move the  NPC hand back
                        NPC_Hand.GetComponent<RectTransform>().position += new Vector3(-((handIncrement * 2) * 3), 0, 0);
                        if (handIncrement > 0 && NPC_Hand.GetComponent<RectTransform>().position.x < initialHandX)
                        {
                            //stop playing heavy breathing ---------------------------------------------------------------------------------------------
                            heavyBreathing.Stop();
                            NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX, initialHandY, 0);//may need to affect this as well
                            scale = 1;
                            NPC_Face.transform.localScale = new Vector2(scale, scale);
                            timeWaited = 0;
                            if (hitCount == 2)
                                state = npcState.deboard;
                            else
                                state = npcState.wait; // go back
                        }
                        else if (handIncrement < 0 && NPC_Hand.GetComponent<RectTransform>().position.x > initialHandX)
                        {
                            //stop playing heavy breathing ---------------------------------------------------------------------------------------------
                            heavyBreathing.Stop();
                            NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX * 3, initialHandY * 3, 0);
                            scale = 1;
                            NPC_Face.transform.localScale = new Vector2(scale, scale);
                            timeWaited = 0;
                            if (hitCount == 2)
                                state = npcState.deboard;
                            else
                                state = npcState.wait; // go back
                        }
                    }
                }
                
                break;
        }
    }

    public GameObject playerHand;
    public void HandHit() // buttonClick
    {
        hitCount++;
        playerHand.SetActive(true);
        //playerHand.GetComponent<RectTransform>().position = new Vector3(0, NPC_Hand.GetComponent<RectTransform>().position.y + 150, 0);
        playerHand.GetComponent<RectTransform>().position = new Vector3(NPC_Hand.GetComponent<RectTransform>().position.x+1.5f, NPC_Hand.GetComponent<RectTransform>().position.y + 2, 0);
        timeWaited = 0;
        //start playing slap ---------------------------------------------------------------------------------------------
        AudioManager.instance.Slap.Play();
        state = npcState.handRetreatBack;
    }
}
