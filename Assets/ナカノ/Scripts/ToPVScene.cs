using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPVScene : MonoBehaviour
{
    [SerializeField, Header("PVÄ¶‚Ü‚Å (•b)")] private float changeTime;
    private float nowTime;

    void Start()
    {
        nowTime = 0;
    }

    void Update()
    {
        nowTime += Time.deltaTime;
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
}
