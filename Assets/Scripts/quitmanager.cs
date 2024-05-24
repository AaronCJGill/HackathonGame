using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitmanager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        checkQuit();
    }

    void checkQuit()
    {
        if (Input.GetKey(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
        }
    }
}
