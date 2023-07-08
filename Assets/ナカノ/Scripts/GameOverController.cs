using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    //�t�F�[�h�C��
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;

    //UI�S��
    [SerializeField] Transform UI;
    [SerializeField] Vector3 UIscale;

    //��
    [SerializeField] GameObject Eye;
    [SerializeField] Transform EyePos;
    [SerializeField] float Limit;
    [SerializeField] float moveSpeed;
    Vector3 move;
    [SerializeField] float eyeOpenSpeed;

    //��
    [SerializeField] GameObject Mirror;
    [SerializeField, Header("������")] Sprite[] m_Sprite;
    Image m_Image;

    //�{�^��
    [SerializeField] GameObject EndButton;
    [SerializeField] GameObject RetryButton;

    //UI�S�̂̊g��
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

        m_Image = Mirror.GetComponent<Image>();
        m_Image.sprite = m_Sprite[0];

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

    //�t�F�[�h�C��
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

    //�t�F�[�h�A�E�g
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

    //�ڂ��J��
    void Open()
    {
        if (EyePos.localScale.y <= 2f)
        {
            EyePos.localScale += new Vector3(0, eyeOpenSpeed * Time.deltaTime, 0);
        }
        else { state = STATE.WAIT; }
    }

    //�{�^���ɐG�ꂽ��/���ꂽ��/���������̏���
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

    //���g���C���̉��o
    IEnumerator Retry()
    {
        m_Image.sprite = m_Sprite[1];
        yield return new WaitForSeconds(1.5f);
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

    //���C���Q�[���֑J��
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainGame");
    }

    //�I�����̉��o
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

    //���X�N���v�g����ĂԊ֐�
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
