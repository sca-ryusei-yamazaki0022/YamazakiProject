	using UnityEngine;

public class MouseController : MonoBehaviour
{
	[Range(0.1f, 10f)]
	[SerializeField]private float lookSensitivity = 5f;
	[Range(0.1f, 1f)]
	[SerializeField] float lookSmooth = 0.1f;

	[SerializeField] Vector2 MinMaxAngle = new Vector2(-65, 65);
	//[SerializeField] Vector2 MinMaxAngle2 = new Vector2(-65, 65);

	private float yRot;
	private float xRot;

	private float currentYRot;
	private float currentXRot;

	private float yRotVelocity;
	private float xRotVelocity;
	GameManager gameManager;
	void Start()
    {
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}
	void Update()
	{
		if(gameManager.Pstop==false)
		{ 
		yRot += Input.GetAxis("Mouse X") * lookSensitivity;  // �}�E�X�̉��ړ�
		xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;  // �}�E�X�̏c�ړ�

		// �i�|�C���g�j�uClamp�v�̈Ӗ��Ǝg�������l�b�g�Œ��ׂ悤�I
		xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y); // �㉺�̊p�x�ړ��̍ő�A�ŏ�
		//yRot = Mathf.Clamp(yRot,MinMaxAngle2.x, MinMaxAngle2.y);
		// �i�|�C���g�j�uSmoothDamp�v�̈Ӗ��Ǝg�������l�b�g�Œ��ׂ悤�I
		currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
		currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

		transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
		}
	}
}