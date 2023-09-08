using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerScript;
    private EnemyBoss enemyBossScript;
    private GameObject EnemyBossObj;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip AA;//使う
    [SerializeField] private GameObject AnimatorPlate;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        EnemyBossObj = GameObject.Find("boss_アニメ有");
        enemyBossScript = EnemyBossObj.GetComponent<EnemyBoss>();
        anim = AnimatorPlate.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {

        //Debug.Log(gameManagerScript.NowFlashCount);
        if (other.gameObject.tag == "EnemyBoss"&& enemyBossScript.EnemyState == EnemyBoss.Enemy.PlayerLook && Input.GetMouseButton(0) && gameManagerScript.NowFlashCount != 0)
        {
            audioSource.PlayOneShot(AA);
            enemyBossScript.EnemyState = EnemyBoss.Enemy.ItemFrightening; gameManagerScript.NowFlashCount -= 1;
            anim.SetTrigger("Flash");
            //Debug.Log(enemyBossScript.EnemyState);
        }
    }
}
