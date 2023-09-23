using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolume : MonoBehaviour
{
    //AudioSourceÇ™ïtÇ¢ÇƒÇ¢ÇÈÇ‡ÇÃÇ…ïtó^ÇµÇƒâπó ïœçX

    float volume;
    AudioSource audioSource;
    float defaultvolume;
    float lastVolume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume");

        audioSource = this.GetComponent<AudioSource>();
        defaultvolume = audioSource.volume;
        lastVolume = audioSource.volume;
        audioSource.volume = audioSource.volume * volume / 100;
    }

    void Update()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume");
        if(volume != lastVolume)
        {
            audioSource = this.GetComponent<AudioSource>();
            lastVolume = volume;
            audioSource.volume = defaultvolume * volume / 100;
        }
    }
}
