using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //int onecount = 0;
    void Start()
    {
        //onecount+=1;
        GameManager.count+=1;
    }
   
    public void OnClickStartButton()
    {
        //PlayerPrefs.SetInt("OC",onecount);
        SceneManager.LoadScene("MainGame");
    }
}
