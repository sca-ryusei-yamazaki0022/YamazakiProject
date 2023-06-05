using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class EnemySound : MonoBehaviour
{

    //�@���g�Ƃ̋������v�Z����^�[�Q�b�g�I�u�W�F�N�g
    [SerializeField]
    private Transform targetObj;
    [SerializeField]
    private Transform PlayerObj;
    //�@������\������e�L�X�gUI
    [SerializeField]
    private Text distanceUI;
   
    [SerializeField] AudioMixer heartAudioMixer_;
    [SerializeField] GameObject PlayerAudio;
    float dis;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = PlayerAudio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //heartAudioMixer_.SetFloat("DistanceSE", 0.5f);
        //�@�������v�Z
        //dis = Vector3.Distance(this.transform.position, targetObj.transform.position);

       
    }
    void FixedUpdate()
    {
        dis = Vector3.Distance(PlayerObj.transform.position, targetObj.transform.position);
        if (distanceUI != null)
        {
            distanceUI.text = dis.ToString("0.00m");
        }
        else
        {
            Debug.Log(dis.ToString("0.00m"));
        }
        MaxVolume();
    }
    void MaxVolume()
    {
        if (dis <= 5) { 
            audioSource.volume = 1.0f;audioSource.pitch = 2.0f;
            heartAudioMixer_.SetFloat("DistanceSE", 0.5f);
        }
        else { 
            IntermediateVolume(); }
    }
    void IntermediateVolume()
    {
        if (dis <= 10) { 
            audioSource.volume = 0.6f; audioSource.pitch = 1.5f;
            heartAudioMixer_.SetFloat("DistanceSE", 0.75f);
        }
        else { 
            MinimumVolume(); }
    }
    void MinimumVolume()
    {
        if (dis <= 15) { 
            audioSource.volume = 0.3f; audioSource.pitch = 1.0f;
            heartAudioMixer_.SetFloat("DistanceSE", 1.0f);
        }
        else {
            audioSource.volume = 0.0f; audioSource.pitch = 0.0f;
            heartAudioMixer_.SetFloat("DistanceSE", 0.0f);
        }
    }
}