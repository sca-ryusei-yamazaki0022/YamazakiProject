using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    
    private Light lt;
    string tag;
    
    // Start is called before the first frame update
    void Start()
    {
        lt = gameObject.GetComponent<Light>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //tag=this.gameObject.tag;
        if (this.gameObject.tag == "LightOn")
            { 
            this.lt.range = 10f;
        }
        
    }

    //プロパティー
    
}
