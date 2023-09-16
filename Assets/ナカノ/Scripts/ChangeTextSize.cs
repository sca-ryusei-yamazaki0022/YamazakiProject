using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSize : MonoBehaviour
{
    //Textに付与してサイズ変更

    float size;
    RectTransform rectTransform;
    float lastSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        size = PlayerPrefs.GetFloat("TextSize");
        
        rectTransform.localScale = new Vector3(rectTransform.localScale.x * size * 0.7f / 100 + 0.5f, rectTransform.localScale.y * size * 0.7f / 100 + 0.5f, 1);
        lastSize = size;
    }

    void Update()
    {
        size = PlayerPrefs.GetFloat("TextSize");
        if (lastSize != size)
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x * size * 0.7f / 100 + 0.5f, rectTransform.localScale.y * size * 0.7f / 100 + 0.5f, 1);
            lastSize = size;
        }
    }
}
