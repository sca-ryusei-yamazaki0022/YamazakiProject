using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;//UIを使用するときに必要

public class EnemyBoss : MonoBehaviour
{
    public Camera targetCamera; // フラグを返したい特定のカメラ
    public GameObject targetObject; // フラグを返したい特定のオブジェクト
    public LayerMask obstacleLayer; // 壁や障害物のレイヤーマスク
    public bool flag = false; // フラグ
    private bool wasVisible = false; // 前回の可視状態
    private bool isChangingFlag = false; // フラグの変更中かどうか
    //private bool TimeBool;//５秒立った時見分けるため
    bool GetAngry;//怒っているフラグ
    bool Onecount=true;
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
    public Enemy EnemyState;//敵の状態をENUMから引き出す関数
    GameManager gameManager;
    private Animator animator;
    bool UseE=true;

    [SerializeField] private Text a;//テキストをアタッチする
    public enum Enemy
    {
        Patrol,//巡回
        PlayerLook,//怒る
        Frightening,//怯み
        Capture//捕獲
    }


    void Start()
    {
        //rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        //ゲームなねージャーを格納
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        // 巡回地点間の移動を継続させるために自動ブレーキを無効化
        //（エージェントは目的地点に近づいても減速しない)
        agent.autoBraking = false;
        // 次の巡回地点の処理を実行
        GotoNextPoint();
        //Branchpoint();
        EnemyState = Enemy.Patrol;
        animator = GetComponent<Animator>();//animator格納
    }

