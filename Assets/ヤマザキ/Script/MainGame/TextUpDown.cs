using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpDown : MonoBehaviour
{
    private Animator Panime;

    // Start is called before the first frame update
    void Start()
    {
        Panime = GameObject.Find("PêÍópCanvas").GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Panime.SetBool("Text", false);
            
        }
    }
}
