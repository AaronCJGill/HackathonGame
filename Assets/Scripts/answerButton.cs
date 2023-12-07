using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum answer { a, b, c, none }
public class answerButton : MonoBehaviour
{
    public answer answr;
    public GameObject self;
    public GameObject mom;
    public bool isSelected = false;

    public void buttonClick()
    {
        if (isSelected == false) // select this{}
        {
            mom.GetComponent<questions>().answerSelect();
            mom.GetComponent<questions>().answerSelected = answr;
            //change the image of the button
            self.transform.GetChild(0).GetComponent<TMP_Text>().text = "X";
            //change the selected answer from the question script
            mom.GetComponent<questions>().answerSelect();
            isSelected = true;
        }
        else
        {
            //change the image of the button
            self.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            //change the selected answer from the question script
            mom.GetComponent<questions>().answerSelected = answer.none;
            isSelected = false;
        }
    }
}
