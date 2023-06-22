using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;
    [SerializeField] GameObject Eye;
    [SerializeField] Transform EyePos;
    [SerializeField] float moveSpeed;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject EndButton;
    [SerializeField] GameObject RetryButton;

    enum STATE { IN = 0, WAIT, R_ON, R_OUT, R_PUSH, E_ON, E_OUT, E_PUSH};
    STATE state = 0;

    void Start()
    {
        state = 0;
        FadeImage.enabled = true;
        fadeAlpha = 1;
    }

    void Update()
    {
        switch (state)
        {
            case STATE.IN:
                IN();
            break;
            case STATE.WAIT:
            break;
            case STATE.R_ON:
                if (EyePos.localPosition.x <= 85)
                {
                    EyePos.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                break;
            case STATE.R_OUT:
                if (EyePos.localPosition.x > 0)
                {
                    EyePos.localPosition -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                break;
            case STATE.R_PUSH:
                EyePos.localPosition = new Vector3(0, -135, 0);
                RetryButton.SetActive(false);
                EndButton.SetActive(false);
                break;
            case STATE.E_ON:
                if (EyePos.localPosition.x >= -85)
                {
                    EyePos.localPosition -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                break;
            case STATE.E_OUT:
                if (EyePos.localPosition.x < 0)
                {
                    EyePos.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                break;
            case STATE.E_PUSH:
                EyePos.localPosition = new Vector3(0, -135, 0);
                RetryButton.SetActive(false);
                EndButton.SetActive(false);
                break;
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
            FadeImage.enabled = false;
            state = STATE.WAIT;
        }
    }

    public void RetryOn()
    {
        state = STATE.R_ON;
    }

    public void RetryOut()
    {
        state = STATE.R_OUT;
    }

    public void RetryPush()
    {
        state = STATE.R_PUSH;
    }

    public void EndOn()
    {
        state = STATE.E_ON;
    }

    public void EndOut()
    {
        state = STATE.E_OUT;
    }

    public void EndPush()
    {
        state = STATE.E_PUSH;
    }
}
