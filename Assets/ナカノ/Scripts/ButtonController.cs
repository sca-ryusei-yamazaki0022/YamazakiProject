using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
