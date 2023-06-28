using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TEST : MonoBehaviour
{

    public Camera targetCamera; // フラグを返したい特定のカメラ
    public GameObject targetObject; // フラグを返したい特定のオブジェクト
    public LayerMask obstacleLayer; // 壁や障害物のレイヤーマスク
    public bool flag = false; // フラグ
    private bool wasVisible = false; // 前回の可視状態
    private bool isChangingFlag = false; // フラグの変更中かどうか

    private void Start()
    {
        //targetRenderer = targetObject.GetComponent<Renderer>();
    }
    private void Update()
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
            //return;
        }

        bool isVisible = IsVisibleFromCamera(targetObject) && !IsBehindCamera(targetPosition) && !IsObstacleBetweenCamera(targetPosition);

        if (isVisible && !wasVisible && !isChangingFlag)
        {
            flag = true; // オブジェクトがカメラに見えてカメラの後ろにいないかつ壁がない場合はフラグを立てる
            StartCoroutine(FlagChangeDelay()); // フラグ変更の遅延処理を開始
        }

        wasVisible = isVisible; // 現在の可視状態を保存
        Debug.Log(flag);
    }

    private IEnumerator FlagChangeDelay()
    {
        isChangingFlag = true; // フラグ変更中フラグを立てる

        yield return new WaitForSeconds(1.0f); // フラグ変更の遅延時間（例として1秒）

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
            return true; // カメラとターゲットの間に壁がある
        }

        return false; // カメラとターゲットの間に壁がない
    }
}
