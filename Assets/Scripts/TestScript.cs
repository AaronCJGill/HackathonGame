using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolfram.UnityLink;
using Wolfram.NETLink;
public class TestScript : MonoBehaviour
{
    public WolframLanguage wl;
    string s = "DateString[Entity[\"Planet\",\"Venus\"][\"RiseTime\"]]";
    // Start is called before the first frame update
    void Start()
    {
        
        string res = (string) wl.CloudEvaluate(s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
