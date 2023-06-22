using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRollController : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;
    [SerializeField] Text endroll;
    float textAlpha = 0;
    [SerializeField] RectTransform text;
    [SerializeField] Text explain;
    float explainAlpha = 0;
    [SerializeField] float rollSpeed;
    [SerializeField] float limit;
    float clearTime = 0;

    enum STATE { IN = 0, TEXT, STAFROLL, EXPLAIN, OUT };
    STATE state = 0;

    bool isOut;

    void Start()
    {
        state = 0;
        isOut = false;
        fadeAlpha = 1;
        textAlpha = 0;
        explainAlpha = 0;
        clearTime = 0;

        endroll.text = 
        "外はまだ大雨でしたが、もう関係ありませんでした。" +
        "\n\n私は必死に走って、気が付いたらもう自宅の前にいました。" +
        "\n\nあれ以来あそこの帰り道は使っていません。" +
        "\n\nあの廃墟と化け物たちは一体何だったのでしょう。\n\n\n\n\n\n\n\n\n\n\n\n" +
        "プランナー\n\n佐藤　陸\n\n\n\n" +
        "プログラマー\n\n山崎　流聖\n\n中野　綾女\n\n猪野　天斗\n\n\n\n" +
        "2Dデザイナー\n\n長倉　愛華\n\n安孫子　要人\n\n\n\n" +
        "3Dデザイナー\n\n笠井　郁斗\n\n八巻　佑駿\n\n\n\n\n\n\n\n\n\n\n\n" +
        "クリアタイム\n\n" + clearTime;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && state != STATE.IN)
        {
            isOut = true;
        }

        if (isOut)
        {
            if (fadeAlpha <= 1)
            {
                fadeAlpha += 0.3f * Time.deltaTime;
                FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
            }
            if (fadeAlpha >= 1)
            {
                SceneManager.LoadScene("TitleScene");
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

    void IN()
    {
        if (fadeAlpha >= 0)
        {
            fadeAlpha -= 0.25f * Time.deltaTime;
            FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
        }
        if (fadeAlpha <= 0)
        {
            state = STATE.TEXT;
        }
    }

    void TEXT()
    {
        if (textAlpha <= 1)
        {
            textAlpha += 1f * Time.deltaTime;
            endroll.GetComponent<Text>().color = new Color(255, 255, 255, textAlpha);
        }
        if (textAlpha >= 1)
        {
            StartCoroutine("waitTime");
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3);
        state = STATE.STAFROLL;
    }

    void STAFROLL()
    {
        if (text.position.y <= limit)
        {
            text.position += new Vector3(0f, rollSpeed * Time.deltaTime, 0f);
        }
        if(text.position.y >= limit)
        {
            state = STATE.EXPLAIN;
        }
    }

    void EXPLAIN()
    {
        if (explainAlpha <= 1)
        {
            explainAlpha += 1f * Time.deltaTime;
            explain.GetComponent<Text>().color = new Color(255, 255, 255, explainAlpha);
        }
        if (Input.GetMouseButton(0))
        {
            state = STATE.OUT;
        }
    }
}
