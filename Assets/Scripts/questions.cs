using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class questions : MonoBehaviour
{
    public string question;
    public List<string> answerStrings = new List<string>();
    public answer correctAnswer;
    public answer answerSelected;
    public GameObject self;

    public void Start()
    {
        // put all the elements into the object
        self.transform.GetChild(0).GetComponent<TMP_Text>().text = question;
        for (int i = 0; i < answerStrings.Count; i++)
        {
            self.transform.GetChild(1).transform.GetChild(i + 1).GetComponent<TMP_Text>().text = answerStrings[i];
        }
    }
}
