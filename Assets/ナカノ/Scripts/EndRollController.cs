using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRollController : MonoBehaviour
{
    //フェードイン
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;
    [SerializeField] float fadeSpeed;
    [SerializeField] int FadeColor;

    //エンドロール
    [SerializeField] Text endroll;
    [SerializeField] Text epilogue;
    [SerializeField] string[] epilogueTexts;
    int textsNum = 0;
    bool isFadein, isFadeout;
    float textAlpha = 0;
    [SerializeField] RectTransform text; //テキスト移動用
    [SerializeField] float rollSpeed;
    [SerializeField] float limit;


    //NewRecord
    [SerializeField] Text newRecord;
    float newRecordAlpha = 0;

    //「クリックでタイトルに戻る」
    [SerializeField] Text explain;
    float explainAlpha = 0;

    //クリアタイム　メインゲームから取得
    float clearTime;
    float fastestTime; //最速タイム
    [SerializeField] Text ClearTime;
    [SerializeField] Text HighScore;
    bool isNewRecord;

    bool isOut;
    bool isSkip;
    bool inoperable;

    enum STATE { IN = 0, TEXT, STAFROLL, EXPLAIN, OUT };
    STATE state = 0;

    //BGM
    AudioSource audioSource;
    [SerializeField] float SoundFadeInSpeed;
    [SerializeField] float SoundFadeOutSpeed;
    bool isSoundFadeIn;

    void Start()
    {
        state = 0;
        isOut = false;
        isSkip = false;
        inoperable = false;

        textsNum = 0;
        isFadein = true;
        isFadeout = false;

        fadeAlpha = 1;
        textAlpha = 0;
        explainAlpha = 0;
        clearTime = 0;

        FadeImage.color = new Color(FadeColor, FadeColor, FadeColor, fadeAlpha);
        epilogue.color = new Color(0, 0, 0, textAlpha);
        explain.color = new Color(255, 255, 255, explainAlpha);
        newRecord.color = new Color(1, 1, 1, newRecordAlpha);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        isSoundFadeIn = true;

        isNewRecord = false;
        clearTime = GameManager.PlayTime;
        fastestTime = PlayerPrefs.GetFloat("FastestTime", 1000000);
        if(clearTime <= fastestTime)
        {
            isNewRecord = true;
            fastestTime = clearTime;
            //PlayerPrefs.DeleteKey("FastestTime");
            PlayerPrefs.SetFloat("FastestTime", fastestTime);
        }
        
        ClearTime.text = TimeDisp(Mathf.Floor(clearTime / 60)) + ":" + TimeDisp(Mathf.Floor(clearTime % 60));
        HighScore.text = TimeDisp(Mathf.Floor(fastestTime / 60)) + ":" + TimeDisp(Mathf.Floor(fastestTime % 60));
    }

    //時間の表示調整
    string TimeDisp(float t)
    {
        string time = "";
        if(t < 10)
        {
            time = "0" + t;
        }
        if(t >= 10)
        {
            time = t + "";
        }
        return time;
    }

    void Update()
    {
        //フェードイン終わった後に一回クリックでエンドロールスキップ
        if (Input.GetMouseButtonDown(0) && state != STATE.IN && state != STATE.TEXT && !isSkip)
        {
            isSkip = true;
        }

        if (isSkip)
        {
            text.localPosition = new Vector3(30f, limit, 0f);
            state = STATE.EXPLAIN;
            isSkip = false;
        }

        if(text.localPosition.y >= limit)
        {
            if(newRecordAlpha <= 1 && isNewRecord)
            {
                newRecordAlpha += fadeSpeed * 2 * Time.deltaTime;
                newRecord.color = new Color(1, 1, 1, newRecordAlpha);
            }
        }

        if (isOut)
        {
            //フェードアウト
            if (fadeAlpha <= 1)
            {
                fadeAlpha += fadeSpeed * Time.deltaTime;
                FadeImage.color = new Color(FadeColor, FadeColor, FadeColor, fadeAlpha);
            }
            if (fadeAlpha >= 1)
            {
                SceneManager.LoadScene("TitleScene");
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
                case STATE.STAFROLL:
                    STAFROLL();
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
            FadeImage.color = new Color(FadeColor, FadeColor, FadeColor, fadeAlpha);
        }
        if (fadeAlpha <= 0)
        {
            state = STATE.TEXT;
        }
    }

    //エピローグ表示
    void TEXT()
    {
        epilogue.text = epilogueTexts[textsNum];

        if (isFadein)
        {
            if (textAlpha <= 1)
            {
                textAlpha += 0.5f * Time.deltaTime;
                epilogue.color = new Color(255, 255, 255, textAlpha);

                if (Input.GetMouseButtonDown(0) && textsNum < epilogueTexts.Length)
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
                epilogue.color = new Color(255, 255, 255, textAlpha);
            }
            if (textAlpha <= 0)
            {
                isFadeout = false;
                isFadein = true;
                textsNum++;
            }
        }

        if (textsNum == epilogueTexts.Length - 1)
        {
            StartCoroutine("waitTime");
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3);
        state = STATE.STAFROLL;
    }

    //スタッフロール
    void STAFROLL()
    {
        if (text.localPosition.y <= limit)
        {
            text.localPosition += new Vector3(0f, rollSpeed * Time.deltaTime, 0f);
        }
        if(text.localPosition.y >= limit)
        {
            state = STATE.EXPLAIN;
        }
    }

    //「クリックでタイトルに戻る」
    void EXPLAIN()
    {
        StartCoroutine("wait");

        if (explainAlpha <= 1)
        {
            explainAlpha += 1f * Time.deltaTime;
            explain.color = new Color(255, 255, 255, explainAlpha);
        }
        
        if (Input.GetMouseButton(0) && inoperable)
        {
            state = STATE.OUT;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        inoperable = true;
    }
}
