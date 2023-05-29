using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
	
	public enum PlayerState
	{
		Idle, Walking, Running, Jumping
	}

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

		// ���ǉ�
		private GameObject FPSCamera;
		private Vector3 moveDir = Vector3.zero;

		void Start()
		{
			// ���ǉ�
			FPSCamera = GameObject.Find("Main Camera");

			charaController = GetComponent<CharacterController>();

			//cameraShake = Camera.main.GetComponent<CameraShake>();
		}

		void FixedUpdate()
		{
			Move();
		}

		void Move()
		{
			float moveH = Input.GetAxis("Horizontal");
			float moveV = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveH, 0, moveV);

			if (movement.sqrMagnitude > 1)
			{
				movement.Normalize();
			}

			// ���ǉ�
			Vector3 desiredMove = FPSCamera.transform.forward * movement.z + FPSCamera.transform.right * movement.x;
			moveDir.x = desiredMove.x * 5f;
			moveDir.z = desiredMove.z * 5f;

			// ���C��
			if (Input.GetKey(KeyCode.LeftShift))
			{
				charaController.Move(moveDir * Time.fixedDeltaTime * runSpeed);
				//cameraShake.Shake(shakeDuration, shakeIntensity);
			}
			else
			{
				charaController.Move(moveDir * Time.fixedDeltaTime * walkSpeed);
				//cameraShake.Shake(shakeDuration, shakeIntensity);
			}
		}
	}


}