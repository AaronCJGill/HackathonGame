using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class panelUI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Title;
    
    /// OPEN PANEL
    public void OpenPanel()
    {
        if(Panel != null)
        {
            //toggle visibility
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        
        if(Title != null)
        {
            //toggle visibility
            bool isActive = Title.activeSelf;
            Title.SetActive(!isActive);
        }
    }
}

    


