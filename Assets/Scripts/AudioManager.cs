using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

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
    }

    public AudioSource Ambiance;
    public AudioSource HeavyBreathing;
    public AudioSource PhoneOff;
    public AudioSource WrongPassword;
    public AudioSource PhoneTouchCode;
    public AudioSource Stop;
    public AudioSource Slap;
}
