using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
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
        gameOverController.EndOn();
    }

    public void OutMouse()
    {
        gameOverController.EndOut();
    }

    public void Push()
    {
        gameOverController.EndPush();
    }
}
