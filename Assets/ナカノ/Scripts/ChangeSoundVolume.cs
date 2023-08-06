using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolume : MonoBehaviour
{
    //AudioSource‚ª•t‚¢‚Ä‚¢‚é‚à‚Ì‚É•t—^‚µ‚Ä‰¹—Ê•ÏX

    float volume;
    AudioSource audioSource;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume") / 100;

        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume *= volume;
    }
}
