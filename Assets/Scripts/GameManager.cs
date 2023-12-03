using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public List<GameObject> scenesInGame = new List<GameObject>();
    public float mainXCoordinate;
    public float mainYCoordinate;

    public NPCBehavior.activeScreen activeScreen = NPCBehavior.activeScreen.down;

    public string Password;
    public int correctPassword = 5234;
    public GameObject passwordImage;
    public List<Sprite> passwordSprites = new List<Sprite>();
    public int passwordListIndex;

    [SerializeField]
    private GameObject playerSlapObj;
    [SerializeField]
    public Transform playerSlapParent;

    void Start()
    {
        mainXCoordinate = scenesInGame[0].GetComponent<scene>().xCoordinateInitial;
        mainYCoordinate = scenesInGame[0].GetComponent<scene>().yCoordinateInitial;
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
    }

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
            //play sound
        }
        Password = "";
    }

    public GameObject content;
    public int score;
    public int totalScore;

    public GameObject EndScene;
    public void GameEnd()
    {
        //calculate the score
        totalScore = content.transform.childCount;
        for (int i = 0; i < totalScore; i++)
        {
            if (content.transform.GetChild(i).GetComponent<questions>().answerSelected == content.transform.GetChild(i).GetComponent<questions>().correctAnswer)
                score++;
        }

        EndScene.SetActive(true);
        EndScene.transform.GetChild(1).GetComponent<TMP_Text>().text += score + "/" + totalScore;
    }

    public void playerSlap()
    {
        Instantiate(playerSlapObj, playerSlapParent);
    }


}
