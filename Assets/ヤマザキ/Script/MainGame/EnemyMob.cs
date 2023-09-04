using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// "NavMeshAgent"�֘A�N���X���g�p�ł���悤�ɂ���
using UnityEngine.AI;

public class EnemyMob : MonoBehaviour
{
    // ����n�_�I�u�W�F�N�g���i�[����z��
    public Transform[] points;
    // ����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    // NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;
    private NavMeshAgent agentBoss;
    private EnemyMobClass EnemyMobState;
    private EnemyBoss enemyBoss;
    [SerializeField] private GameObject EnemyBossP;//�Ăяo��BOSS�̃I�u�W�F�N�g��ݒ�
    [SerializeField] private GameObject SummoningPoints;//�Ăяo���|�C���g
    [SerializeField] private GameObject BoxColloder;//Cry�ɓ��������ɃI���g���K�[���Ăяo���Ȃ����߂�
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;
    bool OnCount = true;//�G�̍��W������͈̂�񂾂�
    private Animator animator;

    public enum EnemyMobClass
    {
        Patrol,//����
        Cry//�Ȃ�
    }
    // �Q�[���X�^�[�g���̏���
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        agentBoss = EnemyBossP.GetComponent<NavMeshAgent>();
        // ����n�_�Ԃ̈ړ����p�������邽�߂Ɏ����u���[�L�𖳌���
        //�i�G�[�W�F���g�͖ړI�n�_�ɋ߂Â��Ă��������Ȃ�)
        agent.autoBraking = false;
        // ���̏���n�_�̏��������s
        GotoNextPoint();
        EnemyMobState = EnemyMobClass.Patrol;
        enemyBoss = GameObject.FindWithTag("EnemyBoss").GetComponent<EnemyBoss>();
        animator = GetComponent<Animator>();//animator�i�[

    }



    // �Q�[�����s���̌J��Ԃ�����
    void FixedUpdate()
    {
        //ENUM�̓��e���m�F
        switch (EnemyMobState)
        {
            case EnemyMobClass.Patrol:
                // �G�[�W�F���g�����݂̏���n�_�ɓ��B������
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    // ���̏���n�_��ݒ肷�鏈�������s
                    GotoNextPoint();
                break;
            case EnemyMobClass.Cry:
                Invoke("MobCry", 3.0f);
                Invoke("Mob", 7.0f);
                break;
        }
        //Debug.Log(EnemyMobState);
    }

    // ���̏���n�_��ݒ肷�鏈��
    void GotoNextPoint()
    {
        //Debug.Log("�Ă΂�Ă܂�");
        // ����n�_���ݒ肳��Ă��Ȃ����
        if (points.Length == 0)
            // ������Ԃ��܂�
            return;
        // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
        agent.destination = points[destPoint].position;
        // �z��̒����玟�̏���n�_��I���i�K�v�ɉ����ČJ��Ԃ��j
        destPoint = (destPoint + 1) % points.Length;
    }
    void MobCry()
    {
        if (OnCount)
        {

            animator.SetBool("Cry", true);
            audioSource.PlayOneShot(sound);
            agent.destination = this.gameObject.transform.position;
            agent.enabled = false;
            agentBoss.enabled = false;
            EnemyBossP.transform.position = new Vector3(SummoningPoints.gameObject.transform.position.x, SummoningPoints.gameObject.transform.position.y, SummoningPoints.gameObject.transform.position.z);
            enemyBoss.EnemyState = EnemyBoss.Enemy.PlayerLook;
            agentBoss.enabled = true;
            //transform.LookAt(transform.position);
            OnCount = false;
        }
        //Debug.Log("�Ȃ���[");
    }
    void Mob()
    {
        agent.enabled = true;
        EnemyMobState = EnemyMobClass.Patrol;
        animator.SetBool("Cry", false);
        BoxColloder.SetActive(true);


    }
    /*
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            BoxColloder.SetActive(false);
            EnemyMobState = EnemyMobClass.Cry;
            OnCount = true;
        }
    }
    */
}
