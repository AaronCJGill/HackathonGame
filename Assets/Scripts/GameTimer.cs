using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolfram.UnityLink;

public class GameTimer : MonoBehaviour
{
    //5 stops, 30 seconds for each stop
    private int stopNum = 0;
    [SerializeField]
    private int stopMax = 5;
    private float stopTimer = 30;
    [SerializeField]
    List<Sprite> stopSprites = new List<Sprite>();
    [SerializeField]
    SpriteRenderer stopSpriteRenderer;

    private void Start()
    {
        StartCoroutine(doStop());
    }

    IEnumerator doStop()
    {
        //takes stopTimer amount per stop
        yield return new WaitForSeconds(stopTimer - 5);
        //do stop noises here (door opening, announcement, 2 seconds)

        yield return new WaitForSeconds(2f);
        //train has stopped 
        //start blink of current stopSprite - flash on
        stopNum++;
        stopSpriteRenderer.sprite = stopSprites[stopNum];

        yield return new WaitForSeconds(1);
        //flash off
        stopSpriteRenderer.sprite = stopSprites[stopNum-1];

        //door close noises and start back


        yield return new WaitForSeconds(1);
        stopSpriteRenderer.sprite = stopSprites[stopNum];

        //end check
        if (stopNum != stopMax)
            StartCoroutine(doStop());
        else
            endGame();
    }


    void endGame()
    {
        StopCoroutine(doStop());//redundancy check to make sure the coroutine is stopped
    }



}
