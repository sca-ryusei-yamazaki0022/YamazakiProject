using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleAnimController : MonoBehaviour
{
    Animator anim;

    bool isPush;
    float tmp = 1;
    [SerializeField] float zoomSpeed;
    [SerializeField] float acceleration;
    [SerializeField] GameObject title;
    [SerializeField] float waitTime;
    AudioSource audioSource;
    [SerializeField] AudioSource zoomSound;
    [SerializeField] AudioSource openSound;
    bool isPlay;

    [SerializeField] GameObject setting;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject text;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("highlighted", false);
        anim.SetBool("selected", false);
        anim.SetBool("pushed", false);
        isPush = false;
        audioSource = this.GetComponent<AudioSource>();
        isPlay = true;
        setting.SetActive(true);
        exit.SetActive(true);
        text.SetActive(true);
    }

    void Update()
    {
        if (isPush)
        {
            setting.SetActive(false);
            exit.SetActive(false);
            text.SetActive(false);
            if (isPlay)
            {
                zoomSound.Play();
                isPlay = false;
            }
            tmp += acceleration;
            zoomSpeed *= (1 + tmp * Time.deltaTime);
            if (title.transform.localScale.x < 380)
            {
                title.transform.localScale += new Vector3(zoomSpeed * Time.deltaTime, zoomSpeed * Time.deltaTime, 0f);
            }
            else if (title.transform.localScale.x >= 380)
            {
                title.transform.localScale = new Vector3(380, 380, 1);
                StartCoroutine(SceneChange());
            }
        }
    }

    public void OnMouse()
    {
        anim.SetBool("highlighted", true);
        if(!isPush)
        {
            openSound.Play();
        }
    }

    public void OutMouse()
    {
        if(!isPush)
        {
            anim.SetBool("highlighted", false);
        }
    }

    public void Push()
    {
        isPush = true;
    }

    IEnumerator SceneChange()
    {
        GameManager.count += 1;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("OpeningScene");
    }
}
