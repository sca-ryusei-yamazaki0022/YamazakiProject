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

    //�ݒ��ʂ�
    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    //�^�C�g����
    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //�A�v���I��
    public void OnExit()
    {
        Application.Quit();
    }
}