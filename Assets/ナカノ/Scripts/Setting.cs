using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    public static float _volume;
    [SerializeField] Text soundValue;
    [SerializeField] Slider textSizeSlider;
    float _textSize;
    [SerializeField] Text sizeValue;

    [SerializeField] AudioSource SE1;
    [SerializeField] AudioSource SE2;
    [SerializeField] Text SampleText;

    void Start()
    {
        _volume = PlayerPrefs.GetFloat("SoundVolume");
        soundValue.text = _volume + "";
        soundSlider.value = _volume;
        _textSize = PlayerPrefs.GetFloat("TextSize");
        sizeValue.text = _textSize + "";
        textSizeSlider.value = _textSize;
    }

    void Update()
    {
        _volume = soundSlider.value;
        SetVolume(_volume);
        soundValue.text = _volume + "";

        _textSize = textSizeSlider.value;
        SetTextSize(_textSize);
        sizeValue.text = _textSize + "";

        SE1.volume = _volume / 100;
        SE2.volume = _volume / 100;
        SampleText.rectTransform.localScale = new Vector3(_textSize * 0.25f / 100 + 1, _textSize * 0.25f / 100 + 1, 1);
    }

    /// <summary>
    /// volume•Û‘¶
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    /// <summary>
    /// TextSize•Û‘¶
    /// </summary>
    /// <param name="size"></param>
    public void SetTextSize(float size)
    {
        PlayerPrefs.SetFloat("TextSize", size);
    }
}
