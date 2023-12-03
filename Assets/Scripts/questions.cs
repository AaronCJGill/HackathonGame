using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class questions : MonoBehaviour
{
    public string question;
    public List<string> answerStrings = new List<string>();
    public answer correctAnswer;
    public answer answerSelected;
    public GameObject self;
    public GameObject image;
    public int questionNum = 1;
    public Sprite img;

    public void Start()
    {
        // put all the elements into the object
        self.transform.GetChild(0).GetComponent<TMP_Text>().text = questionNum + ") " + question;
        for (int i = 0; i < answerStrings.Count; i++)
        {
            self.transform.GetChild(i+1).transform.GetChild(1).GetComponent<TMP_Text>().text = answerStrings[i];
        }
        //change the size so it fits
        self.GetComponent<RectTransform>().sizeDelta = new Vector2(880, 480);

        if (img)
        {
            image.SetActive(true);
            image.GetComponent<Image>().sprite = img;
            //hopefully this works, pray to god
            image.GetComponent<Image>().rectTransform.sizeDelta =
                    new Vector2(200,
                    260 * img.bounds.size.y / img.bounds.size.x);
        }
    }

    public void answerSelect()
    {
        for (int i = 0; i < 3; i++) // see if theres already and answer thats been selected
        {
            if (self.transform.GetChild(i + 1).transform.GetChild(0).GetComponent<answerButton>().isSelected == true)
            {
                self.transform.GetChild(i + 1).transform.GetChild(0).GetComponent<answerButton>().transform.GetChild(0).GetComponent<TMP_Text>().text = "";
                self.transform.GetChild(i + 1).transform.GetChild(0).GetComponent<answerButton>().isSelected = false;
            }
        }
    }
}
