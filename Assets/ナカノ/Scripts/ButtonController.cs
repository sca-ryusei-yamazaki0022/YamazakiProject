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

    //設定画面へ
    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    //タイトルへ
    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //アプリ終了
    public void OnExit()
    {
        Application.Quit();
    }
}