using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRollController : MonoBehaviour
{
    //�t�F�[�h�C��
    [SerializeField] Image FadeImage;
    float fadeAlpha = 1;

    //�G���h���[��
    [SerializeField] Text endroll;
    float textAlpha = 0;
    [SerializeField] RectTransform text; //�e�L�X�g�ړ��p
    [SerializeField] float rollSpeed;
    [SerializeField] float limit;

    //�u�N���b�N�Ń^�C�g���ɖ߂�v
    [SerializeField] Text explain;
    float explainAlpha = 0;

    //�N���A�^�C���@���C���Q�[������擾�\��
    float clearTime = 0;

    bool isOut;
    bool isSkip;

    enum STATE { IN = 0, TEXT, STAFROLL, EXPLAIN, OUT };
    STATE state = 0;

    void Start()
    {
        state = 0;
        isOut = false;
        isSkip = false;
        fadeAlpha = 1;
        textAlpha = 0;
        explainAlpha = 0;
        clearTime = 0;

        endroll.text = 
        "�O�͂܂���J�ł������A�����֌W����܂���ł����B" +
        "\n\n���͕K���ɑ����āA�C���t�������������̑O�ɂ��܂����B" +
        "\n\n����ȗ��������̋A�蓹�͎g���Ă��܂���B" +
        "\n\n���̔p�ЂƉ����������͈�̉��������̂ł��傤�B\n\n\n\n\n\n\n\n\n\n\n\n" +
        "�v�����i�[\n\n�����@��\n\n\n\n" +
        "�v���O���}�[\n\n�R��@����\n\n����@����\n\n����@�V�l\n\n\n\n" +
        "2D�f�U�C�i�[\n\n���q�@����\n\n�����q�@�v�l\n\n\n\n" +
        "3D�f�U�C�i�[\n\n�}��@��l\n\n�����@�C�x\n\n\n\n\n\n\n\n\n\n\n\n" +
        "�N���A�^�C��\n\n" + clearTime;
    }

    void Update()
    {
        //���N���b�N�ŃG���h���[���X�L�b�v
        if (Input.GetMouseButtonDown(0) && state != STATE.IN)
        {
            isSkip = true;
        }

        if (isSkip)
        {
            text.localPosition = new Vector3(0f, 1500f, 0f);
            state = STATE.EXPLAIN;
        }

        //���ڃN���b�N�Ń^�C�g���֑J��
        if (Input.GetMouseButtonDown(0) && isSkip)
        {
            StartCoroutine("wait");
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
            state = STATE.TEXT;
        }
    }

    //�G�s���[�O�\��
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

    //�X�^�b�t���[��
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

    //�u�N���b�N�Ń^�C�g���ɖ߂�v
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

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        isOut = true;
    }
}
