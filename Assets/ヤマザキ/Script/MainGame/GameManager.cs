using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
  　public static int count=0;
    bool MirrorUII=false;
    bool PStop=false;
    int MachMaxCount=3;
    int FlashMaxCount=2;
    int MirrorBreakcount=0;
    float MirrorBreakTime1;
    float MirrorBreakTime2;
    float MirrorBreakTime3;
    string Tagname;
    private float Timer=0;
    [SerializeField] private GameObject MirrirUI;
    [SerializeField] private Slider slider;

    private void Awake()
    {   /*
        if (instance == null) { instance=this;DontDestroyOnLoad(this.gameObject);}
        else { Destroy(this.gameObject);}        
        */
        if (count == 0)
        {
            StartScene();
        }
        slider.value = 0;
        slider.maxValue = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("count", count);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(MirrorUII);
        Clear();
        
    }
    void FixedUpdate()
    {
        MirorrBreakUI();
        MirrorBreakTime();
    }

    void StartScene()
    {  
        SceneManager.LoadScene("TitleScence");
    }

    void Clear()
    {
        
    }
    void MirorrBreakUI()
    {
        
        if (MirrorUII==true)
        {
            Debug.Log("ここでゲージを出したい");
            MirrirUI.SetActive(true);
        }
        else
        {
            Debug.Log("ここでゲージを非表示");
            MirrirUI.SetActive(false);
        }        
    }
    void MirrorBreakTime()
    {
        
        switch (Tagname)
        {
            case "Mirror1":
                Timer = MirrorBreakTime1;
                slider.value = Timer;
                break;
            case "Mirror2":
                Timer = MirrorBreakTime2;
                slider.value = Timer;
                break;
            case "Mirror3":
                Timer = MirrorBreakTime3;
                slider.value = Timer;
                break;
        }
    }
   

    public int MBreak
    {
          get { return this.MirrorBreakcount; }
          set { this.MirrorBreakcount = value; }
    }
    public bool MirrorUi
    {
        get { return this.MirrorUII; }
        set { this.MirrorUII = value; }
    }
    public bool Pstop
    {
        get { return this.PStop; }
        set { this.PStop = value; }
    }
    public float MirrorT1
    {
        get { return this. MirrorBreakTime1; }
        set { this.MirrorBreakTime1 = value; }
    }
    public float MirrorT2
    {
        get { return this.MirrorBreakTime2; }
        set { this.MirrorBreakTime2 = value; }
    }
    public float MirrorT3
    {
        get { return this.MirrorBreakTime3; }
        set { this.MirrorBreakTime3 = value; }
    }
    public string HitTag
    {
        get { return this.Tagname; }
        set { this.Tagname = value; }
    }
}
