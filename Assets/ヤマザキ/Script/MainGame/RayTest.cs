using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private float rayDistance;
    private RaycastHit hit;
    private string tagname;
    private GameObject child;
    private GameManager gameManager;
    private bool weapon;
    private GameObject previousHitObject; // 前回の当たり判定で使用したオブジェクトを保持する変数
    private bool Mirror;
    private bool Map;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rayDistance = 5.0f;
        previousHitObject = null;
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private void CastRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {


            //GameObject currentHitObject = hit.collider.gameObject;
            GameObject currentHitObject = hit.collider.gameObject;
            tagname = hit.collider.gameObject.tag;

            if (currentHitObject != previousHitObject)
            {
                if (previousHitObject != null && previousHitObject.layer != 8) // Layerが8でない場合にのみLayerを変更する
                {
                    previousHitObject.layer = 0;
                }

                previousHitObject = currentHitObject;

                if (previousHitObject.layer != 8) // Layerが8でない場合にのみLayerを変更する
                {
                    previousHitObject.layer = 3;
                }
            }

            switch (tagname)
            {
                case "Light":
                    HandleLightObject();
                    break;

                case "Chest":
                    Debug.Log("tyesutoda");
                    HandleChestObject();
                    break;

                case "Weapon":
                    HandleWeaponObject();
                    break;

                case "Mirror1":
                case "Mirror2":
                case "Mirror3":
                    HandleMirrorObject();
                    break;
                case "Mirror":
                    if (Input.GetKey(KeyCode.E))
                    {
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        mirror=true;
                    }
                    break;

                case "Map":
                    if (Input.GetKey(KeyCode.E))
                    {
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        map=true;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (previousHitObject != null && previousHitObject.layer != 8) // Layerが8でない場合にのみLayerを変更する
            {
                previousHitObject.layer = 0;
            }
            previousHitObject = null;
        }
    }

    private void HandleLightObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit.collider.gameObject.tag = "LightOn";
            hit.collider.gameObject.layer = 0;
        }
    }

    private void HandleChestObject()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("yobareteru");
            child = hit.collider.gameObject.transform.GetChild(0).gameObject;
            child.SetActive(true);
            hit.collider.gameObject.layer = 0;
            //Ecount++;
        }
    }

    private void HandleWeaponObject()
    {
        if (Input.GetKey(KeyCode.E))
        {
            weapon = true;
            hit.collider.gameObject.SetActive(false);
        }
    }

    private void HandleMirrorObject()
    {
        if (weapon && Input.GetKey(KeyCode.E))
        {
            gameManager.MirrorUi = true;
            gameManager.Pstop = true;
            gameManager.HitTag = hit.collider.gameObject.tag;

            switch (tagname)
            {
                case "Mirror1":
                    gameManager.MirrorT1 += Time.deltaTime; TimeCount();
                    break;
                case "Mirror2":
                    gameManager.MirrorT2 += Time.deltaTime; TimeCount1();
                    break;
                case "Mirror3":
                    gameManager.MirrorT3 += Time.deltaTime; TimeCount2();
                    break;
            }

            
        }
        else
        {
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
    }

    private void TimeCount()
    {
        if (gameManager.MirrorT1 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
       
       
    }
    private void TimeCount1()
    {
        if (gameManager.MirrorT2 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
    }
    private void TimeCount2()
    {
        if (gameManager.MirrorT3 >= 10)
        {
            gameManager.MBreak += 1;
            gameManager.Clear();
            hit.collider.gameObject.SetActive(false);
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
    }

    public bool mirror
    {
        get { return Mirror; }
        set { Mirror = value; }
    }
    public bool map
    {
        get { return Map; }
        set { Map = value; }
    }
}