using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  Å@public static int count=0;
    int CCount = 0;
    bool MirrorUI;
    public RayTest raytest;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Mslider;
    // Start is called before the first frame update
    void Start()
    {
        Goal.SetActive(false);
        Mslider.SetActive(false);
        //DontDestroyOnLoad(this);

        //PlayerPrefs.SetInt("count", count);
        if (count==0)
        {
            
            StartScene();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Clear();
        SliderUI();


    }

    void StartScene()
    {
        
        
        SceneManager.LoadScene("TitleScence");
    }

    void Clear()
    {
        CCount = raytest.Mirror;
        //Debug.Log(CCount);
        if (CCount == 3)
        {
            //Debug.Log(CCount);
            Goal.SetActive(true);

        }
    }
    void SliderUI()
    {
        MirrorUI=raytest.MirrorCheck;
        Debug.Log(MirrorUI);
        if(MirrorUI==true)
        {
            Mslider.SetActive(true);
        }
        else
        {
            Mslider.SetActive(false);
        }
    }
}
