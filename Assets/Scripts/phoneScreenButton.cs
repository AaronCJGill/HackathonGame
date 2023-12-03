using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneScreenButton : MonoBehaviour
{
    public GameObject trivia;
    public GameObject blank;
    public GameObject startScreen;
    public bool phoneOpen = false;

    public void Start()
    {
        trivia.SetActive(false);
        blank.SetActive(false);
        startScreen.SetActive(true);
        phoneOpen = true;
    }

    public void ButtonClick()
    {
        if (phoneOpen == true)
        {
            trivia.SetActive(false);
            startScreen.SetActive(false);
            blank.SetActive(true);
            phoneOpen = false;
        }
        else
        {
            blank.SetActive(false);
            startScreen.SetActive(true);
            phoneOpen = true;
            phoneOpen = true;
        }
        
    }

}
