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

    private bool mirrorUIActive = false;
    private bool pauseGame = false;
    private int maxMirrorCount = 3;
    private int maxFlashCount = 2;
    private int mirrorBreakCount = 0;
    private float mirrorBreakTime1;
    private float mirrorBreakTime2;
    private float mirrorBreakTime3;
    private string hitTag;
    private float timer = 0;
    private float timer1 = 0;
    private float timer2 = 0;

    private void Awake()
    {

        if (count == 0)
        {
            //StartScene();
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
        // Update game logic
    }

    void FixedUpdate()
    {
        UpdateMirrorBreakUI();
        UpdateMirrorBreakTime();
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
        if (mirrorBreakCount == 3)
        {
            closingDoor.SetActive(false);
        }

    }
    

    public void PredationScene()
    {
        SceneManager.LoadScene("GameoverScene");
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
   
}
