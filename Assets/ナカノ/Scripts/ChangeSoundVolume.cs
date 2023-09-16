using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolume : MonoBehaviour
{
    //AudioSourceが付いているものに付与して音量変更

    float volume;
    AudioSource audioSource;
    float lastVolume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume");

        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume *= volume / 100;
        lastVolume = volume;
    }

    void Update()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume");
        if(volume != lastVolume)
        {
            audioSource = this.GetComponent<AudioSource>();
            audioSource.volume *= volume / 100;
            lastVolume = volume;
        }
    }
}
