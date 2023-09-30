using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPVScene : MonoBehaviour
{
    [SerializeField, Header("PVÄ¶‚Ü‚Å (•b)")] private float changeTime;
    private float nowTime;

    bool isCount;

    void Start()
    {
        isCount = true;
        nowTime = 0;
    }

    void Update()
    {
        if(isCount)
        {
            nowTime += Time.deltaTime;
        }
        else
        {
            nowTime = 0;
        }

        if(nowTime >= changeTime)
        {
            SceneManager.LoadScene("PVScene");
            nowTime = 0;
        }

        if(Input.GetMouseButton(0))
        {
            nowTime = 0;
        }
    }

    public void countStart()
    {
        isCount = true;
    }

    public void countStop()
    {
        isCount = false;
    }
}
