using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerScript;
    private EnemyBoss enemyBossScript;
    private GameObject EnemyBossObj;
    // Start is called before the first frame update
    void Start()
    {
        EnemyBossObj = GameObject.Find("boss_ÉAÉjÉÅóL");
        enemyBossScript = EnemyBossObj.GetComponent<EnemyBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(gameManagerScript.NowFlashCount);
        if (other.gameObject.tag == "EnemyBoss"&& enemyBossScript.EnemyState == EnemyBoss.Enemy.PlayerLook && Input.GetKeyDown(KeyCode.E) && gameManagerScript.NowFlashCount != 0)
        {
            enemyBossScript.EnemyState = EnemyBoss.Enemy.ItemFrightening; gameManagerScript.NowFlashCount -= 1;
            //Debug.Log(enemyBossScript.EnemyState);
        }
    }
}
