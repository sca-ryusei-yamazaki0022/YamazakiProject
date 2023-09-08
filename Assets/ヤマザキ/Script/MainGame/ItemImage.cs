using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImage : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private GameObject Cr1;
    [SerializeField] private GameObject Cr2;
    [SerializeField] private GameObject Match1;
    [SerializeField] private GameObject Match2;
    [SerializeField] private GameObject Match3;
    [SerializeField] private GameObject Match4;
    [SerializeField] private GameObject Match5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(gameManagerScript.NowFlashCount)
        {
            case 0:
                Cr1.gameObject.SetActive(false);
                Cr2.gameObject.SetActive(false);
                break;
            case 1:
                Cr1.gameObject.SetActive(true);
                Cr2.gameObject.SetActive(false);
                break;
            case 2:
                Cr1.gameObject.SetActive(true);
                Cr2.gameObject.SetActive(true);
                break;
        }
        Debug.Log(gameManagerScript.NowMatchCount);
        switch(gameManagerScript.NowMatchCount)
        {
            case 0:
                Match1.gameObject.SetActive(false);Match2.gameObject.SetActive(false);Match3.gameObject.SetActive(false);Match4.gameObject.SetActive(false);Match5.gameObject.SetActive(false);
                break;
            case 1:
                Match1.gameObject.SetActive(true); Match2.gameObject.SetActive(false); Match3.gameObject.SetActive(false); Match4.gameObject.SetActive(false); Match5.gameObject.SetActive(false);
                break;
            case 2:
                Match1.gameObject.SetActive(true); Match2.gameObject.SetActive(true); Match3.gameObject.SetActive(false); Match4.gameObject.SetActive(false); Match5.gameObject.SetActive(false);
                break;
            case 3:
                Match1.gameObject.SetActive(true); Match2.gameObject.SetActive(true); Match3.gameObject.SetActive(true); Match4.gameObject.SetActive(false); Match5.gameObject.SetActive(false);
                break;
            case 4:
                Match1.gameObject.SetActive(true); Match2.gameObject.SetActive(true); Match3.gameObject.SetActive(true); Match4.gameObject.SetActive(true); Match5.gameObject.SetActive(false);
                break;
            case 5:
                Match1.gameObject.SetActive(true); Match2.gameObject.SetActive(true); Match3.gameObject.SetActive(true); Match4.gameObject.SetActive(true); Match5.gameObject.SetActive(true);
                break;
        }
    }
}
