using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseEnabled : MonoBehaviour
{
    [SerializeField] GameObject poseWindow;
    [SerializeField] Image poseImage;
    Animator poseAnim;

    void Start()
    {
        poseWindow.SetActive(false);
        poseAnim = poseImage.GetComponent<Animator>();   
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            poseWindow.SetActive(true);
            StartCoroutine(poseAnimation());
        }
    }

    IEnumerator poseAnimation()
    {
        yield return new WaitForEndOfFrame();
        poseAnim.SetTrigger("Reduction");
        yield break;
    }
}
