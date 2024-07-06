using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class passwordButton : MonoBehaviour
{
    public int num;
    private bool del;
    private KeyCode kc => getKey();
    private void Update()
    {
        //check to see if the player has pressed any of the buttons down and enter it as long as 
        if (!del && GameManager.instance.phoneLocked && Input.GetKeyDown(kc))
        {
            numberButtonPressed();
        }
    }

    KeyCode getKey()
    {
        switch (num)
        {
            case 0:
                return KeyCode.Alpha0;
            case 1:
                return KeyCode.Alpha1;
            case 2:
                return KeyCode.Alpha2;
            case 3:
                return KeyCode.Alpha3;
            case 4:
                return KeyCode.Alpha4;
            case 5:
                return KeyCode.Alpha5;
            case 6:
                return KeyCode.Alpha6;
            case 7:
                return KeyCode.Alpha7;
            case 8:
                return KeyCode.Alpha8;
            case 9:
                return KeyCode.Alpha9;
            default:
                return KeyCode.None;
                break;
        }
    }

    public void numberButtonPressed()
    {
        //go and add this num to the password in
        GameManager.instance.Password += num;
        //play sound for touch screen -------------------------------------------------------
        AudioManager.instance.PhoneTouchCode.Play();
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
