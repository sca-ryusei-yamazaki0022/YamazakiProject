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
		yRot += Input.GetAxis("Mouse X") * lookSensitivity;  // マウスの横移動
		xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;  // マウスの縦移動

		// （ポイント）「Clamp」の意味と使い方をネットで調べよう！
		xRot = Mathf.Clamp(xRot, MinMaxAngle.x, MinMaxAngle.y); // 上下の角度移動の最大、最小
		//yRot = Mathf.Clamp(yRot,MinMaxAngle2.x, MinMaxAngle2.y);
		// （ポイント）「SmoothDamp」の意味と使い方をネットで調べよう！
		currentXRot = Mathf.SmoothDamp(currentXRot, xRot, ref xRotVelocity, lookSmooth);
		currentYRot = Mathf.SmoothDamp(currentYRot, yRot, ref yRotVelocity, lookSmooth);

		transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0);
		}
	}
}