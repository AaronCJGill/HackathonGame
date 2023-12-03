using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCBehavior : MonoBehaviour
{
    AudioSource audioSource;
    Transform startPos, endPos;
    [SerializeField]
    bool isDog = false;

    public enum activeScreen
    {
        up,
        left,
        right,
        down
    }

    public enum actions 
    {
        normal,
        breathing, //heavy breathing - handsy - 
        handsy
    }
    actions currentAction = actions.normal;

    [SerializeField]//the screen this is set to
    activeScreen currentScreen;
    [SerializeField]
    GameObject hand;
    
    [SerializeField]
    Transform handStartPos;
    [SerializeField]
    Transform handEndPos;
    bool handTouched = false;

    private void Awake()
    {
        
    }
    private void Start()
    {
        float x = Random.Range(1, 3);//10/15 to start off
        //Debug.Log(x);
        StartCoroutine(TakePhoneRoutine(12));
    }
    private void Update()
    {
        checkIfLooking();
    }
    void checkIfLooking()
    {
        //if player is looking at this screen
        //if they are, and the phone is paused, then reset back to normal
        if (GameManager.instance.activeScreen == currentScreen)
        {
            StartCoroutine(moveAway());
        }
    }

    IEnumerator TakePhoneRoutine(float seconds)
    {
        handTouched = false;
        Debug.Log("TakePhone Called" + seconds + "  " + (seconds / 3));
        yield return new WaitForSeconds(seconds/3);
        Debug.Log("breathing - grow bigger");
        //wait a certain amount of time and then play heavy breathing
        //play Heavy breathing
        //audioSource.Play();
        currentAction = actions.breathing;
        growBigger();
        yield return new WaitForSeconds(seconds/3);
        Debug.Log("start reaching for phone");
        //start to reach for phone - should take the same amount of time to reach down
        StartCoroutine(handMoveBehavior(seconds/3));
        currentAction = actions.handsy;
        yield return new WaitForSeconds(seconds/3);
        //lose game
        GameManager.instance.GameEnd();

    }

    public void triggered()
    {
        StartCoroutine( moveAway());
    }

    IEnumerator moveAway()
    {
        StopCoroutine(handMoveBehavior(100f));
        StopCoroutine(TakePhoneRoutine(40));
        Debug.Log("HandMoveCoroutine Stopped - moveaway triggered - behavior reset");
        StartCoroutine(moveHandBackBehavior(3f));
        handTouched = true;
        yield return new WaitForSeconds(1f);
        //react 
        //move back to normal position
        //move hand away if on screen
        //stop heavy breathing

        shrinkSmaller();
        //audioSource.Stop();
        currentAction = actions.normal;

        //move hand back to position really quickly

        //start new routine
        float sec = Random.Range(15,40);
        StartCoroutine(TakePhoneRoutine(sec));
    }
    IEnumerator handMoveBehavior(float t)
    {

            //Debug.Log("Starting Hand Move Behavior");
            float changePos = hand.transform.position.x;
            float timeElapsed = 0;
            while (timeElapsed < t)
            {
                changePos = EasingFunction.EaseInOutSine(handStartPos.position.x, handEndPos.position.x, timeElapsed / t);//Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                hand.transform.position = new Vector3(changePos, handEndPos.position.y, 0);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            hand.transform.position = handEndPos.position;
            //Debug.Log("HandMoveComplete");
        
        
    }
    IEnumerator moveHandBackBehavior(float t)
    {
        float changePos = hand.transform.position.x;
        float startHandPosNew = hand.transform.position.x;
        float timeElapsed = 0;
        while (timeElapsed < t)
        {
            changePos = EasingFunction.EaseOutExpo(startHandPosNew, handStartPos.position.x, timeElapsed / t);//Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            hand.transform.position = new Vector3(changePos, handEndPos.position.y, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        hand.transform.position = handStartPos.position;
    }



    void growBigger()
    {
        //grows self bigger when breathing
        if (isDog)
        {
            transform.localScale = new Vector3(200,200,1);
        }
        else
        {
            transform.localScale = new Vector3(200, 200, 1);
        }
    }
    void shrinkSmaller()
    {

        if (isDog)
        {
            transform.localScale = new Vector3(108, 108, 1);
        }
        else
        {
            transform.localScale = new Vector3(108, 108, 1);
        }
    }

}
