 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private float rayDistance;
    //public LightTest lightTest;
    private bool light;
    private bool chest;
    private bool weapon;
    private int Ecount=0;
    private int mirrorcount=0;
    private float MirrorBreakTiem1=0;
    private float MirrorBreakTiem2 = 0;
    private float MirrorBreakTiem3 = 0;
    private string Tag;
    private bool Mirrorcheck;
    //public WeaponController Weaponcontroller;
    GameObject child;

    void Start()
    {
        rayDistance = 10.0f;//RAYの長さ
    }

    void Update()
    {
        //メインカメラからRAYを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //RAYの可視化、後で消す（デバック用）
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たったオブジェクトの名前表示
            //Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.CompareTag("Light"))
            {
                if(Input.GetMouseButtonDown(0))
                { 
                    light =true;
                    //Debug.Log(lCheck);
                }

            }
            if(hit.collider.gameObject.CompareTag("Chest"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(Ecount<1)
                    { 
                    chest = true;
                    child = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    child.SetActive(true);
                    Ecount++;
                    }

                }
            }
            if (hit.collider.gameObject.CompareTag("Weapon"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    weapon=true;
                    hit.collider.gameObject.SetActive(false);
                    //Debug.Log(weapon);
                }
            }
            
            if (hit.collider.gameObject.CompareTag("Mirror1")|| hit.collider.gameObject.CompareTag("Mirror2") || hit.collider.gameObject.CompareTag("Mirror3"))
            {
                if (Input.GetKey(KeyCode.E)&&weapon==true)
                {
                    switch (hit.collider.gameObject.tag)
                    {
                        case "Mirror1":
                            Tag= hit.collider.gameObject.tag;
                            Mirrorcheck = true;
                            MirrorBreakTiem1 += Time.deltaTime;
                            //Debug.Log(MirrorBreakTiem1);
                            if(MirrorBreakTiem1>=10)
                            {
                                hit.collider.gameObject.SetActive(false);
                                
                                
                                mirrorcount++;
                            }
                            break;
                        case "Mirror2":
                            Tag = hit.collider.gameObject.tag;
                            Mirrorcheck = true;
                            MirrorBreakTiem2 += Time.deltaTime;
                            if (MirrorBreakTiem2 >= 10)
                            {
                                hit.collider.gameObject.SetActive(false);
                                
                                
                                mirrorcount++;
                            }
                            break;
                        case "Mirror3":
                            Tag = hit.collider.gameObject.tag;
                            Mirrorcheck = true;
                            MirrorBreakTiem3 += Time.deltaTime;
                            if (MirrorBreakTiem3 >= 10)
                            {
                                hit.collider.gameObject.SetActive(false);
                                
                                mirrorcount++;
                            }
                            break;
                            
                    }

                    
                   
                    
                }
                
                //Mirrorcheck = false;
            }

            else
            {
                Mirrorcheck = false;
            }

        }



    }

    public bool LightCheck
    {
        get { return this.light; }  //取得用
        private set { this.light = value; } //値入力用
    }
    
    public bool Chest
    {
        get { return this.chest;}
        private set { this.chest = value; }
    }

    public int Mirror
    {
        get { return this.mirrorcount; }
        private set { this.mirrorcount = value; }
    }

    public float Mirror1
    {
        get { return this.MirrorBreakTiem1; }
        private set { this.MirrorBreakTiem1 = value; }
    }
    public float Mirror2
    {
        get { return this.MirrorBreakTiem2; }
        private set { this.MirrorBreakTiem2 = value; }
    }
    public float Mirror3
    {
        get { return this.MirrorBreakTiem3; }
        private set { this.MirrorBreakTiem3 = value; }
    }
    public bool MirrorCheck
    {
        get { return this.Mirrorcheck; }
        private set { this.Mirrorcheck = value; }
    }
    public string TagMirror
    {
        get { return this.Tag; }
        private set { this.Tag = value; }
    }
    /*
    public bool Weapon
    {
        get { return this.weapon; }  //取得用
        private set { this.weapon = value; } //値入力用
    }
    */
}
