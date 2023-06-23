using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;  // �h��̋���
    public float shakeSpeed = 1.0f;  // �h��̑��x

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // ���s���̗h���\��
        float shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.localPosition = originalPosition + new Vector3(0f, shakeOffset, 0f);
    }
}