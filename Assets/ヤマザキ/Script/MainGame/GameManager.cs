using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int count = 0;

    [SerializeField] private GameObject mirrorUI;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject closingDoor;
    [SerializeField] private Text text;
    

    private bool mirrorUIActive = false;
    private bool pauseGame = false;
    private int maxMirrorCount = 3;
    private int nowMatchCount = 5;
    private int nowFlashCount = 0;
    private int mirrorBreakCount = 0;
    private float mirrorBreakTime1;
    private float mirrorBreakTime2;
    private float mirrorBreakTime3;
    private string hitTag;
    private float timer = 0;
    private float timer1 = 0;
    private float timer2 = 0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip First;//最初に使う
    public static float PlayTime;//プレイ時間

    [SerializeField] List<string> messageList = new List<string>();//会話文リスト
    [SerializeField] Text OPEDtext;
    [SerializeField] float novelSpeed;//一文字一文字の表示する速さ
    int novelListIndex = 0; //現在表示中の会話文の配列


    private void Awake()
    {

        if (count == 0)
        {
            //StartScene();
        }

        slider.value = 0;
        slider.maxValue = 10;

        PlayTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        audioSource.PlayOneShot(First);
        StartCoroutine(Novel());
        //PlayerPrefs.SetInt("count", count);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pstop)
        {
            PlayTime += Time.deltaTime;
        }
        // Update game logic
    }


   
    void FixedUpdate()
    {
        

        if (nowMatchCount>5)
        {
            nowMatchCount=5;
        }
        UpdateMirrorBreakUI();
        UpdateMirrorBreakTime();
        //Debug.Log(PlayTime);
        //Debug.Log(mirrorBreakTime2);
        //Debug.Log(mirrorBreakTime3);
    }

    void StartScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    void UpdateMirrorBreakUI()
    {
        mirrorUI.SetActive(mirrorUIActive);
    }

    void UpdateMirrorBreakTime()
    {
        switch (hitTag)
        {
            case "Mirror1":
                timer = mirrorBreakTime1;
                slider.value = timer;
                break;
            case "Mirror2":
                timer1 = mirrorBreakTime2;
                slider.value = timer1;
                break;
            case "Mirror3":
                timer2 = mirrorBreakTime3;
                slider.value = timer2;
                break;
        }
    }

    public void Clear()
    {
        switch(mirrorBreakCount)
        {
            case 1:
                text.text="×1";
                break;
            case 2:
                text.text = "×2";
                break;
            case 3:
                text.text = "×3";
                closingDoor.SetActive(false);
                StartCoroutine(Novel());
                break;
        }

    }

    private IEnumerator Novel()
    {
        int messageCount = 0; //現在表示中の文字数
        OPEDtext.text = ""; //テキストのリセット
        while (messageList[novelListIndex].Length > messageCount)//文字をすべて表示していない場合ループ
        {
            OPEDtext.text += messageList[novelListIndex][messageCount];//一文字追加
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(novelSpeed);//任意の時間待つ
        }

        novelListIndex++; //次の会話文配列
        if (novelListIndex < messageList.Count)//全ての会話を表示したか
        {
            
        }
    }

    public void PredationScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("GameoverScene");
        //Debug.Log("ゲームオーバー");
    }

    public int MBreak
    {
        get { return mirrorBreakCount; }
        set { mirrorBreakCount = value; }
    }

    public bool MirrorUi
    {
        get { return mirrorUIActive; }
        set { mirrorUIActive = value; }
    }

    public bool Pstop
    {
        get { return pauseGame; }
        set { pauseGame = value; }
    }

    public float MirrorT1
    {
        get { return mirrorBreakTime1; }
        set { mirrorBreakTime1 = value; }
    }

    public float MirrorT2
    {
        get { return mirrorBreakTime2; }
        set { mirrorBreakTime2 = value; }
    }

    public float MirrorT3
    {
        get { return mirrorBreakTime3; }
        set { mirrorBreakTime3 = value; }
    }

    public string HitTag
    {
        get { return hitTag; }
        set { hitTag = value; }
    }

    public int NowMatchCount
    {
        get { return nowMatchCount; }
        set { nowMatchCount = value; }
    }

    public int NowFlashCount
    {
        get { return nowFlashCount; }
        set { nowFlashCount = value; }
    }
}
