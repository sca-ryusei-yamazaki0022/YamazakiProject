using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed;
    Rigidbody rb;

    void Start()
    {
        speed = 5.0f;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 position = new Vector3(moveX, 0, moveZ);
        rb.velocity = position.normalized * speed;
        Debug.Log(rb.velocity);
    }
}

