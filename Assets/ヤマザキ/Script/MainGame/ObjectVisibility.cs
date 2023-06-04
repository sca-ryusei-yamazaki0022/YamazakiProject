using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private Renderer objectRenderer;

    private void Start()
    {
        // カメラを特定のカメラに変更してください（例: targetCamera = Camera.main）
        
        objectRenderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!IsVisibleByCamera(targetCamera))
        {
            // オブジェクトがカメラに映っていない場合の処理をここに記述します
            Debug.Log("うつってない");
        }
        else
        {
            Debug.Log("写ってるよーーーーーー");
        }
    }

    private bool IsVisibleByCamera(Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);

        if (GeometryUtility.TestPlanesAABB(frustumPlanes, objectRenderer.bounds))
        {
            // オブジェクトがカメラに映っている場合
            return true;
        }

        // オブジェクトがカメラに映っていない場合
        return false;
    }
}