using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public int currentStop = 0;
    public int stops;
    public int timeBetweenStops = 0;
    public int gameTime =  0;
    public bool which = true; //true = 1, false = 2
    public float timeAlarm;
    public float timeAlarm2;
    public bool gameEnded = false;

    public List<Sprite> stopImages = new List<Sprite>(); // the count of this obj gives stop num
    public Image stopSign;

    public static timer instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject npc1;
    public GameObject npc2;

    public void Start()
    {
        stops = stopImages.Count -1;
        if (timeBetweenStops == 0) //according to whichever you set up, will calculate the other
        {
            timeBetweenStops = gameTime / stops;
        }
        else if (gameTime == 0)
        {
            gameTime = timeBetweenStops * stops;
        }

        stopSign.GetComponent<Image>().sprite = stopImages[0];
    }

    public void Update()
    {
        if (!gameEnded)
        {
            if(which ==true)
                timeAlarm += Time.deltaTime;
            else
                timeAlarm2 += Time.deltaTime;
        }

        if (timeAlarm >= timeBetweenStops) // stopping time
        {
            // play sound for deboarding -------------------------------------
            AudioManager.instance.Stop.Play();
            //change the sprite when you get here
            currentStop++;
            stopSign.GetComponent<Image>().sprite = stopImages[currentStop];

            //if an NPC was not boarded last stop, board them
            if (npc1.GetComponent<NPC_Manager>().isInScreen == false)
            {
                npc1.GetComponent<NPC_Manager>().isInScreen = true;
            }
            if (npc2.GetComponent<NPC_Manager>().isInScreen == false)
            {
                npc2.GetComponent<NPC_Manager>().isInScreen = true;
            }

            //people might board and deboard
            if (npc1.GetComponent<NPC_Manager>().state == npcState.deboard)
            {
                npc1.GetComponent<NPC_Manager>().NPC_Face.SetActive(false);
                npc1.GetComponent<NPC_Manager>().NPC_Hand.SetActive(false);
                npc1.GetComponent<NPC_Manager>().isInScreen = false;
                npc1.GetComponent<NPC_Manager>().state = npcState.offScreen;
            }
            if (npc2.GetComponent<NPC_Manager>().state == npcState.deboard)
            {
                npc2.GetComponent<NPC_Manager>().NPC_Face.SetActive(false);
                npc2.GetComponent<NPC_Manager>().NPC_Hand.SetActive(false);
                npc2.GetComponent<NPC_Manager>().isInScreen = false;
                npc2.GetComponent<NPC_Manager>().state = npcState.offScreen;
            }
            timeAlarm = 0;
            which = false;
        }
        else if (timeAlarm2 >= Time.deltaTime) // waiting between stops
        {
            // play sound for closing doors -------------------------------------
            AudioManager.instance.Stop.Play();

            if (currentStop == stops) // the game ends
            {
                //close the NPC's
                npc1.GetComponent<NPC_Manager>().NPC_Face.SetActive(false);
                npc1.GetComponent<NPC_Manager>().NPC_Hand.SetActive(false);
                npc1.GetComponent<NPC_Manager>().state = npcState.offScreen;

                npc2.GetComponent<NPC_Manager>().NPC_Face.SetActive(false);
                npc2.GetComponent<NPC_Manager>().NPC_Hand.SetActive(false);
                npc2.GetComponent<NPC_Manager>().state = npcState.offScreen;

                //go to end screen
                GameManager.instance.GameEnd(true);
                gameEnded = true;
            }
            
            timeAlarm2 = 0;
            which = true;
        }
    }
}
