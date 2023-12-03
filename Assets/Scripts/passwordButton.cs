using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class passwordButton : MonoBehaviour
{
    public int num;

    public void numberButtonPressed()
    {
        //go and add this num to the password in
        GameManager.instance.Password += num;
        if (GameManager.instance.Password.Length != 4)
        {
            GameManager.instance.passwordListIndex++;
            GameManager.instance.passwordImage.GetComponent<Image>().sprite = GameManager.instance.passwordSprites[GameManager.instance.passwordListIndex];
        }
        else
        {
            GameManager.instance.passwordListIndex = 0;
            GameManager.instance.CheckPassword();
        }
    }

    public void deleteNum()
    {
        GameManager.instance.Password = GameManager.instance.Password.Remove(-1);
    }
}
