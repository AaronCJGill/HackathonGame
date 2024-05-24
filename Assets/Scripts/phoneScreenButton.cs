using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneScreenButton : MonoBehaviour
{
    public static phoneScreenButton instance;

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
    public GameObject trivia;
    public GameObject blank;
    public GameObject startScreen;
    public bool phoneOpen = true;

    public void Start()
    {
        trivia.SetActive(false);
        blank.SetActive(false);
        startScreen.SetActive(true);
        phoneOpen = true;
    }

    bool firstTimeClicked = false;
    public GameObject Highlight;
    float opacity = 1;
    float opacityAdd = -.004f;
    public void Update()
    {
        // change the alpha
        if (firstTimeClicked == false)
        {
            //Debug.Log(opacity);
            opacity += opacityAdd;
            Highlight.GetComponent<Image>().color = new Color(255, 255, 255, opacity);

            if (opacity <= 0 || opacity >= 1)
            {
                opacityAdd = -opacityAdd;
            }
        }
    }

    public void ButtonClick()
    {
        if (firstTimeClicked == false)
        {
            firstTimeClicked = true;
            Highlight.SetActive(false);
        }
            

         //start playing button press -------------------------------
         AudioManager.instance.PhoneOff.Play();

        if (phoneOpen == true)
        {
            trivia.SetActive(false);
            startScreen.SetActive(false);
            blank.SetActive(true);
            phoneOpen = false;
            GameManager.instance.Password = "";
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
