using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// "NavMeshAgent"�֘A�N���X���g�p�ł���悤�ɂ���
using UnityEngine.AI;
 
public class Patrol : MonoBehaviour
{
    // ����n�_�I�u�W�F�N�g���i�[����z��
    [SerializeField]private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    // ����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    private int bPoint=0;

    // NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;
    private bool Branch;

    // �Q�[���X�^�[�g���̏���
    void Start()
    {
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        // ����n�_�Ԃ̈ړ����p�������邽�߂Ɏ����u���[�L�𖳌���
        //�i�G�[�W�F���g�͖ړI�n�_�ɋ߂Â��Ă��������Ȃ�)
        agent.autoBraking = false;
        // ���̏���n�_�̏��������s
        GotoNextPoint();
        //Branchpoint();
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
        
        switch (destPoint) { 
            case 3:
                destPoint+=4;
                Branch=false;
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
            case 12 :
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


    // �Q�[�����s���̌J��Ԃ�����
    void FixedUpdate()
    {
        //Debug.Log(bPoint);
        //Debug.Log(destPoint);
        // �G�[�W�F���g�����݂̏���n�_�ɓ��B������
        if (!agent.pathPending && agent.remainingDistance < 0.05f) {
            // ���̏���n�_��ݒ肷�鏈�������s
            if (!Branch) { 
            GotoNextPoint(); }
            else{
                GotoBranchPoint();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AAAAAAAAA");
        if(other.gameObject.CompareTag("BPoint"))
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
                    bPoint=0; destPoint=9;
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
    
}