    void FixedUpdate()
    {
        if(flag)
        { 
        a.text = "見つかってる";//テキストの中身を変更
        }
        else
        {
            a.text="見つかってない";
        }
        Camera();
        //Debug.Log(EnemyState);
        switch (EnemyState)
        {
            case Enemy.Patrol://巡回
                //Debug.Log("巡回");
                
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    //Debug.Log("巡回に入りました");
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
                }else if(agent.pathPending == null)
                {
                    Branch=false;
                    agent.destination = points[destPoint].position;
                }
                break;
            case Enemy.PlayerLook://怒る
                animator.SetBool("Run", true);
                //Debug.Log("プレイヤー発見");
                agent.destination = player.transform.position;
                ChaseTime();
                break;
            case Enemy.Frightening://怯み
                EnemyFrightening();
                //Debug.Log("プレイヤーアイテム使用");
                break;
            case Enemy.Capture://捕獲
                Predation();
                //Debug.Log("プレイヤーを捕まえた");
                break;
        }
        if(flag)
        {
            //Debug.Log("見つかってる");
        }
        else
        {
            //Debug.Log("見つかってない");
        }
    }
    // Update is called once per frame
    void Update()
    {

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
                    //Debug.Log("8で０担ってます");
                    bPoint = 0; destPoint = 14;
                    Branch = false;
                    break;
                case 9:
                    //Debug.Log("９で０担ってます");
                    bPoint = 0; destPoint = 9;
                    Branch = false;
                    break;
                case 10:
                    //Debug.Log("10で０担ってます");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
                case 12:
                    //Debug.Log("12で0担ってます");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
            }

        }
    }

    public void GameOver(Collider other)
    {
       // Debug.Log("引っかかったq");
        EnemyState = Enemy.Capture;
        agent.destination = this.gameObject.transform.position;
        agent.enabled = false;
        //Debug.Log(EnemyState);
    }
    
    void ChaseTime()//逃走時間
    {
        if(flag&& EnemyState == Enemy.PlayerLook)
        { 
            Chasetime += Time.deltaTime;
            //Debug.Log("回転して");
        }
        else
        {
            Chasetime=0;
        }
        //Debug.Log(Chasetime);
        if (Chasetime >= 5)
        {
            Debug.Log("５秒経過");
            EnemyState = Enemy.Patrol;
            animator.SetBool("Run", false);
            if (!Branch)
            {
                GotoNextPoint();
            }
            else
            {
                GotoBranchPoint();
            }
        }
       
    }

    void Predation()//捕食時
    {
        //Debug.Log("a");
        //ここでアニメーション再生系を設定
        if (flag) {
            animator.SetBool("RunAttack", true);
        }
        else{
        animator.SetBool("WalkAttack", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            EnemyState=Enemy.Frightening;//怯みに変更
            CancelInvoke("SceneGameover");
            //Debug.Log("ここで呼ばれたよ");
            UseE =false;
        }
        else if (Onecount&&UseE)
        {
            Invoke("SceneGameover", 3.0f);
            Debug.Log("ここで呼ばれたよ");
            Onecount=false; UseE = false;
        }
            
    }
    void SceneGameover()
    {

        gameManager.PredationScene();
    }
    void Camera()
    {
        if (targetCamera == null || targetObject == null)
        {
            return;
        }

        Vector3 targetPosition = targetObject.transform.position;
        Vector3 viewportPosition = targetCamera.WorldToViewportPoint(targetPosition);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1 || viewportPosition.z < 0 || viewportPosition.z > targetCamera.farClipPlane)
        {
            flag = false; // オブジェクトがカメラの描画外に出たらフラグを下ろす
            //EnemyState = Enemy.Patrol;
            //return;
        }

        bool isVisible = IsVisibleFromCamera(targetObject) && !IsBehindCamera(targetPosition) && !IsObstacleBetweenCamera(targetPosition);

        if (isVisible && !wasVisible && !isChangingFlag)
        {
            flag = true; // オブジェクトがカメラに見えてカメラの後ろにいないかつ壁がない場合はフラグを立てる
            EnemyState = Enemy.PlayerLook;//Enum変更
            StartCoroutine(FlagChangeDelay()); // フラグ変更の遅延処理を開始
        }
        /*
        else if(EnemyState == Enemy.PlayerLook && TimeBool)
        {
            //Debug.Log("パトロールに戻したよ");
            EnemyState = Enemy.Patrol;
            animator.SetBool("Run", false);
            TimeBool =false;
        }
        */
        wasVisible = isVisible; // 現在の可視状態を保存
    }

    void EnemyFrightening()
    {
        Debug.Log("ひるませるよ");
        animator.SetBool("MissAttack", true);
        StartCoroutine(MissAttackDelay());
    }
    private IEnumerator MissAttackDelay()
    {
        yield return new WaitForSeconds(3.0f); //遅延
        agent.enabled = true;//Navを基に戻す
        EnemyState = Enemy.PlayerLook;
        UseE=true;
        animator.SetBool("MissAttackRun", true); animator.SetBool("RunAttack", false); animator.SetBool("MissAttack", false);
    }

    private IEnumerator FlagChangeDelay()
    {
        isChangingFlag = true; // フラグ変更中フラグを立てる

        yield return new WaitForSeconds(0.1f); // フラグ変更の遅延時間（例として0.1秒）

        isChangingFlag = false; // フラグ変更中フラグを解除

        //Debug.Log(flag);
    }

    private bool IsVisibleFromCamera(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null || !renderer.enabled)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private bool IsBehindCamera(Vector3 targetPosition)
    {
        Vector3 cameraToTarget = targetPosition - targetCamera.transform.position;
        Vector3 cameraForward = targetCamera.transform.forward;

        if (Vector3.Dot(cameraToTarget, cameraForward) <= 0)
        {
            return true; // オブジェクトがカメラの後ろにいる
        }

        return false; // オブジェクトがカメラの前にいる
    }

    private bool IsObstacleBetweenCamera(Vector3 targetPosition)
    {
        Vector3 cameraPosition = targetCamera.transform.position;
        Vector3 direction = targetPosition - cameraPosition;
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, direction, out hit, direction.magnitude, obstacleLayer))
        {
            //Debug.Log("壁あるよ");
            return true; //カメラとターゲットの間に壁がある
        }
        //Debug.Log("壁ないよ");
        return false; // カメラとターゲットの間に壁がない
    }
}
