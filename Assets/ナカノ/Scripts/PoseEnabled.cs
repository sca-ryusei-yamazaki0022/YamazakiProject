using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseEnabled : MonoBehaviour
{
    [SerializeField] GameObject poseWindow;
    [SerializeField] Image poseImage;
    Animator poseAnim;
    [SerializeField] private GameManager gameManager;

    [SerializeField] Image backGround;

    void Start()
    {
        poseWindow.SetActive(false);
        poseAnim = poseImage.GetComponent<Animator>();

        backGround.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&gameManager.Pstop==false)
        {
            poseWindow.SetActive(true);
            StartCoroutine(poseAnimation());
            backGround.enabled = true;
        }
    }

    IEnumerator poseAnimation()
    {
        gameManager.Pstop = true;
        yield return new WaitForEndOfFrame();
        poseAnim.SetTrigger("Reduction");
        yield break;
    }
}
