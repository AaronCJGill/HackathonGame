using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HandScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    NPCBehavior parentNPC;

    private void OnMouseDown()
    {
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("Recoil hand back");
        Invoke("recoilHand", 1f);
    }

    private void recoilHand()
    {
        parentNPC.triggered();
        GameManager.instance.playerSlap();
    }
}
