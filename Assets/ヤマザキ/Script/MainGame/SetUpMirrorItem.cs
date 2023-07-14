using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SetUpMirrorItem : MonoBehaviour
{
    [SerializeField] GameObject IMirror1;
    [SerializeField] GameObject IMirror2;
    [SerializeField] GameObject IMirror3;
    private String tag;
    Quaternion test;
    int mirror1 = 1;
    int mirror2 = 1;
    int count1=3;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        // 0�ȏ�̐�����Point�̐��������񂾔z��
        int[] array1 = Enumerable.Range(0, transform.childCount).ToArray();
        // array1���V���b�t������
        int[] array2 = array1.OrderBy(i => Guid.NewGuid()).ToArray();


        // �z�u����A�C�e���̐�
        //int count = 10;
        int[] Mirror1 = Enumerable.Range(0, transform.childCount).ToArray();
        // array1���V���b�t������
        int[] Mirror2 = Mirror1.OrderBy(i => Guid.NewGuid()).ToArray();

        

        for (int n = 0; n < count1; n++)
        {


            tag = transform.GetChild(Mirror2[n]).gameObject.tag;

            if (mirror1 > n)
            {
                Rotation(); tagSet();
                Instantiate(IMirror1, transform.GetChild(Mirror2[n]).position, test);
                
            }
            else if(mirror2 > n)
            {
                Rotation(); tagSet();
                Instantiate(IMirror2, transform.GetChild(Mirror2[n]).position, test);

            }
            else
            {
                Rotation(); tagSet();
                Instantiate(IMirror3, transform.GetChild(Mirror2[n]).position, test);
            }
        }
        // �j��
        Destroy(gameObject);
    }
    void tagSet()
    {
        ++count;
        switch (count)
        {
            case 1:
                IMirror1.tag = "Mirror1";
                break;
            case 2:
                IMirror2.tag = "Mirror2";
                break;
            case 3:
                IMirror3.tag = "Mirror3";
                break;

        }
    }
    void Rotation()
    {
        switch (tag)
        {
            case "Up":
                test = Quaternion.Euler(90, 0f, 0);
                Debug.Log("�ゾ��[");
                break;
            case "Down":
                test = Quaternion.Euler(90, 180f, 0);
                Debug.Log("sita����[");
                break;
            case "Left":
                test = Quaternion.Euler(90, 270f, 0);
                //SquareCorners.transform.Rotate(new Vector3(0, 180, 0));
                Debug.Log("hidari����[");
                break;
            case "Right":
                test = Quaternion.Euler(90, 90f, 0);
                //SquareCorners.transform.Rotate(new Vector3(0, 0, 0));
                Debug.Log("migi����[");
                break;

        }
    }
}
