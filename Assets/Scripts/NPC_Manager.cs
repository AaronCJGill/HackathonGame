using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int waitTime = 1; //static var, maybe make it an array and put both wait times here? - represents seconds
    float timeWaited; // incremented var
    // bool startGettingCloser = false;
    float scale = 1;
    float finalScale;
    int hitCount;
    [Range(0.1f, 1)] [SerializeField][Tooltip("Divide 2 by this number to get the amount of seconds it will take to finish scaling.")]
    private float scaleIncrement = 0.5f;

    float handIncrementWebGL;//use if have to hard code webgl differences

    //heavy breathing sound
    public AudioSource heavyBreathing;

    // ---- UI CHANGES
    public GameObject LookUI;
    public Sprite lookHit;
    public Sprite lookDown;
    // ----

    void Start()
    {
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
    public bool debugMode = false;
    
    void Update()
    {   
        if (debugMode)
        {
            Debug.Log(state);
        }
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
                timeWaited += Time.deltaTime;

                if (waitTime <= timeWaited)
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
                if (phoneScreenButton.instance.phoneOpen == true)
                    scale += scaleIncrement * Time.deltaTime;
                else
                    scale -= scaleIncrement * Time.deltaTime;
                
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
                NPC_Hand.GetComponent<RectTransform>().position += new Vector3(handIncrement * Time.deltaTime, 0, 0);
                
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
                timeWaited += Time.deltaTime;

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
                if (playerHand.GetComponent<RectTransform>().position.y > GameManager.instance.mainYCoordinate - 1)
                {
                    playerHand.GetComponent<RectTransform>().position -= new Vector3(0, 6* Time.deltaTime, 0);
                }
                else
                {
                    //close the player hand if still open
                    if (playerHand.activeSelf)
                    {
                        playerHand.SetActive(false);
                    }
                        
                    //move the  NPC hand back
                    NPC_Hand.GetComponent<RectTransform>().position += new Vector3(-(handIncrement * 2) * Time.deltaTime, 0, 0);

                    
                    if (handIncrement > 0 && NPC_Hand.GetComponent<RectTransform>().position.x < initialHandX)
                    {
                        // ---- UI CHANGES
                        LookUI.GetComponent<Image>().sprite = lookDown;
                        // ----

                        //stop playing heavy breathing 
                        heavyBreathing.Stop();
                        //move
                        NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX * Time.deltaTime, initialHandY * Time.deltaTime, 0);
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
                        // ---- UI CHANGES
                        LookUI.GetComponent<Image>().sprite = lookDown;
                        // ----

                        //stop playing heavy breathing
                        heavyBreathing.Stop();
                        //move
                        NPC_Hand.GetComponent<RectTransform>().position += new Vector3(initialHandX * Time.deltaTime, initialHandY * Time.deltaTime, 0);
                        scale = 1;
                        NPC_Face.transform.localScale = new Vector2(scale, scale);
                        timeWaited = 0;
                        if (hitCount == 2)
                            state = npcState.deboard;
                        else
                            state = npcState.wait; // go back
                    }
                }
                
                break;
        }
    }

    public GameObject playerHand;

    public void HandHit() // buttonClick
    {
        // ---- UI CHANGES
        LookUI.GetComponent<Image>().sprite = lookHit;
        // ----

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
