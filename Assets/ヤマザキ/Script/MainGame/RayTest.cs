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
    private string Tagname;
    //private int mirrorcount=0;
    //private float MirrorBreakTiem1=0;
    //private float MirrorBreakTiem2 = 0;
    //private float MirrorBreakTiem3 = 0;
    
    
    //public WeaponController Weaponcontroller;
    GameObject child;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rayDistance = 10.0f;//RAYの長さ
    }

    void Update()
    {
        Ray(); 
    }

    void Ray()
    {
        //メインカメラからRAYを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //RAYの可視化、後で消す（デバック用）
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Tagname = hit.collider.gameObject.tag;
            switch (Tagname)
            {
                case "Light":
                    if(Input.GetMouseButtonDown(0))
                    {
                        hit.collider.gameObject.tag = "LightOn";
                    }
                    break;

                case "Chest":
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        if (Ecount < 1)
                        {
                            //chest = true;
                            child = hit.collider.gameObject.transform.GetChild(0).gameObject;
                            child.SetActive(true);
                            Ecount++;
                        }
                    }
                    break;
                case "Weapon":
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        weapon = true;
                        hit.collider.gameObject.SetActive(false);
                    }
                    break;
                case "Mirror1":
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT1 += Time.deltaTime;
                            if (gameManager.MirrorT1 >= 10)
                            {
                                gameManager.MBreak += 1;
                                gameManager.Clear();
                                hit.collider.gameObject.SetActive(false);
                                gameManager.MirrorUi = false; gameManager.Pstop = false;
                            }
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
                case"Mirror2":
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT2 += Time.deltaTime;
                            if (gameManager.MirrorT2 >= 10)
                            {
                                gameManager.MBreak += 1;
                                gameManager.Clear();
                                hit.collider.gameObject.SetActive(false);
                                gameManager.MirrorUi = false; gameManager.Pstop = false;
                            }
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
                case "Mirror3":
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT3 += Time.deltaTime;
                            if (gameManager.MirrorT3 >= 10)
                            {
                                gameManager.MBreak += 1;
                                gameManager.Clear();
                                hit.collider.gameObject.SetActive(false);
                                gameManager.MirrorUi = false; gameManager.Pstop = false;
                            }
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
            }
            //gameManager.MirrorUi = false; gameManager.Pstop = false;
        }
    }    
}
