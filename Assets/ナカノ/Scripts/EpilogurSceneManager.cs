using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EpilogurSceneManager : MonoBehaviour
{
    [SerializeField] Text epilogue;
    [SerializeField] float fadeinSpeed;
    [SerializeField] float waitTime;
    float alpha;
    bool fadein, fadeout;

    void Start()
    {
        alpha = 0;
        epilogue.color = new Color(1, 1, 1, alpha);
        fadein = true;
        fadeout = false;
    }

    void Update()
    {
        if(fadein)
        {
            alpha += fadeinSpeed * Time.deltaTime;
            epilogue.color = new Color(1, 1, 1, alpha);
            if(alpha >= 1)
            {
                fadein = false;
                StartCoroutine("Wait");
            }
        }

        if(fadeout)
        {
            alpha -= fadeinSpeed * Time.deltaTime;
            epilogue.color = new Color(1, 1, 1, alpha);
            if(alpha <= 0)
            {
                fadeout = false;
                StartCoroutine("SceneChange");
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        fadeout = true;
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ClearScene");
    }
}
