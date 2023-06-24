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

    RaycastHit hit;

    //public WeaponController Weaponcontroller;
    GameObject child;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rayDistance = 5.0f;//RAY�̒���
    }

    void FixedUpdate()
    {
        Ray(); 
    }

    void Ray()
    {
        //���C���J��������RAY���΂�
        Ray ray = new Ray(transform.position, transform.forward);
        
        //RAY�̉����A��ŏ����i�f�o�b�N�p�j
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Tagname = hit.collider.gameObject.tag;
            
            switch (Tagname)
            {
                
                case "Light":
                    hit.collider.gameObject.layer=3;
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.gameObject.tag = "LightOn";
                        hit.collider.gameObject.layer = 0;
                    }
                    break;

                case "Chest":
                    hit.collider.gameObject.layer = 3;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (Ecount < 1)
                        {
                            //chest = true;
                            child = hit.collider.gameObject.transform.GetChild(0).gameObject;
                            child.SetActive(true); hit.collider.gameObject.layer = 0;
                            Ecount++;
                        }
                    }
                    break;
                case "Weapon":
                    hit.collider.gameObject.layer = 3;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        weapon = true;
                        hit.collider.gameObject.SetActive(false);
                    }
                    break;
                case "Mirror1":
                    hit.collider.gameObject.layer = 3;
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT1 += Time.deltaTime;
                            TimeCount();
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
                case"Mirror2":
                    hit.collider.gameObject.layer = 3;
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT2 += Time.deltaTime;
                            TimeCount();
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
                case "Mirror3":
                    hit.collider.gameObject.layer = 3;
                    if (weapon == true)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            gameManager.MirrorUi = true; gameManager.Pstop = true;
                            gameManager.HitTag = hit.collider.gameObject.tag;
                            gameManager.MirrorT3 += Time.deltaTime;
                            TimeCount();
                        }
                        else
                        {
                            gameManager.MirrorUi = false; gameManager.Pstop = false;
                        }
                    }
                    break;
                default:         //���̑��̏ꍇ
                    //hit.collider.gameObject.layer = 0;
                    break;
            }
            //gameManager.MirrorUi = false; gameManager.Pstop = false;
        }
        //else
        //{
            //hit.collider.gameObject.layer = 0;
        //}
    }    
    void TimeCount()
    {
        if (gameManager.MirrorT1 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false; gameManager.Pstop = false;
        }
        if (gameManager.MirrorT2 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false; gameManager.Pstop = false;
        }
        if (gameManager.MirrorT3 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false; gameManager.Pstop = false;
        }
    }
}
