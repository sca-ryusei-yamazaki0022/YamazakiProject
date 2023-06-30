using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    EnemyBoss gameOverScript;
    GameObject m_Parent;
    // Start is called before the first frame update
    void Start()
    {
        m_Parent = this.transform.parent.gameObject;
        gameOverScript = m_Parent.GetComponent<EnemyBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameOverScript.GameOver(other);
        }
    }
}
