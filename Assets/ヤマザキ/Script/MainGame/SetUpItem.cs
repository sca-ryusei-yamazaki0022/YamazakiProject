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
    int TrapC=3;
    
    // Start is called before the first frame update
    void Start()
    {
        // 0以上の整数がPointの数だけ並んだ配列
        int[] array1 = Enumerable.Range(0, transform.childCount).ToArray();
        // array1をシャッフルする
        int[] array2 = array1.OrderBy(i => Guid.NewGuid()).ToArray();


        // 配置するアイテムの数
        int count = 10;
        // アイテム配置
        for (int n = 0; n < count; n++)
        {
            
            //Debug.Log(transform.GetChild(array2[n]).gameObject.tag);
            tag = transform.GetChild(array2[n]).gameObject.tag;
            //SquareCorners = transform.GetChild(array2[n]).gameObject;
            //SquareCorners.transform.Rotate(new Vector3(0, 180, 0));
            //Debug.Log(SquareCorners.transform.Rotate);
            if (WeponC>n)
            {
                //SquareCorners=Wepon;
                Rotation();
                Instantiate(Wepon, transform.GetChild(array2[n]).position, Wepon.transform.rotation);
                //Wepon.transform.Rotate(new Vector3(0, 180, 0));
            }
            else if(TrapC >= n)
            {
                //SquareCorners = Trap;
                Rotation();
                Instantiate(Trap, transform.GetChild(array2[n]).position, Trap.transform.rotation);
                //Trap.transform.Rotate(new Vector3(0, 180, 0));
                //Debug.Log("T");
            }
            else
            {
                //SquareCorners = Light;
                Rotation();
                Instantiate(Light, transform.GetChild(array2[n]).position, Light.transform.rotation);
                //Light.transform.Rotate(new Vector3(0, 180, 0));
                //Debug.Log("L");
            }
        }
        // 破壊
        Destroy(gameObject);
        
    }
    void Rotation()
    {
       switch(tag)
        {
            case "Up":
                //SquareCorners.transform.Rotate(new Vector3(0, 270, 0));
                Debug.Log("上だよー");
                break;
            case "Down":
                //SquareCorners.transform.Rotate(new Vector3(0, 90, 0));
                Debug.Log("sitaだよー");
                break;
            case "Left":
                //SquareCorners.transform.Rotate(new Vector3(0, 180, 0));
                Debug.Log("hidariだよー");
                break;
            case "Right":
                //SquareCorners.transform.Rotate(new Vector3(0, 0, 0));
                Debug.Log("migiだよー");
                break;

        }
    }
}