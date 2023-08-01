using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSoundFadeOut : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float SoundFadeOutSpeed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
    }

    void Update()
    {
        if (audioSource.volume >= 0)
        {
            audioSource.volume -= SoundFadeOutSpeed * Time.deltaTime;
        }
    }
}
