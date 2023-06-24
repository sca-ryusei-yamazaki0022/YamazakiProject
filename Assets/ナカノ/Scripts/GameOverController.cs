using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    //フェードイン
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;

    //UI全体
    [SerializeField] Transform UI;
    [SerializeField] Vector3 UIscale;

    //目
    [SerializeField] GameObject Eye;
    [SerializeField] Transform EyePos;
    [SerializeField] float Limit;
    [SerializeField] float moveSpeed;
    Vector3 move;
    [SerializeField] float eyeOpenSpeed;

    //ボタン
    [SerializeField] GameObject EndButton;
    [SerializeField] GameObject RetryButton;

    //UI全体の拡大
    [SerializeField] float zoomSpeed;
    [SerializeField] float acceleration;

    enum STATE { IN = 0, OPEN,  WAIT, R_ON, R_OUT, R_PUSH, E_ON, E_OUT, E_PUSH};
    STATE state = 0;

    void Start()
    {
        state = 0;
        FadeImage.enabled = true;
        fadeAlpha = 1;

        EyePos.localScale = new Vector3(2f, 0f, 2f);
        UI.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        move = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    void Update()
    {
        switch (state)
        {
            case STATE.IN:
                IN();
            break;
            case STATE.OPEN:
                Open();
            break;
            case STATE.WAIT:
            break;
            case STATE.R_ON:
                R_on();
                break;
            case STATE.R_OUT:
                R_out();
                break;
            case STATE.R_PUSH:
                R_push();
                break;
            case STATE.E_ON:
                E_on();
                break;
            case STATE.E_OUT:
                E_out();
                break;
            case STATE.E_PUSH:
                E_push();
                break;
        }
    }

    //フェードイン
    void IN()
    {
        if (fadeAlpha >= 0)
        {
            fadeAlpha -= 0.25f * Time.deltaTime;
            FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
        }
        if (fadeAlpha <= 0)
        {
            FadeImage.enabled = false;
            state = STATE.OPEN;
        }
    }

    //フェードアウト
    void OUT()
    {
        FadeImage.enabled = true;

        if (fadeAlpha <= 1)
        {
            fadeAlpha += 0.25f * Time.deltaTime;
            FadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
        }
        if (fadeAlpha >= 1)
        {
            Application.Quit();
        }
    }

    //目が開く
    void Open()
    {
        if (EyePos.localScale.y <= 2f)
        {
            EyePos.localScale += new Vector3(0, eyeOpenSpeed * Time.deltaTime, 0);
        }
        else { state = STATE.WAIT; }
    }

    //ボタンに触れた時/離れた時/押した時の処理
    void R_on()
    {
        if (EyePos.localPosition.x <= Limit)
        {
            EyePos.localPosition += move;
        }
    }

    void R_out()
    {
        if (EyePos.localPosition.x > 0)
        {
            EyePos.localPosition -= move;
        }
    }

    void R_push()
    {
        EyePos.localPosition = new Vector3(0, -540, 0);
        RetryButton.SetActive(false);
        EndButton.SetActive(false);
        StartCoroutine("Retry");
    }

    void E_on()
    {
        if (EyePos.localPosition.x >= -Limit)
        {
            EyePos.localPosition -= move;
        }
    }

    void E_out()
    {
        if (EyePos.localPosition.x < 0)
        {
            EyePos.localPosition += move;
        }
    }

    void E_push()
    {
        EyePos.localPosition = new Vector3(0, -540, 0);
        RetryButton.SetActive(false);
        EndButton.SetActive(false);
        StartCoroutine("EyeClose");
    }

    //リトライ時の演出
    IEnumerator Retry()
    {
        yield return new WaitForSeconds(0.5f);
        zoomSpeed *= (1 + acceleration * Time.deltaTime);
        if (EyePos.localScale.x <= 3.5)
        {
            EyePos.localScale += new Vector3(10f * Time.deltaTime, 10f * Time.deltaTime, 0f);
        }
        if(UI.localScale.y < UIscale.y)
        {
            UI.localScale += new Vector3(zoomSpeed * Time.deltaTime, zoomSpeed * Time.deltaTime, 0f);
            UI.localPosition += new Vector3(0, zoomSpeed * 580 * Time.deltaTime, 0);

        }
        else if(UI.localScale.y >= UIscale.y)
        {
            StartCoroutine("SceneChange");
        }
    }

    //メインゲームへ遷移
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainGame");
    }

    //終了時の演出
    IEnumerator EyeClose()
    {
        yield return new WaitForSeconds(0.5f);
        if (EyePos.localScale.y > 0)
        {
            EyePos.localScale += new Vector3(0, -eyeOpenSpeed * Time.deltaTime, 0);
        }
        else if (EyePos.localScale.y <= 0)
        {
            EyePos.localScale = new Vector3(0, 0, 0);
            OUT();
        }
    }

    //他スクリプトから呼ぶ関数
    public void RetryOn()
    {
        if(state != STATE.IN && state != STATE.OPEN)
        state = STATE.R_ON;
    }

    public void RetryOut()
    {
        if (state != STATE.IN && state != STATE.OPEN)
        state = STATE.R_OUT;
    }

    public void RetryPush()
    {
        if (state != STATE.IN && state != STATE.OPEN)
        state = STATE.R_PUSH;
    }

    public void EndOn()
    {

        if (state != STATE.IN && state != STATE.OPEN)
        state = STATE.E_ON;
    }

    public void EndOut()
    {
        if (state != STATE.IN && state != STATE.OPEN)
        state = STATE.E_OUT;
    }

    public void EndPush()
    {
        if (state != STATE.IN && state != STATE.OPEN)
        state = STATE.E_PUSH;
    }
}
