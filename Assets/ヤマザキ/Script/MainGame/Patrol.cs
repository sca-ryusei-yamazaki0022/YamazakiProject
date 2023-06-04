using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// "NavMeshAgent"関連クラスを使用できるようにする
using UnityEngine.AI;
 
public class Patrol : MonoBehaviour
{
    // 巡回地点オブジェクトを格納する配列
    [SerializeField]private Transform[] points;
    [SerializeField] private Transform[] BranchpointsOne;
    [SerializeField] private Transform[] BranchpointsTwo;
    [SerializeField] private Transform[] BranchpointsThree;
    [SerializeField] private Transform[] BranchpointsFour;
    // 巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    private int bPoint=0;

    // NavMesh Agent コンポーネントを格納する変数
    private NavMeshAgent agent;
    private bool Branch;

    // ゲームスタート時の処理
    void Start()
    {
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        // 巡回地点間の移動を継続させるために自動ブレーキを無効化
        //（エージェントは目的地点に近づいても減速しない)
        agent.autoBraking = false;
        // 次の巡回地点の処理を実行
        GotoNextPoint();
        //Branchpoint();
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
        
        switch (destPoint) { 
            case 3:
                destPoint+=4;
                Branch=false;
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
            case 12 :
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


    // ゲーム実行中の繰り返し処理
    void FixedUpdate()
    {
        //Debug.Log(bPoint);
        //Debug.Log(destPoint);
        // エージェントが現在の巡回地点に到達したら
        if (!agent.pathPending && agent.remainingDistance < 0.05f) {
            // 次の巡回地点を設定する処理を実行
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
                    Debug.Log("8で０担ってます");
                    bPoint = 0; destPoint = 14;
                    Branch = false;
                    break;
                case 9:
                    Debug.Log("９で０担ってます");
                    bPoint=0; destPoint=9;
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
    
}