using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Getangry : MonoBehaviour
{
    int ICheck;
    public RayTest rayTest;
    [SerializeField] private Image Enemy;
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject EnemyView;
    // Start is called before the first frame update
    void Start()
    {

        
        Enemy.color = new Color(255, 0, 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        ICheck = rayTest.Mirror;
        switch(ICheck)
        {
            case 1:
                Enemy.color = new Color(255, 0, 0, 0.3f);
                break;
            case 2:
                Enemy.color = new Color(255, 0, 0, 0.6f);
                break;
            case 3:
                Image.SetActive(false);
                EnemyView.SetActive(false);
                break;
            default:
                Enemy.color = new Color(255, 0, 0, 0.0f);
                break;
        }
    }
}
