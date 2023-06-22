using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    [SerializeField] GameOverController gameOverController;

    void Start()
    {
    }

    void Update()
    {
    }

    public void OnMouse()
    {
        gameOverController.RetryOn();
    }

    public void OutMouse()
    {
        gameOverController.RetryOut();
    }

    public void Push()
    {
        gameOverController.RetryPush();
    }
}
