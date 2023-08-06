using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolume : MonoBehaviour
{
    //AudioSource���t���Ă�����̂ɕt�^���ĉ��ʕύX

    float volume;
    AudioSource audioSource;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SoundVolume") / 100;

        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume *= volume;
    }
}
