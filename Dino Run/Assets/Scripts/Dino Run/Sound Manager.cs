using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource jumpSound;

    bool isSFXEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasSFXEnabled()
    { 
        return isSFXEnabled;
    }

    public void SetSFX(bool sfx)
    {
        isSFXEnabled = sfx;
    }

    public void PlayJumpSound()
    {
        if (isSFXEnabled)
        {
            jumpSound.Play();
        }
        
    }
}
