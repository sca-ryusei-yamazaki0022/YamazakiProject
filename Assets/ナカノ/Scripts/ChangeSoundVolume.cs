using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolume : MonoBehaviour
{
    //AudioSourceが付いているものに付与して音量変更

    float volume;
    AudioSource audioSource;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume") / 100;

        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume *= volume;
    }
}
