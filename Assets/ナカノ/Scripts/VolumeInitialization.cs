using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeInitialization : MonoBehaviour
{
    //ボリューム・文字サイズをゲーム起動時に初期化
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
