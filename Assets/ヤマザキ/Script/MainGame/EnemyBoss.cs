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
    bool Onecount = true;
    // 巡回地点オブジェクトを格納する配列
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Magic;
    //[SerializeField] private Transform playerT;
    // 巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    private int bPoint = 0;
    private float Chasetime;//巡回モードに戻るときに10秒間見られていないかの確認
    private NavMeshAgent agent;// NavMesh Agent コンポーネントを格納する変数
    private bool Branch;//分岐点を決める
    public Enemy EnemyState;//敵の状態をENUMから引き出す関数
    GameManager gameManager;
    private Animator animator;
    private Animator PAnimator;
    private Animator Door;
    private GameObject DoorOBJ;
    bool UseE = true;
    bool DoorFlag;
    private Animator DoorAnim;
    // bool playerHide;//プレイヤーが敵におわれている際に隠れているかのフラグ
    //[SerializeField] private Text a;//テキストをアタッチする
    private GameObject BreakDoor;
    [SerializeField] private AudioClip Shout;//叫ぶ
    [SerializeField] private AudioClip Flinch;//怯む
    [SerializeField] private AudioClip Walk;//歩く
    bool walk = true;
    bool run = true;
    [SerializeField] private AudioSource audioSourceSmall;
    [SerializeField] private AudioSource audioSourceBig;
    [SerializeField] private GameObject GameOverPlayer;
    [SerializeField] private GameObject Mirror;
    bool Capture;
    float NavSpeed=1;
    bool EnemyOne;

    public enum Enemy
    {
        Patrol,//巡回
        PlayerLook,//怒る
        Frightening,//怯み
        ItemFrightening,//アイテムでの怯み
        Capture,//捕獲
        DoorAttack,//Doorがあった時用
        end
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
        PAnimator = player.GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        if (wasVisible)
        {
            Mirror.SetActive(false);
            
        }
        else
        {
            Mirror.SetActive(true);
        }
        if(wasVisible&&EnemyState==Enemy.Patrol||EnemyState==Enemy.PlayerLook)
        {
            EnemyState = Enemy.PlayerLook; agent.destination = player.transform.position;
        }
        switch(gameManager.MBreak)
        {
            case 1:
                NavSpeed=1.3f;
                break;
            case 2:
                NavSpeed=1.6f;
                break;
            case 3:
                
                EnemyState=Enemy.end;
                audioSourceBig.PlayOneShot(Shout, 0.7f);
                animator.SetBool("Run", true);
                this.transform.position=new Vector3(Magic.gameObject.transform.position.x, Magic.gameObject.transform.position.y, Magic.gameObject.transform.position.z);
                gameManager.MBreak+=1;
                break;
        }
        //Debug.Log(gameManager.MBreak);
        //this.agent.speed = 2f;
        Camera();
        //Debug.Log(EnemyState);
        switch (EnemyState)
        {
            case Enemy.Patrol://巡回
                animator.SetBool("Run", false);
                this.agent.speed = 3.5f*NavSpeed;
                if (!agent.pathPending && agent.remainingDistance < 0.3f)
                {
                    
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
                else if (agent.pathPending == null)
                {
                    Branch = false;
                    agent.destination = points[destPoint].position;
                }
                break;

            case Enemy.PlayerLook://怒る
                this.agent.speed = 4.0f*NavSpeed;
                animator.SetBool("Run", true);
                /*
                if(run)
                { 
                    Debug.Log("tootta");
                    
                    audioSourceSmall.PlayOneShot(Walk,0.4f);
                    audioSourceSmall.pitch = 1.8f;
                    audioSourceSmall.loop = true;
                    run =false;walk=true;
                }*/
                //playerHide
                //Debug.Log("プレイヤー発見");
                audioSourceBig.PlayOneShot(Shout, 0.3f);
                
                ChaseTime();
                break;

            case Enemy.Frightening://怯み
                EnemyFrightening();
                audioSourceSmall.loop = false;
                audioSourceSmall.PlayOneShot(Flinch);
                //Debug.Log("プレイヤーアイテム使用");
                break;

            case Enemy.ItemFrightening://アイテムでの怯み
                Debug.Log("入ってる");
                StartCoroutine(EnemyItemiFrightening());
                break;
            case Enemy.Capture://捕獲
                Predation();

                
                //Debug.Log("プレイヤーを捕まえた");
                break;

            case Enemy.DoorAttack:

                StartCoroutine(Doorattack());
                break;
            case Enemy.end:
                this.agent.speed = 5.5f;
                agent.destination = player.transform.position;
                break;
        }
        if (flag)
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
    void GotoNextPoint()//次に向かうべき場所の選定
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
                //Debug.Log(bPoint);
                break;
            case 9:
                if (BranchpointsOne.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsOne[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsOne.Length;
                //Debug.Log(bPoint);
                break;
            case 10:
                if (BranchpointsTwo.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsTwo[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsTwo.Length;
                //Debug.Log(bPoint);
                break;
            case 12:
                if (BranchpointsThree.Length == 0)
                    // 処理を返します
                    return;
                // 現在選択されている配列の座標を巡回地点の座標に代入
                agent.destination = BranchpointsThree[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsThree.Length;
                //Debug.Log(bPoint);
                break;



        }
    }//次に向かうべき場所の選定（巡回地点の切り替え時）
    void Branchpoint()
    {
        Branch = Random.value > 0.5f;
    }//巡回地点の切り替え
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door")&& !Capture)
        {
            var Animetor = other.transform.parent.parent.gameObject.GetComponent<Animator>();
            DoorAnim = other.transform.parent.parent.gameObject.GetComponent<Animator>();
            DoorFlag = Animetor.GetBool("Door");
            if(!DoorFlag)
            {
                Debug.Log("開いてない");
                EnemyState = Enemy.DoorAttack;
                animator.SetBool("DoorAttack", true);
                BreakDoor= other.transform.parent.parent.gameObject;
            }
        }
        if(other.gameObject.CompareTag("OneSideDoor")&&!Capture)
        { 
            var Animetor1 = other.transform.parent.gameObject.GetComponent<Animator>();
            DoorAnim = other.transform.parent.gameObject.GetComponent<Animator>();
            DoorFlag = Animetor1.GetBool("Door");
            if (DoorFlag)
            {
                Debug.Log("開いてない");
                EnemyState = Enemy.DoorAttack;
                animator.SetBool("DoorAttack", true);
                BreakDoor = other.transform.parent.gameObject;
            }
           
        }
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
    }//自分の巡回しているところを確認

    public void GameOver(Collider other)
    {
        // Debug.Log("引っかかったq");
        if(!EnemyOne&&EnemyState!=Enemy.ItemFrightening)
        { 
            EnemyState = Enemy.Capture;
            
            Capture=true;
            agent.destination = this.gameObject.transform.position;
            agent.enabled = false;
            EnemyOne = true;
        }
    }//ゲームオーバー時のてきBoss の動き



    void Predation()//捕食時
    {
        //Debug.Log("TOOOOOOO");

        //Debug.Log("a");
        //ここでアニメーション再生系を設定
        animator.SetBool("MissAttackRun", false);
        animator.SetBool("WalkAttack", false);
        animator.SetBool("RunAttack", true);
        animator.SetBool("WalkAttack", true);


        
        Onecount = true; UseE = true;
        //UseE=true;
        if (Input.GetKey(KeyCode.E) && gameManager.NowFlashCount != 0)
        {
            PAnimator.SetBool("GameOver", false);
            EnemyState = Enemy.Frightening;//怯みに変更
            gameManager.NowFlashCount -= 1;
            CancelInvoke("SceneGameover");
            
            //Debug.Log("ここで呼ばれたよ");
            UseE = false;
            Capture = true;
        }
        else if (Onecount && UseE)
        {
           
            PAnimator.SetBool("GameOver", true);
            player.transform.position=new Vector3(GameOverPlayer.gameObject.transform.position.x, player.gameObject.transform.position.y,GameOverPlayer.gameObject.transform.position.z);
            Invoke("SceneGameover", 3.6f);
            Debug.Log(Onecount);

            Onecount = false; UseE = false;
        }

    }

    private IEnumerator Doorattack()
    {
        agent.destination = this.gameObject.transform.position;
        yield return new WaitForSeconds(1);
        animator.SetBool("DoorAttack", false);
        yield return new WaitForSeconds(2);
        BreakDoor.SetActive(false);
        EnemyState = Enemy.Patrol;

    }
    void SceneGameover()//ゲームオーバー画面にかえるだけ
    {
        EnemyState = Enemy.end;
        gameManager.PredationScene();
    }
    void Camera()
    {
        Debug.Log("ボス移動");
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

        if (isVisible && !wasVisible) //&& !isChangingFlag)
        {
            flag = true; // オブジェクトがカメラに見えてカメラの後ろにいないかつ壁がない場合はフラグを立てる
            //EnemyState = Enemy.PlayerLook;//Enum変更
            //StartCoroutine(FlagChangeDelay()); // フラグ変更の遅延処理を開始
        }
       
        wasVisible = isVisible; // 現在の可視状態を保存
        //Debug.Log(isVisible);
    }//カメラに写っているかの確認

    void EnemyFrightening()//捕食時にアイテムを使われた
    {

        Debug.Log("ひるませるよ");
        animator.SetBool("MissAttack", true);
        StartCoroutine(MissAttackDelay());
    }

    private IEnumerator EnemyItemiFrightening()
    {
        agent.destination = this.gameObject.transform.position;
        animator.SetBool("Item", true);
        yield return new WaitForSeconds(6.0f);
        animator.SetBool("Item", false);
        
        EnemyState = Enemy.Patrol;
    }

    private IEnumerator MissAttackDelay()
    {
        yield return new WaitForSeconds(2.0f); //遅延
        agent.enabled = true;//Navを基に戻す
        EnemyState = Enemy.PlayerLook;
        Capture = false;
        UseE = true; EnemyOne = false;
        animator.SetBool("MissAttackRun", true); animator.SetBool("RunAttack", false); animator.SetBool("MissAttack", false);
    }

   
   
    private bool IsVisibleFromCamera(GameObject obj)//カメラ外
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null || !renderer.enabled)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private bool IsBehindCamera(Vector3 targetPosition)//カメラの後ろにいるかどうか
    {
        Vector3 cameraToTarget = targetPosition - targetCamera.transform.position;
        Vector3 cameraForward = targetCamera.transform.forward;


        if (Vector3.Dot(cameraToTarget, cameraForward) <= 0)
        {
            return true; // オブジェクトがカメラの後ろにいる
        }

        return false; // オブジェクトがカメラの前にいる
    }

    private bool IsObstacleBetweenCamera(Vector3 targetPosition)//間に壁があるか
    {
        Vector3 cameraPosition = targetCamera.transform.position;
        Vector3 direction = targetPosition - cameraPosition;
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, direction, out hit, direction.magnitude, obstacleLayer))
        {
                return true; //カメラとターゲットの間に壁がある
        }
        
        //Debug.Log("壁ないよ");
        return false; // カメラとターゲットの間に壁がない
    }

    void ChaseTime()//逃走時間
    {
        if (EnemyState == Enemy.PlayerLook && !wasVisible)
        {
            Chasetime += Time.deltaTime;
            
        }
        else
        {
            Chasetime = 0;
        }
        //Debug.Log(Chasetime);
        if (Chasetime >= 5)
        {
            //Debug.Log("５秒経過");
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


    
    /*
    void OnCollisionEnter(Collision collision)
    {
        DoorOBJ=collision.gameObject;
        DoorOBJ.GetComponent<Animator>();
        
    }*/
}
