using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSize : MonoBehaviour
{
    //Text�ɕt�^���ăT�C�Y�ύX

    float size;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        size = PlayerPrefs.GetFloat("TextSize") * 0.25f / 100 + 1;
        
        rectTransform.localScale = new Vector3(rectTransform.localScale.x * size, rectTransform.localScale.y * size, 1);
    }
}
