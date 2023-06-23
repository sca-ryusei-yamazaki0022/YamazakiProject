using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleAnimController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("highlighted", false);
        anim.SetBool("selected", false);
        anim.SetBool("pushed", false);
    }

    void Update()
    {
        
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
        anim.SetBool("pushed", true);
    }

    public void SceneChange()
    {
        GameManager.count += 1;
        SceneManager.LoadScene("OpeningScene");
    }
}
