using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorBreakSlider : MonoBehaviour
{
    private float Timer;
    public RayTest rayTest;
    string Tagname;
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        slider.value = 0;
        slider.maxValue=10;
    }

    // Update is called once per frame
    void Update()
    {
        Tagname=rayTest.TagMirror;
        //Debug.Log(Tagname);
        switch (Tagname)
        {
            case "Mirror1":
                Timer = rayTest.Mirror1;
                slider.value = Timer;
                break;
            case "Mirror2":
                Timer = rayTest.Mirror2;
                slider.value = Timer;
                break;
            case "Mirror3":
                Timer = rayTest.Mirror3;
                slider.value = Timer;
                break;
        }
        //switch ()
    }
}
