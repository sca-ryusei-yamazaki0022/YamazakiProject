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
            //Debug.Log("AAAAAAAAAAAAAAAAAA");
            if(WeponC>n)
            { 
                Instantiate(Wepon, transform.GetChild(array2[n]).position, Wepon.transform.rotation);
                //Debug.Log("W");
            }
            else if(TrapC >= n)
            {
                Instantiate(Trap, transform.GetChild(array2[n]).position, Trap.transform.rotation);
                //Debug.Log("T");
            }
            else
            {
                Instantiate(Light, transform.GetChild(array2[n]).position, Light.transform.rotation);
                //Debug.Log("L");
            }
        }
        // 破壊
        Destroy(gameObject);
        /*
        int n = 0;
        foreach (int i in ary2)
        {
            if (n < count)
            {
                Instantiate(item, transform.GetChild(i).position, item.transform.rotation);
            }
            Destroy(transform.GetChild(i).gameObject);
        }
        */
    }
}