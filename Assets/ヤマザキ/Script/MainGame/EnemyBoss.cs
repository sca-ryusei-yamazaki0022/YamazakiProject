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

    float distance = 100; // ��΂�&�\������Ray�̒���
    float duration = 3;   // �\�����ԁi�b�j
    bool GetAngry;//�{���Ă���t���O

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
    private Enemy EnemyState;//�G�̏�Ԃ�ENUM��������o���֐�


    public enum Enemy
    {
        Patrol,//����
        PlayerLook,//�{��
        Frightening,//����
        Capture//�ߊl
    }


    void Start()
    {
        rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        // ����n�_�Ԃ̈ړ����p�������邽�߂Ɏ����u���[�L�𖳌���
        //�i�G�[�W�F���g�͖ړI�n�_�ɋ߂Â��Ă��������Ȃ�)
        agent.autoBraking = false;
        // ���̏���n�_�̏��������s
        GotoNextPoint();
        //Branchpoint();
        EnemyState = Enemy.Patrol;
    }

    void FixedUpdate()
    {
        //Debug.Log(destPoint);
        switch (EnemyState)
        {
            case Enemy.Patrol://����
                //Debug.Log("����");
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
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
                }
                break;
            case Enemy.PlayerLook://�{��
                //Debug.Log("�v���C���[����");
                agent.destination = player.transform.position;
                ChaseTime();
                break;
            case Enemy.Frightening://����
                //Debug.Log("�v���C���[�A�C�e���g�p");
                break;
            case Enemy.Capture://�ߊl
                //Debug.Log("�v���C���[��߂܂���");
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
            //����hit�̃^�O��"Player"�ƈ�v���Ă����ꍇ�D�D�D�̏������e
            EnemyState = Enemy.PlayerLook;
            Chasetime = 0;
        }
       
       
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
                    Debug.Log("8�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 14;
                    Branch = false;
                    break;
                case 9:
                    Debug.Log("�X�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 9;
                    Branch = false;
                    break;
                case 10:
                    Debug.Log("10�łO�S���Ă܂�");
                    bPoint = 0; destPoint = 13;
                    Branch = false;
                    break;
                case 12:
                    Debug.Log("12��0�S���Ă܂�");
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
            Debug.Log("�T�b�o��");
            EnemyState = Enemy.Patrol;
        }
        else
        {
            EnemyState = Enemy.PlayerLook;
        }
    }
}
