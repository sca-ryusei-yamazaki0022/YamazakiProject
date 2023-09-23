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
    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeColor;

    //UI�S��
    [SerializeField] Transform UI;
    [SerializeField] Vector3 UIscale;

    //��
    [SerializeField] float moveSpeed;
    Vector3 move;
    [SerializeField] float eyeOpenSpeed;
    [SerializeField] AudioClip eyeSound;
    AudioSource eyeSoundSource;

    //��
    [SerializeField] GameObject Mirror;
    [SerializeField] Transform MirrorPos;

    Animator eyeMoveAnim;

    //�{�^��
    [SerializeField] GameObject EndButton;
    [SerializeField] GameObject RetryButton;

    //UI�S�̂̊g��
    [SerializeField] float zoomSpeed;
    [SerializeField] float acceleration;
    float tmp = 1;
    [SerializeField] GameObject zoomSound;
    AudioSource zoomSource;
    bool isOpenSound;

    enum STATE { IN = 0, OPEN,  WAIT, R_ON, R_OUT, R_PUSH, E_ON, E_OUT, E_PUSH};
    STATE state = 0;

    void Start()
    {
        Cursor.visible = true;

        state = 0;
        FadeImage.enabled = true;
        fadeAlpha = 1;
        FadeImage.color = new Color(fadeColor, fadeColor, fadeColor, fadeAlpha);

        UI.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        move = new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        zoomSource = zoomSound.GetComponent<AudioSource>();
        eyeSoundSource = this.GetComponent<AudioSource>();
        isOpenSound = true;

        eyeMoveAnim = Mirror.GetComponent<Animator>();

        MirrorPos.localScale = new Vector3(2.8f, 0f, 2.8f);
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
            fadeAlpha -= fadeSpeed * Time.deltaTime;
            FadeImage.color = new Color(fadeColor, fadeColor, fadeColor, fadeAlpha);
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
            fadeAlpha += fadeSpeed * Time.deltaTime;
            FadeImage.color = new Color(fadeColor, fadeColor, fadeColor, fadeAlpha);
        }
        if (fadeAlpha >= 1)
        {
            //Application.Quit();
            SceneManager.LoadScene("TitleScene");
        }
    }

    //�ڂ��J��
    void Open()
    {
        if(isOpenSound)
        {
            eyeSoundSource.PlayOneShot(eyeSound);
            isOpenSound = false;
        }
        if(MirrorPos.localScale.y < 2.8f)
        {
            MirrorPos.localScale += new Vector3(0, eyeOpenSpeed * Time.deltaTime, 0);
        }
        else { MirrorPos.localScale = new Vector3(0, 2.8f, 0); state = STATE.WAIT; }
    }

    //�{�^���ɐG�ꂽ��/���ꂽ��/���������̏���
    void R_on()
    {
        eyeMoveAnim.SetBool("RightMove", true);
        eyeMoveAnim.SetBool("RightBack", false);
    }

    void R_out()
    {
        eyeMoveAnim.SetBool("RightMove", false);
        eyeMoveAnim.SetBool("RightBack", true);
    }

    void R_push()
    {
        eyeMoveAnim.SetBool("RightMove", false);
        eyeMoveAnim.SetBool("RightBack", false);
        eyeMoveAnim.SetBool("Break", true);
        RetryButton.SetActive(false);
        EndButton.SetActive(false);
        StartCoroutine("Retry");
    }

    void E_on()
    {
        eyeMoveAnim.SetBool("LeftMove", true);
        eyeMoveAnim.SetBool("LeftBack", false);
    }

    void E_out()
    {
        eyeMoveAnim.SetBool("LeftMove", false);
        eyeMoveAnim.SetBool("LeftBack", true);
    }

    void E_push()
    {
        eyeMoveAnim.SetBool("LeftMove", false);
        eyeMoveAnim.SetBool("LeftBack", false);
        eyeMoveAnim.SetBool("Default", true);
        RetryButton.SetActive(false);
        EndButton.SetActive(false);
        StartCoroutine("EyeClose");
    }

    public void ZoomSound()
    {
        StartCoroutine(Zoom());
    }

    IEnumerator Zoom()
    {
        yield return new WaitForSeconds(1f);
        zoomSource.Play();
    }

    //���g���C���̉��o
    IEnumerator Retry()
    {
        yield return new WaitForSeconds(1.0f);

        tmp += acceleration;
        zoomSpeed *= (1 + tmp * Time.deltaTime);
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
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainGame");
    }

    //�I�����̉��o
    IEnumerator EyeClose()
    {
        yield return new WaitForSeconds(0.5f);
        if (MirrorPos.localScale.y > 0)
        {
            MirrorPos.localScale += new Vector3(0, -eyeOpenSpeed * Time.deltaTime, 0);
        }
        else if (MirrorPos.localScale.y <= 0)
        {
            MirrorPos.localScale = new Vector3(0, 0, 0);
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
