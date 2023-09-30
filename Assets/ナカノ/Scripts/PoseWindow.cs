using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseWindow : MonoBehaviour
{
    [SerializeField] GameObject setting, title;
    [SerializeField] Image backGround;

    Animator poseAnim;
    [SerializeField] Image poseBackground;

    [SerializeField] GameObject RestartButton;
    Image restartButtonImage;

    [SerializeField] Sprite[] lightFrame;

    [SerializeField] GameObject SettingWindow;

    [SerializeField] GameObject poseWindow;

    void Start()
    {
        //backGround.enabled = false;
        setting.SetActive(false);
        title.SetActive(false);
        RestartButton.SetActive(false);
        SettingWindow.SetActive(false);

        restartButtonImage = RestartButton.GetComponent<Image>();
        restartButtonImage.color = new Color(255, 255, 255, 0);
        restartButtonImage.sprite = lightFrame[0];

        poseAnim = poseBackground.gameObject.GetComponent<Animator>();
        poseAnim.SetBool("Reduction", false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            poseAnim.SetTrigger("Reduction");
        }
    }

    public void LightOn()
    {
        restartButtonImage.color = new Color(255, 255, 255, 1);
        restartButtonImage.sprite = lightFrame[1];
    } //���ɃJ�[�\���d�Ȃ����Ƃ����C�g��t����

    public void LightOff()
    {
        restartButtonImage.color = new Color(255, 255, 255, 0);
        restartButtonImage.sprite = lightFrame[0];
    }
     
    public void ButtonActive()
    {
        setting.SetActive(true);
        title.SetActive(true);
        RestartButton.SetActive(true);
        backGround.enabled = true;
        Cursor.visible = true;
    }

    public void GameRestart() //�Q�[���ɖ߂�
    {
        backGround.enabled = false;
        setting.SetActive(false);
        title.SetActive(false);
        RestartButton.SetActive(false);
        poseAnim.SetTrigger("Expansion");

        Cursor.visible = false;
    }

    public void PoseDontEnabled() //�Q�[���ɖ߂�Ƃ��A�g��A�j���[�V�����I����A�|�[�Y��ʂ𖳌���
    {
        poseWindow.SetActive(false);
    }

    public void Setting()
    {
        SettingWindow.SetActive(true);
    }

    public void back()
    {
        SettingWindow.SetActive(false);
    }
}
