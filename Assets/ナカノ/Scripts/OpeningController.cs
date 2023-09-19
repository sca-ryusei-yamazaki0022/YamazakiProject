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
    [SerializeField] float fadeSpeed;
    [SerializeField] int fadeInColor;
    [SerializeField] int fadeOutColor;

    //プロローグ
    [SerializeField] Text prologue;
    [SerializeField] string[] prologueTexts;
    int num = 0;
    float textAlpha = 0;
    bool isFadein, isFadeout;

    //「クリックでスタート」
    [SerializeField] Text explain;
    float explainAlpha = 0;

    enum STATE { IN = 0, TEXT, EXPLAIN, OUT};
    STATE state = 0;

    bool isOut;

    //BGM
    AudioSource audioSource;
    [SerializeField] float SoundFadeInSpeed;
    [SerializeField] float SoundFadeOutSpeed;
    bool isSoundFadeIn;

    /*帰り道、突如大雨に見舞われた私は雨宿りのため
近くにあった廃墟で雨宿りすることにしました
不自然なくらいに古い廃墟
はじめは玄関で雨がやむのを待っていたのですが
つい気になって中を覗いてみることにしたのです*/

    void Start()
    {
        state = 0;
        isOut = false;
        fadeAlpha = 1;
        textAlpha = 0;
        explainAlpha = 0;
        isFadein = true;
        isFadeout = false;
        num = 0;

        FadeImage.color = new Color(fadeInColor, fadeInColor, fadeInColor, fadeAlpha);
        prologue.color = new Color(255, 255, 255, textAlpha);
        explain.color = new Color(255, 255, 255, explainAlpha);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        isSoundFadeIn = true;
    }

    void Update()
    {
        //クリックでメインゲームに遷移
        if(Input.GetMouseButtonDown(0) && state != STATE.IN && state != STATE.TEXT)
        {
            isOut = true;
        }

        if(isOut)
        {
            //フェードアウト
            if (fadeAlpha <= 1)
            {
                fadeAlpha += fadeSpeed * Time.deltaTime;
                FadeImage.color = new Color(fadeOutColor, fadeOutColor, fadeOutColor, fadeAlpha);
            }
            if (fadeAlpha >= 1)
            {
                SceneManager.LoadScene("MainGame");
            }

            //BGMフェードアウト
            if (audioSource.volume >= 0)
            {
                isSoundFadeIn = false;
                audioSource.volume -= SoundFadeOutSpeed * Time.deltaTime;
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
            fadeAlpha -= fadeSpeed * Time.deltaTime;
            FadeImage.color = new Color(fadeInColor, fadeInColor, fadeInColor, fadeAlpha);
        }
        if(fadeAlpha <= 0)
        {
            state = STATE.TEXT;
        }
    }

    //プロローグ表示
    void TEXT()
    {
        //BGMフェードイン
        if (audioSource.volume <= 1 * PlayerPrefs.GetFloat("SoundVolume") / 100 && isSoundFadeIn)
        {
            audioSource.volume += SoundFadeInSpeed * Time.deltaTime;
        }

        prologue.text = prologueTexts[num];

        if (isFadein)
        {
            if (textAlpha <= 1)
            {
                textAlpha += 0.5f * Time.deltaTime;
                prologue.color = new Color(255, 255, 255, textAlpha);

                if (Input.GetMouseButtonDown(0) && num < prologueTexts.Length - 1)
                {
                    isFadein = false;
                    isFadeout = true;
                }
            }
        }

        if (isFadeout)
        {
            if (textAlpha >= 0)
            {
                textAlpha -= 0.5f * Time.deltaTime;
                prologue.color = new Color(255, 255, 255, textAlpha);
            }
            if (textAlpha <= 0)
            {
                isFadeout = false;
                isFadein = true;
                num++;
            }
        }

        if (num == prologueTexts.Length - 1)
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
