using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpDown : MonoBehaviour
{
    private Animator Panime;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject S;
    // Start is called before the first frame update
    void Start()
    {
        Panime = GameObject.Find("TextPlayer").GetComponent<Animator>();
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
            S.SetActive(true);
            gameManager.Pstop = false;
        }
    }
}
