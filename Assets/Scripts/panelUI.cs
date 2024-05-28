using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class panelUI : MonoBehaviour
{
    public GameObject Panel;
    
    /// OPEN PANEL
    public void OpenPanel()
    {
        if(Panel != null)
        {
            //toggle visibility
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}

    


