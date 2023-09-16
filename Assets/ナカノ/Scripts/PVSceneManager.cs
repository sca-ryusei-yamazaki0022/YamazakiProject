using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PVSceneManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeSpeed;
    float fadeAlpha;

    delegate void PVScene();
    PVScene pvScene;

    void Start()
    {
        fadeAlpha = 1;
        fadeImage.color = new Color(0, 0, 0, fadeAlpha);

        pvScene = fadeIn;
    }

    void Update()
    {
        pvScene();
    }

    void fadeIn()
    {
        if (fadeAlpha >= 0)
        {
            fadeAlpha -= fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
        }
        if (fadeAlpha <= 0)
        {
            pvScene = pvStart;
        }
    }

    void pvStart()
    {
        videoPlayer.Play();

        if (Input.GetMouseButton(0))
        {
            pvScene = fadeOut;
        }
    }

    void fadeOut()
    {
        if (fadeAlpha <= 1)
        {
            fadeAlpha += fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
        }
        if (fadeAlpha >= 1)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
