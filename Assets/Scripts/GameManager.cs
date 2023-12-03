using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else if (chr == 'a')
        {
            currentSeneToChange = scenesInGame[2];
        }
        else if (chr == 'd')
        {
            currentSeneToChange = scenesInGame[3];
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
    }
}
