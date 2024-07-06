using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionrandomizer : MonoBehaviour
{
    //a list of math questions, culture questions, geography questions
    [Header("Questions")]
    [SerializeField]
    private List<GameObject> mathQuestions = new List<GameObject>();
    [SerializeField]
    private List<GameObject> cultureQuestions = new List<GameObject>();
    [SerializeField]
    private List<GameObject> geographyQuestions = new List<GameObject>();
    //number of questions of each to spawn in
    [Header("Amount of Questions")]
    [SerializeField]
    private int amountOfMathQuestions = 4;
    [SerializeField]
    private int amountOfCultureQuestions = 4;
    [SerializeField]
    private int amountOfGeographyQuestions = 4;
    public int totalQuestions => (amountOfGeographyQuestions + amountOfCultureQuestions + amountOfMathQuestions);
    //need a place to put the quiz questions (child named content in viewport)
    [SerializeField]
    Transform QuestionsParent;//this should be named content in our current structure

    public static questionrandomizer instance;
    public void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    public void CreateTest()
    {
        randomizeListOfQuestions();

        //question order should also be randomized
        //have a queue of each question type
        Queue<int> mQ = new Queue<int>();
        for (int i = 0; i < amountOfMathQuestions; i++)
        {
            //Instantiate(mathQuestions[i], QuestionsParent);
            mQ.Enqueue(i);
        }
        Queue<int> cQ = new Queue<int>();
        for (int i = 0; i < amountOfCultureQuestions; i++)
        {
            //Instantiate(mathQuestions[i], QuestionsParent);
            cQ.Enqueue(i);
        }
        Queue<int> gQ = new Queue<int>();
        for (int i = 0; i < amountOfGeographyQuestions; i++)
        {
            //Instantiate(mathQuestions[i], QuestionsParent);
            gQ.Enqueue(i);
        }

        //Have a list of queues that holds all of the questions in a random order
        List<GameObject> loQ = new List<GameObject>();
        List<GameObject> prerandomizedQList = new List<GameObject>();
        for (int i = 0; i < amountOfGeographyQuestions + amountOfCultureQuestions + amountOfMathQuestions; i++)
        {/*
            if (loQ[0].Count == 0)//if any of these queues are depleted then delete their position
                loQ.RemoveAt(0);
            if (loQ[1].Count == 0)//if any of these queues are depleted then delete their position
                loQ.RemoveAt(1);
            if (loQ[2].Count == 0)//if any of these queues are depleted then delete their position
                loQ.RemoveAt(2);
            //randomly choose which item to remove based on the length of the list
            int x = Random.Range(0,loQ.Count);//we have no way of knowing what queue this is
            Instantiate((loQ[x].Dequeue()), QuestionsParent);*/
        }

        //add all of these questions to a list and just shuffle 
        for (int i = 0; i < amountOfMathQuestions; i++)
        {
            loQ.Add(mathQuestions[mQ.Dequeue()]);
        }
        for (int i = 0; i < amountOfGeographyQuestions; i++)
        {
            loQ.Add(geographyQuestions[gQ.Dequeue()]);
        }
        for (int i = 0; i < amountOfCultureQuestions; i++)
        {
            loQ.Add(cultureQuestions[cQ.Dequeue()]);
        }
        loQ = randomizeLOQ(loQ);

        for (int i = 0; i < loQ.Count; i++)
        {
            GameObject g = Instantiate(loQ[i], QuestionsParent);
            if (g.TryGetComponent<questions>(out questions q))
            {
                q.questionNum = i + 1;
            }
        }
    }
    private List<GameObject> randomizeLOQ(List<GameObject> loq)
    {
        for (int i = 0; i < loq.Count; i++)
        {
            int rq = Random.Range(0, loq.Count);
            GameObject cachedQ = loq[rq];
            loq[rq] = loq[i];
            loq[i] = cachedQ;
        }
        return loq;
    }
    private void randomizeListOfQuestions()
    {
        //randomize the list of each 
        for (int i = 0; i < mathQuestions.Count; i++)
        {
            int rq = Random.Range(0, mathQuestions.Count);
            GameObject cachedQ = mathQuestions[rq];
            mathQuestions[rq] = mathQuestions[i];
            mathQuestions[i] = cachedQ;
        }
        for (int i = 0; i < cultureQuestions.Count; i++)
        {
            int rq = Random.Range(0, cultureQuestions.Count);
            GameObject cachedQ = cultureQuestions[rq];
            cultureQuestions[rq] = cultureQuestions[i];
            cultureQuestions[i] = cachedQ;
        }
        for (int i = 0; i < geographyQuestions.Count; i++)
        {
            int rq = Random.Range(0, geographyQuestions.Count);
            GameObject cachedQ = geographyQuestions[rq];
            geographyQuestions[rq] = geographyQuestions[i];
            geographyQuestions[i] = cachedQ;
        }
    }

}
