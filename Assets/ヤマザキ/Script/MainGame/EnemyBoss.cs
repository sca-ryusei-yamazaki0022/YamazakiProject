using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;//UI���g�p����Ƃ��ɕK�v

public class EnemyBoss : MonoBehaviour
{
    public Camera targetCamera; // �t���O��Ԃ���������̃J����
    public GameObject targetObject; // �t���O��Ԃ���������̃I�u�W�F�N�g
    public LayerMask obstacleLayer; // �ǂ��Q���̃��C���[�}�X�N

    public bool flag = false; // �t���O
    private bool wasVisible = false; // �O��̉����
    private bool isChangingFlag = false; // �t���O�̕ύX�����ǂ���
    //private bool TimeBool;//�T�b���������������邽��
    bool GetAngry;//�{���Ă���t���O
    bool Onecount = true;
    // ����n�_�I�u�W�F�N�g���i�[����z��
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Magic;
    //[SerializeField] private Transform playerT;
    // ����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    private int bPoint = 0;
    private float Chasetime;//���񃂁[�h�ɖ߂�Ƃ���10�b�Ԍ����Ă��Ȃ����̊m�F
    private NavMeshAgent agent;// NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private bool Branch;//����_�����߂�
    public Enemy EnemyState;//�G�̏�Ԃ�ENUM��������o���֐�
    GameManager gameManager;
    private Animator animator;
    private Animator PAnimator;
    private Animator Door;
    private GameObject DoorOBJ;
    bool UseE = true;
    bool DoorFlag;
    private Animator DoorAnim;
    // bool playerHide;//�v���C���[���G�ɂ����Ă���ۂɉB��Ă��邩�̃t���O
    //[SerializeField] private Text a;//�e�L�X�g���A�^�b�`����
    private GameObject BreakDoor;
    [SerializeField] private AudioClip Shout;//����
    [SerializeField] private AudioClip Flinch;//����
    [SerializeField] private AudioClip Walk;//����
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
        Patrol,//����
        PlayerLook,//�{��
        Frightening,//����
        ItemFrightening,//�A�C�e���ł̋���
        Capture,//�ߊl
        DoorAttack,//Door�����������p
        end
    }

    void Start()
    {

        //rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        //�Q�[���Ȃˁ[�W���[���i�[
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        // ����n�_�Ԃ̈ړ����p�������邽�߂Ɏ����u���[�L�𖳌���
        //�i�G�[�W�F���g�͖ړI�n�_�ɋ߂Â��Ă��������Ȃ�)
        agent.autoBraking = false;
        // ���̏���n�_�̏��������s
        GotoNextPoint();
        //Branchpoint();
        EnemyState = Enemy.Patrol;
        animator = GetComponent<Animator>();//animator�i�[
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
            case Enemy.Patrol://����
                animator.SetBool("Run", false);
                this.agent.speed = 3.5f*NavSpeed;
                if (!agent.pathPending && agent.remainingDistance < 0.3f)
                {
                    
                    // ���̏���n�_��ݒ肷�鏈�������s
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

            case Enemy.PlayerLook://�{��
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
                //Debug.Log("�v���C���[����");
                audioSourceBig.PlayOneShot(Shout, 0.3f);
                
                ChaseTime();
                break;

            case Enemy.Frightening://����
                EnemyFrightening();
                audioSourceSmall.loop = false;
                audioSourceSmall.PlayOneShot(Flinch);
                //Debug.Log("�v���C���[�A�C�e���g�p");
                break;

            case Enemy.ItemFrightening://�A�C�e���ł̋���
                Debug.Log("�����Ă�");
                StartCoroutine(EnemyItemiFrightening());
                break;
            case Enemy.Capture://�ߊl
                Predation();

                
                //Debug.Log("�v���C���[��߂܂���");
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
            //Debug.Log("�������Ă�");
        }
        else
        {
            //Debug.Log("�������ĂȂ�");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    // ���̏���n�_��ݒ肷�鏈��
    void GotoNextPoint()//���Ɍ������ׂ��ꏊ�̑I��
    {
        // ����n�_���ݒ肳��Ă��Ȃ����
        if (points.Length == 0)
            // ������Ԃ��܂�
            return;
        // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
        agent.destination = points[destPoint].position;
        // �z��̒����玟�̏���n�_��I���i�K�v�ɉ����ČJ��Ԃ��j
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
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsFour[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsFour.Length;
                //Debug.Log(bPoint);
                break;
            case 9:
                if (BranchpointsOne.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsOne[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsOne.Length;
                //Debug.Log(bPoint);
                break;
            case 10:
                if (BranchpointsTwo.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsTwo[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsTwo.Length;
                //Debug.Log(bPoint);
                break;
            case 12:
                if (BranchpointsThree.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsThree[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsThree.Length;
                //Debug.Log(bPoint);
                break;



        }
    }//���Ɍ������ׂ��ꏊ�̑I��i����n�_�̐؂�ւ����j
    void Branchpoint()
    {
        Branch = Random.value > 0.5f;
    }//����n�_�̐؂�ւ�
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door")&& !Capture)
        {
            var Animetor = other.transform.parent.parent.gameObject.GetComponent<Animator>();
            DoorAnim = other.transform.parent.parent.gameObject.GetComponent<Animator>();
            DoorFlag = Animetor.GetBool("Door");
            if(!DoorFlag)
            {
                Debug.Log("�J���ĂȂ�");
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
                Debug.Log("�J���ĂȂ�");
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
                    //Debug.Log("8�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 14;
                    Branch = false;
                    break;
                case 9:
                    //Debug.Log("�X�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 9;
                    Branch = false;
                    break;
                case 10:
                    //Debug.Log("10�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
                case 12:
                    //Debug.Log("12��0�S���Ă܂�");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
            }

        }
    }//�����̏��񂵂Ă���Ƃ�����m�F

    public void GameOver(Collider other)
    {
        // Debug.Log("������������q");
        if(!EnemyOne&&EnemyState!=Enemy.ItemFrightening)
        { 
            EnemyState = Enemy.Capture;
            
            Capture=true;
            agent.destination = this.gameObject.transform.position;
            agent.enabled = false;
            EnemyOne = true;
        }
    }//�Q�[���I�[�o�[���̂Ă�Boss �̓���



    void Predation()//�ߐH��
    {
        //Debug.Log("TOOOOOOO");

        //Debug.Log("a");
        //�����ŃA�j���[�V�����Đ��n��ݒ�
        animator.SetBool("MissAttackRun", false);
        animator.SetBool("WalkAttack", false);
        animator.SetBool("RunAttack", true);
        animator.SetBool("WalkAttack", true);


        
        Onecount = true; UseE = true;
        //UseE=true;
        if (Input.GetKey(KeyCode.E) && gameManager.NowFlashCount != 0)
        {
            PAnimator.SetBool("GameOver", false);
            EnemyState = Enemy.Frightening;//���݂ɕύX
            gameManager.NowFlashCount -= 1;
            CancelInvoke("SceneGameover");
            
            //Debug.Log("�����ŌĂ΂ꂽ��");
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
    void SceneGameover()//�Q�[���I�[�o�[��ʂɂ����邾��
    {
        EnemyState = Enemy.end;
        gameManager.PredationScene();
    }
    void Camera()
    {
        Debug.Log("�{�X�ړ�");
        if (targetCamera == null || targetObject == null)
        {
            return;
        }

        Vector3 targetPosition = targetObject.transform.position;
        Vector3 viewportPosition = targetCamera.WorldToViewportPoint(targetPosition);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1 || viewportPosition.z < 0 || viewportPosition.z > targetCamera.farClipPlane)
        {
            flag = false; // �I�u�W�F�N�g���J�����̕`��O�ɏo����t���O�����낷
            //EnemyState = Enemy.Patrol;
            //return;
        }

        bool isVisible = IsVisibleFromCamera(targetObject) && !IsBehindCamera(targetPosition) && !IsObstacleBetweenCamera(targetPosition);

        if (isVisible && !wasVisible) //&& !isChangingFlag)
        {
            flag = true; // �I�u�W�F�N�g���J�����Ɍ����ăJ�����̌��ɂ��Ȃ����ǂ��Ȃ��ꍇ�̓t���O�𗧂Ă�
            //EnemyState = Enemy.PlayerLook;//Enum�ύX
            //StartCoroutine(FlagChangeDelay()); // �t���O�ύX�̒x���������J�n
        }
       
        wasVisible = isVisible; // ���݂̉���Ԃ�ۑ�
        //Debug.Log(isVisible);
    }//�J�����Ɏʂ��Ă��邩�̊m�F

    void EnemyFrightening()//�ߐH���ɃA�C�e�����g��ꂽ
    {

        Debug.Log("�Ђ�܂����");
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
        yield return new WaitForSeconds(2.0f); //�x��
        agent.enabled = true;//Nav����ɖ߂�
        EnemyState = Enemy.PlayerLook;
        Capture = false;
        UseE = true; EnemyOne = false;
        animator.SetBool("MissAttackRun", true); animator.SetBool("RunAttack", false); animator.SetBool("MissAttack", false);
    }

   
   
    private bool IsVisibleFromCamera(GameObject obj)//�J�����O
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null || !renderer.enabled)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    private bool IsBehindCamera(Vector3 targetPosition)//�J�����̌��ɂ��邩�ǂ���
    {
        Vector3 cameraToTarget = targetPosition - targetCamera.transform.position;
        Vector3 cameraForward = targetCamera.transform.forward;


        if (Vector3.Dot(cameraToTarget, cameraForward) <= 0)
        {
            return true; // �I�u�W�F�N�g���J�����̌��ɂ���
        }

        return false; // �I�u�W�F�N�g���J�����̑O�ɂ���
    }

    private bool IsObstacleBetweenCamera(Vector3 targetPosition)//�Ԃɕǂ����邩
    {
        Vector3 cameraPosition = targetCamera.transform.position;
        Vector3 direction = targetPosition - cameraPosition;
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, direction, out hit, direction.magnitude, obstacleLayer))
        {
                return true; //�J�����ƃ^�[�Q�b�g�̊Ԃɕǂ�����
        }
        
        //Debug.Log("�ǂȂ���");
        return false; // �J�����ƃ^�[�Q�b�g�̊Ԃɕǂ��Ȃ�
    }

    void ChaseTime()//��������
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
            //Debug.Log("�T�b�o��");
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
