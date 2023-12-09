using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PotatoModeHandler : MonoBehaviour
{
    //variable is based on the checkbox in main menu
    //this then moves with the scene change 
    public static PotatoModeHandler instance;
    
    public bool potatomode = false;
    public Toggle toggle;
    [SerializeField]
    GameObject togglePanel;

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
        DontDestroyOnLoad(gameObject);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //togglePanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (toggle != null)
        {
            potatomode = toggle.isOn;
            
        }
    }

}
