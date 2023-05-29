using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverToTitle : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
