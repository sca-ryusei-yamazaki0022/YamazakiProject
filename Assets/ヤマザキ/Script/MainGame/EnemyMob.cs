using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// "NavMeshAgent"関連クラスを使用できるようにする
using UnityEngine.AI;

public class EnemyMob : MonoBehaviour
{
    // 巡回地点オブジェクトを格納する配列
    public Transform[] points;
    // 巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    // NavMesh Agent コンポーネントを格納する変数
    private NavMeshAgent agent;
    private NavMeshAgent agentBoss;
    private EnemyMobClass EnemyMobState;
    private EnemyBoss enemyBoss;
    [SerializeField] private GameObject EnemyBossP;//呼び出すBOSSのオブジェクトを設定
    [SerializeField] private GameObject SummoningPoints;//呼び出しポイント
    [SerializeField] private GameObject BoxColloder;//Cryに入った時にオントリガーを呼び出さないために
    bool OnCount=true;//敵の座標かえるのは一回だけ
    private Animator animator;

    public enum EnemyMobClass
    {
        Patrol,//巡回
        Cry//なく
    }
    // ゲームスタート時の処理
    void Start()
    {
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        agentBoss=EnemyBossP.GetComponent<NavMeshAgent>();
        // 巡回地点間の移動を継続させるために自動ブレーキを無効化
        //（エージェントは目的地点に近づいても減速しない)
        agent.autoBraking = false;
        // 次の巡回地点の処理を実行
        GotoNextPoint();
        EnemyMobState = EnemyMobClass.Patrol;
        enemyBoss = GameObject.FindWithTag("EnemyBoss").GetComponent<EnemyBoss>();
        animator = GetComponent<Animator>();//animator格納
    }

   

    // ゲーム実行中の繰り返し処理
    void FixedUpdate()
    {
        //ENUMの内容を確認
        switch(EnemyMobState)
        { 
            case EnemyMobClass.Patrol:
            // エージェントが現在の巡回地点に到達したら
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                // 次の巡回地点を設定する処理を実行
                GotoNextPoint();
            break;
            case EnemyMobClass.Cry:
                Invoke("MobCry", 1.0f);
                Invoke("Mob", 7.0f);
            break;
        }
        //Debug.Log(EnemyMobState);
    }

    // 次の巡回地点を設定する処理
    void GotoNextPoint()
    {
        //Debug.Log("呼ばれてます");
        // 巡回地点が設定されていなければ
        if (points.Length == 0)
            // 処理を返します
            return;
        // 現在選択されている配列の座標を巡回地点の座標に代入
        agent.destination = points[destPoint].position;
        // 配列の中から次の巡回地点を選択（必要に応じて繰り返し）
        destPoint = (destPoint + 1) % points.Length;
    }
    void MobCry()
    {
        if (OnCount)
        {
            animator.SetBool("Cry",true);
            agent.destination = this.gameObject.transform.position;
            agent.enabled = false;
            agentBoss.enabled = false;
            EnemyBossP.transform.position= new Vector3(SummoningPoints.gameObject.transform.position.x, SummoningPoints.gameObject.transform.position.y, SummoningPoints.gameObject.transform.position.z); 
            enemyBoss.EnemyState = EnemyBoss.Enemy.PlayerLook;
            agentBoss.enabled = true;
            //transform.LookAt(transform.position);
            OnCount =false;
        }
        //Debug.Log("なくよー");
    }
    void Mob()
    {
        agent.enabled = true;
        EnemyMobState = EnemyMobClass.Patrol;
        animator.SetBool("Cry", false);
        BoxColloder.SetActive(true);
        
        
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            BoxColloder.SetActive(false);
            EnemyMobState=EnemyMobClass.Cry;
            OnCount = true;
        }
    }
}
