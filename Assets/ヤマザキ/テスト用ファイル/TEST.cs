using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TEST : MonoBehaviour
{

    public Camera targetCamera; // �t���O��Ԃ���������̃J����
    public GameObject targetObject; // �t���O��Ԃ���������̃I�u�W�F�N�g
    public LayerMask obstacleLayer; // �ǂ��Q���̃��C���[�}�X�N
    public bool flag = false; // �t���O
    private bool wasVisible = false; // �O��̉����
    private bool isChangingFlag = false; // �t���O�̕ύX�����ǂ���

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
            flag = false; // �I�u�W�F�N�g���J�����̕`��O�ɏo����t���O�����낷
            //return;
        }

        bool isVisible = IsVisibleFromCamera(targetObject) && !IsBehindCamera(targetPosition) && !IsObstacleBetweenCamera(targetPosition);

        if (isVisible && !wasVisible && !isChangingFlag)
        {
            flag = true; // �I�u�W�F�N�g���J�����Ɍ����ăJ�����̌��ɂ��Ȃ����ǂ��Ȃ��ꍇ�̓t���O�𗧂Ă�
            StartCoroutine(FlagChangeDelay()); // �t���O�ύX�̒x���������J�n
        }

        wasVisible = isVisible; // ���݂̉���Ԃ�ۑ�
        Debug.Log(flag);
    }

    private IEnumerator FlagChangeDelay()
    {
        isChangingFlag = true; // �t���O�ύX���t���O�𗧂Ă�

        yield return new WaitForSeconds(1.0f); // �t���O�ύX�̒x�����ԁi��Ƃ���1�b�j

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
            return true; // �J�����ƃ^�[�Q�b�g�̊Ԃɕǂ�����
        }

        return false; // �J�����ƃ^�[�Q�b�g�̊Ԃɕǂ��Ȃ�
    }
}
