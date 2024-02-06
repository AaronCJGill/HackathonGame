using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool PotatoMode = false;

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
        //this is spawned in on the loading of the subway scene instead of at start.
        //So on awake we have to get the potatoMode definition from the potatoMode handler which has dontDestroyOnLoad
        
    }

    public List<GameObject> scenesInGame = new List<GameObject>();
    public float mainXCoordinate;
    public float mainYCoordinate;

    public NPCBehavior.activeScreen activeScreen = NPCBehavior.activeScreen.down;

    public string Password;
    public int correctPassword = 5234;
    public GameObject passwordImage;
    public List<Sprite> passwordSprites = new List<Sprite>();
    public int passwordListIndex;

    void Start()
    {
        mainXCoordinate = scenesInGame[0].GetComponent<scene>().xCoordinateInitial;
        mainYCoordinate = scenesInGame[0].GetComponent<scene>().yCoordinateInitial;
        //Debug.Log(mainXCoordinate);
        //Debug.Log(mainYCoordinate);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            getButtonDown('w');
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            getButtonDown('a');
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            getButtonDown('d');
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            getButtonUp('w');
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            getButtonUp('a');
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            getButtonUp('d');
        }
    }

    public GameObject currentSeneToChange;

    void getButtonDown(char chr)
    {
        if (chr == 'w')
        {
            currentSeneToChange = scenesInGame[1];
            activeScreen = NPCBehavior.activeScreen.up;
        }
        else if (chr == 'a')
        {
            currentSeneToChange = scenesInGame[2];
            activeScreen = NPCBehavior.activeScreen.left;
        }
        else if (chr == 'd')
        {
            currentSeneToChange = scenesInGame[3];
            activeScreen = NPCBehavior.activeScreen.right;
        }
        //debugging
        if (chr == 'd')
            Debug.Log("current obj");

        //change the pos of the scenes
        scenesInGame[0].transform.position = new Vector2(0, mainYCoordinate - 100);
        currentSeneToChange.transform.position = new Vector2(mainXCoordinate,mainYCoordinate);

        //change the Xpos of the hands
        npc1HandInitalY = npc1Hand.transform.position.y;
        npc1Hand.transform.position = new Vector2(0, mainYCoordinate - 100);
        npc2HandInitalY = npc2Hand.transform.position.y;
        npc2Hand.transform.position = new Vector2(0, mainYCoordinate - 100);
    }

    public GameObject npc1Hand;
    public GameObject npc2Hand;
    float npc1HandInitalY;
    float npc2HandInitalY;

    void getButtonUp(char chr)
    {
        if (chr == 'w')
        {
            currentSeneToChange = scenesInGame[1];
        }
        else if (chr == 'a')
        {
            currentSeneToChange = scenesInGame[2];
        }
        else if (chr == 'd')
        {
            currentSeneToChange = scenesInGame[3];
        }

        //change the pos of the scenes
        scenesInGame[0].transform.position = new Vector2(mainXCoordinate, mainYCoordinate);
        currentSeneToChange.transform.position =
            new Vector2(currentSeneToChange.GetComponent<scene>().xCoordinateInitial,
            currentSeneToChange.GetComponent<scene>().yCoordinateInitial);

        //reset activeScreen
        activeScreen = NPCBehavior.activeScreen.down;

        //get the hands back!
        npc1Hand.transform.position = new Vector2(0, npc1HandInitalY);
        npc2Hand.transform.position = new Vector2(0, npc2HandInitalY);
    }

    public GameObject trivia;
    public GameObject opneScreen;
    public void CheckPassword()
    {
        passwordImage.GetComponent<Image>().sprite = passwordSprites[passwordListIndex];
        if (correctPassword == Convert.ToInt32(Password))
        {
            trivia.SetActive(true);
            opneScreen.SetActive(false);
        }
        else
        {
            //play wrong password sound -------------------------------------------------------
            AudioManager.instance.WrongPassword.Play();
        }
        Password = "";
    }

    public GameObject content;
    public int score;
    public int totalScore;

    public GameObject EndScene;
    public GameObject BadEndScene;

    public GameObject npc1;
    public GameObject npc2;

    public void GameEnd(bool win) // take a string to write according to win or loss
    {

        // stop all the sounds and objs
        npc1.SetActive(false);
        npc2.SetActive(false);
        AudioManager.instance.Ambiance.Stop();
        AudioManager.instance.HeavyBreathing.Stop();
        AudioManager.instance.PhoneOff.Stop();
        AudioManager.instance.WrongPassword.Stop();
        AudioManager.instance.PhoneTouchCode.Stop();
        AudioManager.instance.Stop.Stop();
        AudioManager.instance.Slap.Stop();

        if (win == true)
        {
            //calculate the score
            totalScore = content.transform.childCount;
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Debug.Log(content.transform.GetChild(i).GetComponent<questions>().answerSelected);
                Debug.Log(content.transform.GetChild(i).GetComponent<questions>().correctAnswer);
                if (content.transform.GetChild(i).GetComponent<questions>().answerSelected == content.transform.GetChild(i).GetComponent<questions>().correctAnswer)
                    score++;
            }

            EndScene.SetActive(true);
            EndScene.transform.GetChild(1).GetComponent<TMP_Text>().text += score + "/" + totalScore;
        }
        else
        {
            BadEndScene.SetActive(true);
        }
        
        timer.instance.gameEnded = true;
    }

    


}
