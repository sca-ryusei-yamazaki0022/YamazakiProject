using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private Renderer objectRenderer;

    private void Start()
    {
        // �J���������̃J�����ɕύX���Ă��������i��: targetCamera = Camera.main�j
        
        objectRenderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!IsVisibleByCamera(targetCamera))
        {
            // �I�u�W�F�N�g���J�����ɉf���Ă��Ȃ��ꍇ�̏����������ɋL�q���܂�
            Debug.Log("�����ĂȂ�");
        }
        else
        {
            Debug.Log("�ʂ��Ă��[�[�[�[�[�[");
        }
    }

    private bool IsVisibleByCamera(Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);

        if (GeometryUtility.TestPlanesAABB(frustumPlanes, objectRenderer.bounds))
        {
            // �I�u�W�F�N�g���J�����ɉf���Ă���ꍇ
            return true;
        }

        // �I�u�W�F�N�g���J�����ɉf���Ă��Ȃ��ꍇ
        return false;
    }
}