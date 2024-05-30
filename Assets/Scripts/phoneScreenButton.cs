using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    
    // ---- UI CHANGES
    public GameObject date;
    public GameObject time;
    public GameObject LookUI;
    public Sprite offUI;
    public Sprite downUI;
    // ----

    public void Update()
    {
        // ---- UI CHANGES
        //if phone is on start screen
        if(startScreen.activeSelf == true)
        {
            //update date
            string dateString = string.Format("{0:dddd, MMM d}", System.DateTime.Now);
            date.GetComponent<TMP_Text>().text = dateString;
            //update time
            string timeString = string.Format("{0:h:mm}", System.DateTime.Now);
            time.GetComponent<TMP_Text>().text = timeString;
        }
        // ---- 

        // ---- COMMENTED OUT OPACITY CHANGES
        /*
        float opacity = 1;
        float opacityAdd = -.004f;
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
        */
        // ---- 
    }
    

    public void ButtonClick()
    {
        if (firstTimeClicked == false)
        {
            firstTimeClicked = true;
            //Highlight.SetActive(false);
        }
        
         //start playing button press 
         AudioManager.instance.PhoneOff.Play();
        
        if (phoneOpen == true)
        {
            // ---- UI CHANGES
            //move left
            Highlight.GetComponent<Image>().transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
            //off UI
            LookUI.GetComponent<Image>().sprite = offUI;
            // ----

            trivia.SetActive(false);
            startScreen.SetActive(false);
            blank.SetActive(true);
            phoneOpen = false;
            GameManager.instance.Password = "";
        }
        else
        {
            // ---- UI CHANGES
            //move back
            Highlight.GetComponent<Image>().transform.position += new Vector3(0.1f, 0.0f, 0.0f);
            //down UI
            LookUI.GetComponent<Image>().sprite = downUI;
            // ----

            blank.SetActive(false);
            startScreen.SetActive(true);
            phoneOpen = true;
            phoneOpen = true;
        }
        
    }

}
