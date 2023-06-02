using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    
    private Light lt;
    public RayTest rayTest;
    
    // Start is called before the first frame update
    void Start()
    {
        lt = gameObject.GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {
        bool ICheck;
        ICheck = rayTest.LightCheck;
        if (ICheck==true)
        {
            this.lt.range = 10f;
            
        }
    }

    //プロパティー
    
}
