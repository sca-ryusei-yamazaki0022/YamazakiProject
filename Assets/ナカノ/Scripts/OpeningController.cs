using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningController : MonoBehaviour
{
    //フェードイン
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;

    //プロローグ
    [SerializeField] Text prologue;
    float textAlpha = 0;

    //「クリックでスタート」
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

        FadeImage.color = new Color(0, 0, 0, fadeAlpha);
        prologue.color = new Color(255, 255, 255, textAlpha);
        explain.color = new Color(255, 255, 255, explainAlpha);
    }

    void Update()
    {
        //クリックでメインゲームに遷移
        if(Input.GetMouseButtonDown(0) && state != STATE.IN)
        {
            isOut = true;
        }

        if(isOut)
        {
            //フェードアウト
            if (fadeAlpha <= 1)
            {
                fadeAlpha += 0.3f * Time.deltaTime;
                FadeImage.color = new Color(0, 0, 0, fadeAlpha);
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

    //フェードイン
    void IN()
    {
        if (fadeAlpha >= 0)
        {
            fadeAlpha -= 0.25f * Time.deltaTime;
            FadeImage.color = new Color(0, 0, 0, fadeAlpha);
        }
        if(fadeAlpha <= 0)
        {
            state = STATE.TEXT;
        }
    }

    //プロローグ表示
    void TEXT()
    {
        if(textAlpha <= 1)
        {
            textAlpha += 1f * Time.deltaTime;
            prologue.color = new Color(255, 255, 255, textAlpha);
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

    //「クリックでスタート」
    void EXPLAIN()
    {
        if (explainAlpha <= 1)
        {
            explainAlpha += 1f * Time.deltaTime;
            explain.color = new Color(255, 255, 255, explainAlpha);
        }
        if(Input.GetMouseButton(0))
        {
            state = STATE.OUT;
        }
    }
}
