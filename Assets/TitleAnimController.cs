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

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("highlighted", false);
        anim.SetBool("selected", false);
        anim.SetBool("pushed", false);
        isPush = false;
    }

    void Update()
    {
        if(isPush)
        {
            tmp += acceleration;
            zoomSpeed *= (1 + tmp * Time.deltaTime);
            if (title.transform.localScale.x <= 350)
            {
                title.transform.localScale += new Vector3(zoomSpeed * Time.deltaTime, zoomSpeed * Time.deltaTime, 0f);
            }
            else if (title.transform.localScale.x > 350)
            {
                SceneChange();
            }
        }
    }

    public void OnMouse()
    {
        anim.SetBool("highlighted", true);
    }

    public void OutMouse()
    {
        anim.SetBool("highlighted", false);
    }

    public void Push()
    {
        //anim.SetBool("pushed", true);
        isPush = true;
    }

    public void SceneChange()
    {
        GameManager.count += 1;
        SceneManager.LoadScene("OpeningScene");
    }
}
