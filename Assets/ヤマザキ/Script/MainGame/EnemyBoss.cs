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
    bool Onecount=true;
    // ����n�_�I�u�W�F�N�g���i�[����z��
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    [SerializeField] private GameObject player;
    // ����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    private int bPoint = 0;
    private float Chasetime;//���񃂁[�h�ɖ߂�Ƃ���10�b�Ԍ����Ă��Ȃ����̊m�F
    private NavMeshAgent agent;// NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private bool Branch;//����_�����߂�
    public Enemy EnemyState;//�G�̏�Ԃ�ENUM��������o���֐�
    GameManager gameManager;
    private Animator animator;
    bool UseE=true;

    [SerializeField] private Text a;//�e�L�X�g���A�^�b�`����
    public enum Enemy
    {
        Patrol,//����
        PlayerLook,//�{��
        Frightening,//����
        Capture//�ߊl
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
    }

    void FixedUpdate()
    {
        if(flag)
        { 
        a.text = "�������Ă�";//�e�L�X�g�̒��g��ύX
        }
        else
        {
            a.text="�������ĂȂ�";
        }
        Camera();
        //Debug.Log(EnemyState);
        switch (EnemyState)
        {
            case Enemy.Patrol://����
                //Debug.Log("����");
                
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    //Debug.Log("����ɓ���܂���");
                    //GotoNextPoint();
                    //Debug.Log(Branch);
                    // ���̏���n�_��ݒ肷�鏈�������s
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
            case Enemy.PlayerLook://�{��
                animator.SetBool("Run", true);
                //Debug.Log("�v���C���[����");
                agent.destination = player.transform.position;
                ChaseTime();
                break;
            case Enemy.Frightening://����
                EnemyFrightening();
                //Debug.Log("�v���C���[�A�C�e���g�p");
                break;
            case Enemy.Capture://�ߊl
                Predation();
                //Debug.Log("�v���C���[��߂܂���");
                break;
        }
        if(flag)
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
    void GotoNextPoint()
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
                Debug.Log(bPoint);
                break;
            case 9:
                if (BranchpointsOne.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsOne[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsOne.Length;
                Debug.Log(bPoint);
                break;
            case 10:
                if (BranchpointsTwo.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
                agent.destination = BranchpointsTwo[bPoint].position;
                bPoint = (bPoint + 1); //% BranchpointsTwo.Length;
                Debug.Log(bPoint);
                break;
            case 12:
                if (BranchpointsThree.Length == 0)
                    // ������Ԃ��܂�
                    return;
                // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
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
    }

    public void GameOver(Collider other)
    {
       // Debug.Log("������������q");
        EnemyState = Enemy.Capture;
        agent.destination = this.gameObject.transform.position;
        agent.enabled = false;
        //Debug.Log(EnemyState);
    }
    
    void ChaseTime()//��������
    {
        if(flag&& EnemyState == Enemy.PlayerLook)
        { 
            Chasetime += Time.deltaTime;
            //Debug.Log("��]����");
        }
        else
        {
            Chasetime=0;
        }
        //Debug.Log(Chasetime);
        if (Chasetime >= 5)
        {
            Debug.Log("�T�b�o��");
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

    void Predation()//�ߐH��
    {
        //Debug.Log("a");
        //�����ŃA�j���[�V�����Đ��n��ݒ�
        if (flag) {
            animator.SetBool("RunAttack", true);
        }
        else{
        animator.SetBool("WalkAttack", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            EnemyState=Enemy.Frightening;//���݂ɕύX
            CancelInvoke("SceneGameover");
            //Debug.Log("�����ŌĂ΂ꂽ��");
            UseE =false;
        }
        else if (Onecount&&UseE)
        {
            Invoke("SceneGameover", 3.0f);
            Debug.Log("�����ŌĂ΂ꂽ��");
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
            flag = false; // �I�u�W�F�N�g���J�����̕`��O�ɏo����t���O�����낷
            //EnemyState = Enemy.Patrol;
            //return;
        }

        bool isVisible = IsVisibleFromCamera(targetObject) && !IsBehindCamera(targetPosition) && !IsObstacleBetweenCamera(targetPosition);

        if (isVisible && !wasVisible && !isChangingFlag)
        {
            flag = true; // �I�u�W�F�N�g���J�����Ɍ����ăJ�����̌��ɂ��Ȃ����ǂ��Ȃ��ꍇ�̓t���O�𗧂Ă�
            EnemyState = Enemy.PlayerLook;//Enum�ύX
            StartCoroutine(FlagChangeDelay()); // �t���O�ύX�̒x���������J�n
        }
        /*
        else if(EnemyState == Enemy.PlayerLook && TimeBool)
        {
            //Debug.Log("�p�g���[���ɖ߂�����");
            EnemyState = Enemy.Patrol;
            animator.SetBool("Run", false);
            TimeBool =false;
        }
        */
        wasVisible = isVisible; // ���݂̉���Ԃ�ۑ�
    }

    void EnemyFrightening()
    {
        Debug.Log("�Ђ�܂����");
        animator.SetBool("MissAttack", true);
        StartCoroutine(MissAttackDelay());
    }
    private IEnumerator MissAttackDelay()
    {
        yield return new WaitForSeconds(3.0f); //�x��
        agent.enabled = true;//Nav����ɖ߂�
        EnemyState = Enemy.PlayerLook;
        UseE=true;
        animator.SetBool("MissAttackRun", true); animator.SetBool("RunAttack", false); animator.SetBool("MissAttack", false);
    }

    private IEnumerator FlagChangeDelay()
    {
        isChangingFlag = true; // �t���O�ύX���t���O�𗧂Ă�

        yield return new WaitForSeconds(0.1f); // �t���O�ύX�̒x�����ԁi��Ƃ���0.1�b�j

        isChangingFlag = false; // �t���O�ύX���t���O������

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
            return true; // �I�u�W�F�N�g���J�����̌��ɂ���
        }

        return false; // �I�u�W�F�N�g���J�����̑O�ɂ���
    }

    private bool IsObstacleBetweenCamera(Vector3 targetPosition)
    {
        Vector3 cameraPosition = targetCamera.transform.position;
        Vector3 direction = targetPosition - cameraPosition;
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, direction, out hit, direction.magnitude, obstacleLayer))
        {
            //Debug.Log("�ǂ����");
            return true; //�J�����ƃ^�[�Q�b�g�̊Ԃɕǂ�����
        }
        //Debug.Log("�ǂȂ���");
        return false; // �J�����ƃ^�[�Q�b�g�̊Ԃɕǂ��Ȃ�
    }
}
