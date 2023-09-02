using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorText : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject Text;
    [SerializeField] private AudioClip DoorLock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player") // Tag�ƕϐ���������������
        {
            audioSource.PlayOneShot(DoorLock);
        }
        //Debug.Log("�������Ă���");
    }
}
