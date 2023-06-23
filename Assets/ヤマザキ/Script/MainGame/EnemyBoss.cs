using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBoss : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public GameObject test;
    [SerializeField] private Camera targetCamera;
    private RectTransform rect;

    float distance = 100; // 飛ばす&表示するRayの長さ
    float duration = 3;   // 表示期間（秒）
    bool GetAngry;//怒っているフラグ

    // 巡回地点オブジェクトを格納する配列
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    [SerializeField] private GameObject player;
    // 巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    private int bPoint = 0;
    private float Chasetime;//巡回モードに戻るときに10秒間見られていないかの確認
    private NavMeshAgent agent;// NavMesh Agent コンポーネントを格納する変数
    private bool Branch;//分岐点を決める
    private Enemy EnemyState;//敵の状態をENUMから引き出す関数


    public enum Enemy
    {
        Patrol,//巡回
        PlayerLook,//怒る
        Frightening,//怯み
        Capture//捕獲
    }


    void Start()
    {
        rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        // 巡回地点間の移動を継続させるために自動ブレーキを無効化
        //（エージェントは目的地点に近づいても減速しない)
        agent.autoBraking = false;
        // 次の巡回地点の処理を実行
        GotoNextPoint();
        //Branchpoint();
        EnemyState = Enemy.Patrol;
    }

    void FixedUpdate()
    {
        //Debug.Log(destPoint);
        switch (EnemyState)
        {
            case Enemy.Patrol://巡回
                //Debug.Log("巡回");
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    //GotoNextPoint();
                    //Debug.Log(Branch);
                    // 次の巡回地点を設定する処理を実行
                    if (!Branch)
                    {
                        GotoNextPoint();
                    }
                    else
                    {
                        GotoBranchPoint();
                    }
                }
                break;
            case Enemy.PlayerLook://怒る
                //Debug.Log("プレイヤー発見");
                agent.destination = player.transform.position;
                ChaseTime();
                break;
            case Enemy.Frightening://怯み
                //Debug.Log("プレイヤーアイテム使用");
                break;
            case Enemy.Capture://捕獲
                //Debug.Log("プレイヤーを捕まえた");
                break;
        }

        PLRay();

    }
    // Update is called once per frame
    void Update()
    {

    }
    void PLRay()
    {
        Ray ray = new Ray(test.transform.position, test.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.point);
        }
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            //もしhitのタグが"Player"と一致していた場合．．．の処理内容
            EnemyState = Enemy.PlayerLook;
            Chasetime = 0;
        }
       
       
    }
    // 次の巡回地点を設定する処理
    void GotoNextPoint()
    {
        // 巡回地点が設定されていなければ
        if (points.Length == 0)
            // 処理を返します
            return;
        // 現在選択されている配列の座標を巡回地点の座標に代入
        agent.destination = points[destPoint].position;
        // 配列の中から次の巡回地点を選択（必要に応じて繰り返し）
        destPoint = (destPoint + 1) % points.Length;
    }
    void GotoBranchPoint()
    {
        switch (destPoint)
        {
            case 3:
                destPoint += 4;
                Branch = false;
                break;
            case 8:
                if (BranchpointsFour.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsFour[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsFour.Length;
                Debug.Log(bPoint);
                break;
            case 9:
                if (BranchpointsOne.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsOne[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsOne.Length;
                Debug.Log(bPoint);
                break;
            case 10:
                if (BranchpointsTwo.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsTwo[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsTwo.Length;
                Debug.Log(bPoint);
                break;
            case 12:
                if (BranchpointsThree.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsThree[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsThree.Length;
                Debug.Log(bPoint);
                break;



        }
    }
    void Branchpoint()
    {
        Branch = Random.value > 0.5f;
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AAAAAAAAA");
        if (other.gameObject.CompareTag("BPoint"))
        {
            Branchpoint();
            //Debug.Log(Branch);
        }//Debug.Log("AAAAAAAAA");
        if (other.gameObject.CompareTag("BPointEnd"))
        {
            //Debug.Log(destPoint);
            switch (destPoint)
            {
                case 8:
                    Debug.Log("8で０担ってます");
                    bPoint = 0; destPoint = 14;
                    Branch = false;
                    break;
                case 9:
                    Debug.Log("９で０担ってます");
                    bPoint = 0; destPoint = 9;
                    Branch = false;
                    break;
                case 10:
                    Debug.Log("10で０担ってます");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
                case 12:
                    Debug.Log("12で0担ってます");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
            }

        }
    }
    void ChaseTime()
    {
        Chasetime += Time.deltaTime;
        if (Chasetime >= 5)
        {
            Debug.Log("５秒経過");
            EnemyState = Enemy.Patrol;
        }
        else
        {
            EnemyState = Enemy.PlayerLook;
        }
    }
}
