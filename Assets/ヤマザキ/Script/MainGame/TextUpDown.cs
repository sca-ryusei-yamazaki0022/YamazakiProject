using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class TextUpDown : MonoBehaviour
{
    private Animator Panime;
    private Animator Panime1;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject S;
    [SerializeField] private GameObject Text;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Panime = GameObject.Find("TextPlayer").GetComponent<Animator>();
        Panime1 = GameObject.Find("âûã}èàíu").GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Panime.SetBool("Text", false);
            Panime.SetBool("Text2", false);
            Panime.SetBool("Text3", false);
            Panime.SetBool("Text4", false);
            Panime1.SetBool("EText", false); Panime1.SetBool("ETextDown", true);
            S.SetActive(true);
            //Text.SetActive(false);
           
            gameManager.Pstop = false;
        }
    }
}
