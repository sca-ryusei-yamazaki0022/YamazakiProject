using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
  Å@public static int count=0;
    int CCount = 0;
    bool MirrorUI;
    public RayTest raytest;
    int MachMaxCount=3;
    int FlashMaxCount=2;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Mslider;
    int MirrorBreakcount=0;

    public enum GameState
    {
        Start,
        Playing,
        Ep,
        End
    }
    private void Awake()
    {
        if (instance == null) { instance=this;DontDestroyOnLoad(this.gameObject);}
        else { Destroy(this.gameObject);}
        if (count == 0)
        {
            StartScene();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Goal.SetActive(false);
        Mslider.SetActive(false);
        //DontDestroyOnLoad(this);

        //PlayerPrefs.SetInt("count", count);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MirrorBreakcount);
        Clear();
        SliderUI();
        

    }

    void StartScene()
    {  
        SceneManager.LoadScene("TitleScence");
    }

    void Clear()
    {
        /*
        CCount = raytest.Mirror;
        //Debug.Log(CCount);
        if (CCount == 3)
        {
            //Debug.Log(CCount);
            Goal.SetActive(true);
        }
        */
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
    void MirorrBreak()
    {

    }
    /*
    public int MachCount
    {
        get { return this.MachMaxCount; }
        private set { this.MachMaxCount = value; }
    }
    public int FlashCount
    {
        get { return this.FlashMaxCount; }
        private set { this.FlashMaxCount = value; }
    }
    */

    public int MBreak
    {
          get { return this.MirrorBreakcount; }
          set { this.MirrorBreakcount = value; }
    }

}
