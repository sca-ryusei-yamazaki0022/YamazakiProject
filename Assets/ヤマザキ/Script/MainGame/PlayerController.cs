using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
	
	

	[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
	public class PlayerController : MonoBehaviour
	{

		[Range(0.1f, 2f)]
		[SerializeField] float walkSpeed = 1.5f;
		[Range(0.1f, 10f)]
		[SerializeField] float runSpeed = 3.5f;
		//private CameraShake cameraShake;
		//private float shakeDuration = 0.2f;
		//private float shakeIntensity = 0.1f;

		private CharacterController charaController;
		[SerializeField] GameObject Mirror;
		//[SerializeField] Light light;
		[SerializeField] GameObject Map;
		[SerializeField] GameObject rayTestOBJ;
		int mouseDownCount = 0;
		// Åöí«â¡
		private GameObject FPSCamera;
		private Vector3 moveDir = Vector3.zero;
		float MaxStamina=5;
		float NowStamina=0;
		GameManager gameManager; 
		RayTest rayTest;
		void Start()
		{
			// Åöí«â¡
			FPSCamera = GameObject.Find("Main Camera");
			gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
			rayTest = rayTestOBJ.GetComponent<RayTest>();
			charaController = GetComponent<CharacterController>();
			Mirror.SetActive(false);
			Map.SetActive(false);
			//light.range = 0.0f;
			//cameraShake = Camera.main.GetComponent<CameraShake>();
		}

		void Update()
        {
			if (!gameManager.Pstop)
			{
				//Debug.Log(gameManager.Pstop);
				WalkMove();
			}
			if (Input.GetMouseButtonDown(1))
			{
				ViewMM();//Debug.Log(NowStamina);
			}

		}

		void FixedUpdate()
		{
			
		}

		void WalkMove()
		{
			float moveH = Input.GetAxis("Horizontal");
			float moveV = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveH, 0, moveV);

			if (movement.sqrMagnitude > 1)
			{
				movement.Normalize();
			}

			// Åöí«â¡
			Vector3 desiredMove = FPSCamera.transform.forward * movement.z + FPSCamera.transform.right * movement.x;
			moveDir.x = desiredMove.x * 5f;
			moveDir.z = desiredMove.z * 5f;
			
			//Debug.Log(moveDir.y);
			
			// ÅöèCê≥
			//if(gameManager.Pstop)
			//{ 
			if (Input.GetKey(KeyCode.LeftShift))
			{
				RunMove();
				//cameraShake.Shake(shakeDuration, shakeIntensity);
			}
			else
			{
				charaController.Move(moveDir * Time.fixedDeltaTime * walkSpeed);
				NowStamina-= Time.deltaTime;
                if (NowStamina <= 0) { NowStamina=0;}
				//cameraShake.Shake(shakeDuration, shakeIntensity);
			}
		//}
		}
		void RunMove()
        {
			NowStamina+= Time.deltaTime;
			if (MaxStamina>NowStamina)
			{ 
				charaController.Move(moveDir * Time.fixedDeltaTime * runSpeed);
				//GameManager.PlayerState.pl= PlayerState.Walking;
				//Debug.Log("hasu");
			}
			else
            {
				charaController.Move(moveDir * Time.fixedDeltaTime * walkSpeed);
				Debug.Log("ëñÇÍÇ»Ç¢");
            }
		}

		void ViewMM()
		{

			
			if (Input.GetMouseButtonDown(1)&&mouseDownCount==0)
			{
				mouseDownCount++;
				if (rayTest.mirror && rayTest.map)
				{
					Mirror.SetActive(true); Map.SetActive(true);
				}
				else if (rayTest.mirror)
				{
					Mirror.SetActive(true);Map.SetActive(false);
				}
				else if (rayTest.map)
				{
					Mirror.SetActive(false);Map.SetActive(true); mouseDownCount = 0;
				}
				else
                {
					mouseDownCount = 0;
				}
				
			}
			else if (Input.GetMouseButtonDown(1) && mouseDownCount == 1)
			{
				Mirror.SetActive(false); Map.SetActive(false); mouseDownCount = 0;
			}
		}
		
            
	}

}