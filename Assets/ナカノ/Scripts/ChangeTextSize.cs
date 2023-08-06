using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSize : MonoBehaviour
{
    //Textに付与してサイズ変更

    float size;
    RectTransform rectTransform;
    float defaultSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultSize = rectTransform.localScale.x;

        size = (PlayerPrefs.GetFloat("TextSize") * 0.25f / 100 + 1) * defaultSize;
        
        rectTransform.localScale = new Vector3(size, size, 1);
    }
}
