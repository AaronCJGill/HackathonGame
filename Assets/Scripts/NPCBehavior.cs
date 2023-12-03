using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    AudioSource audioSource;
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


    [SerializeField]//the screen this is set to
    activeScreen currentScreen;
    [SerializeField]
    GameObject hand;


    private void Awake()
    {
        
    }

    void checkIfLooking()
    {
        //if player is looking at this screen
        //if they are, and the phone is paused, then reset back to normal
        if (GameManager.instance.activeScreen == currentScreen)
        {

        }

    }

    IEnumerator TakePhoneRoutine(float seconds)
    {
        yield return new WaitForSeconds(seconds/2);
        //wait a certain amount of time and then play heavy breathing
        //play Heavy breathing
        audioSource.Play();
        yield return new WaitForSeconds(seconds/2);

            

    }

    IEnumerator moveAway()
    {
        StopCoroutine(TakePhoneRoutine(40));
        yield return new WaitForSeconds(1f);
        //react 
        //move back to normal position

        //move hand away if on screen
        //stop heavy breathing


        //start new routine
        float sec = Random.Range(15,40);
        StartCoroutine(TakePhoneRoutine(sec));

    }

    void growBigger()
    {

    }
    void shrinkSmaller()
    {

    }

}
