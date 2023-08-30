using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    
    private Light lt;
    string tag;
    int Oncount;
    // Start is called before the first frame update
    void Start()
    {
        lt = gameObject.GetComponent<Light>();

    }
    // Update is called once per frame
   
     void FixedUpdate()
    {
        //tag=this.gameObject.tag;
        if (this.gameObject.tag == "LightOn" && Oncount == 0)
        {
            this.lt.range = 15f;
            this.gameObject.layer = 8;
            Oncount++;
        }

    }

    //プロパティー

}
