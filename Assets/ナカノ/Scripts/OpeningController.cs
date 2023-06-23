using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningController : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;
    [SerializeField] Text prologue;
    float textAlpha = 0;
    [SerializeField] Text explain;
    float explainAlpha = 0;

    enum STATE { IN = 0, TEXT, EXPLAIN, OUT};
    STATE state = 0;

    bool isOut;

    void Start()
    {
        state = 0;
        isOut = false;
        fadeAlpha = 1;
        textAlpha = 0;
        explainAlpha = 0;
}

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && state != STATE.IN)
        {
            isOut = true;
        }

        if(isOut)
        {
            if (fadeAlpha <= 1)
            {
                fadeAlpha += 0.3f * Time.deltaTime;
                FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
            }
            if (fadeAlpha >= 1)
            {
                SceneManager.LoadScene("MainGame");
            }
        }
        else
        {
            switch (state)
            {
                case STATE.IN:
                    IN();
                    break;
                case STATE.TEXT:
                    TEXT();
                    break;
                case STATE.EXPLAIN:
                    EXPLAIN();
                    break;
                case STATE.OUT:
                    isOut = true;
                    break;
            }
        }
    }

    void IN()
    {
        if (fadeAlpha >= 0)
        {
            fadeAlpha -= 0.25f * Time.deltaTime;
            FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
        }
        if(fadeAlpha <= 0)
        {
            state = STATE.TEXT;
        }
    }

    void TEXT()
    {
        if(textAlpha <= 1)
        {
            textAlpha += 1f * Time.deltaTime;
            prologue.GetComponent<Text>().color = new Color(255, 255, 255, textAlpha);
        }
        if(textAlpha >= 1)
        {
            StartCoroutine("waitTime");
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3);
        state = STATE.EXPLAIN;
    }

    void EXPLAIN()
    {
        if (explainAlpha <= 1)
        {
            explainAlpha += 1f * Time.deltaTime;
            explain.GetComponent<Text>().color = new Color(255, 255, 255, explainAlpha);
        }
        if(Input.GetMouseButton(0))
        {
            state = STATE.OUT;
        }
    }
}
