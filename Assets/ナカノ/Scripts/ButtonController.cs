using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private void Start()
    {
    }

    //İ’è‰æ–Ê‚Ö
    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    //ƒAƒvƒŠI—¹
    public void OnExit()
    {
        Application.Quit();
    }
}