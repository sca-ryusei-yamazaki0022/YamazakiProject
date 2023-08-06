using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeInitialization : MonoBehaviour
{
    //�{�����[���E�����T�C�Y���Q�[���N�����ɏ�����
    static VolumeInitialization instance;
    [SerializeField] float volumeDefault;
    [SerializeField] float textSizeDefault;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = this;
            PlayerPrefs.SetFloat("SoundVolume", volumeDefault);
            PlayerPrefs.SetFloat("TextSize", textSizeDefault);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
