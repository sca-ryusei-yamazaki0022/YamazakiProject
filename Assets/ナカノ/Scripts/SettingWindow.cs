using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWindow : MonoBehaviour
{
    [SerializeField] GameObject setting;
    
    public void Setting()
    {
        setting.SetActive(true);
    }

    public void Title()
    {
        setting.SetActive(false);
    }
}
