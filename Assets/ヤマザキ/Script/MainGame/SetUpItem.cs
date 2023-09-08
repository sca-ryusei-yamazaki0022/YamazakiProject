using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class SetUpItem : MonoBehaviour
{
    [SerializeField] GameObject Trap;
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Wepon;
    private String tag;
    private GameObject SquareCorners;
    //[SerializeField] [Range(0f, 1f)] float capacity;
    int WeponC=1;
    int TrapC=2;
    Quaternion test;

    // Start is called before the first frame update
    void Start()
    {
        
        // 0�ȏ�̐�����Point�̐��������񂾔z��
        int[] array1 = Enumerable.Range(0, transform.childCount).ToArray();
        // array1���V���b�t������
        int[] array2 = array1.OrderBy(i => Guid.NewGuid()).ToArray();


        // �z�u����A�C�e���̐�
        int count = 10;
        // �A�C�e���z�u
        for (int n = 0; n < count; n++)
        {
            
          
            tag = transform.GetChild(array2[n]).gameObject.tag;
           
            if (WeponC>n)
            {
                Rotation();
                Instantiate(Wepon, transform.GetChild(array2[n]).position, test);
            
            }
            else if(TrapC >= n)
            {
                Rotation();
                Instantiate(Trap, transform.GetChild(array2[n]).position, test);
            }
            else
            {
                Rotation();
                Instantiate(Light, transform.GetChild(array2[n]).position, test);

            }
        }
        // �j��
        Destroy(gameObject);
        
    }
    void Rotation()
    {
       switch(tag)
        {
            case "Up":
                test = Quaternion.Euler(0, 0f, 0);
                Debug.Log("�ゾ��[");
                break;
            case "Down":
                test = Quaternion.Euler(0, 180f, 0);
                Debug.Log("sita����[");
                break;
            case "Left":
                test = Quaternion.Euler(0, 270f, 0);
                //SquareCorners.transform.Rotate(new Vector3(0, 180, 0));
                Debug.Log("hidari����[");
                break;
            case "Right":
                test = Quaternion.Euler(0, 90f, 0);
                //SquareCorners.transform.Rotate(new Vector3(0, 0, 0));
                Debug.Log("migi����[");
                break;

        }
    }
}