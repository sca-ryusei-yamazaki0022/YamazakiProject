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
    private GameObject ChestBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject Text;
    private  Animator anim;
    private Animator Panime;
    [SerializeField] private AudioClip MirrorBreak;//鏡が割れる
    [SerializeField] private AudioClip Item;//アイテム拾う
    //[SerializeField] private AudioClip ItemUse;//使う
    [SerializeField] private AudioClip Light;//マッチ


    bool D;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;
    [SerializeField] private GameObject C;
    [SerializeField] private GameObject DD;
    [SerializeField] private GameObject E;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        rayDistance = 5.0f;
        previousHitObject = null;
        Panime=GameObject.Find("P専用Canvas").GetComponent<Animator>();
        A.SetActive(true);
        Panime.SetBool("Move",true);
        StartCoroutine(AA());
    }

    private void FixedUpdate()
    {
        CastRay();
        //Debug.Log(gameManager.NowFlashCount);
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
                case "Door":
                    Door();
                    break;

                case "Light":
                    
                    HandleLightObject();
                    break;

                case "Chest":

                    HandleChestObject();
                    break;

                case "Weapon":
                    
                    HandleWeaponObject();
                    break;

                case "Match":
                    
                    HandleMatchObject();
                    break;
                
                case "Crystal":
                    
                    HandleFlashItemObject();
                    break;
                case "Mirror1":
                case "Mirror2":
                case "Mirror3":
                    HandleMirrorObject();
                    break;
                case "Mirror":
                    if (Input.GetKey(KeyCode.E))
                    {
                        audioSource.PlayOneShot(Item);
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        mirror = true;
                    }
                    break;

                case "Map":
                    if (Input.GetKey(KeyCode.E))
                    {
                        audioSource.PlayOneShot(Item);
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        map = true;
                    }
                    break;
                case "Text":
                    if (Input.GetKey(KeyCode.E))
                    {
                        hit.collider.gameObject.SetActive(false);
                        //hit.collider.gameObject.SetActive(false);
                        HandleTextObject();
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

    private void Door()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ドアのスクリプト");
            var Animetor1= hit.collider.transform.parent.parent.gameObject.GetComponent<Animator>();
            anim = hit.collider.transform.parent.parent.gameObject.GetComponent<Animator>();
            D = Animetor1.GetBool("Door");
            if(D)
            {
                anim.SetBool("Door",false);
            }
            else
            {
                anim.SetBool("Door",true);
            }
            //Debug.Log(anim);
        }
    }
    private void HandleLightObject()
    {
        if (Input.GetMouseButtonDown(0) && gameManager.NowMatchCount != 0)
        {
            hit.collider.gameObject.tag = "LightOn";
            audioSource.PlayOneShot(Light);
            gameManager.NowMatchCount -= 1;
            hit.collider.gameObject.layer = 0;
            Debug.Log(gameManager.NowMatchCount);
        }
        else if (Input.GetMouseButtonDown(0) && gameManager.NowMatchCount == 0)
        {
            //Debug.Log("マッチがないようだ");
        }
    }

    private void HandleChestObject()
    {
        if (Input.GetKey(KeyCode.E))
        {

            //Debug.Log("yobareteru");
            child = hit.collider.gameObject.transform.GetChild(0).gameObject;
            hit.collider.gameObject.layer = 8;

            child.SetActive(true);
            hit.collider.gameObject.tag="Untagged";

        }
    }

    private void HandleWeaponObject()
    {
        if (Input.GetKey(KeyCode.E))
        {
            audioSource.PlayOneShot(Item);
            weapon = true;
            hit.collider.gameObject.SetActive(false);
        }
    }

    private void HandleMatchObject()
    {
        //Debug.Log("tyesutoda");
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(Item);
            if (gameManager.NowMatchCount > 5)
            {
                gameManager.NowMatchCount += 2;
            }
            //else { //Debug.Log("5個持っているよ");}
            hit.collider.gameObject.SetActive(false);
            //Debug.Log(gameManager.NowMatchCount);
        }
    }

    private void HandleFlashItemObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(Item);
            gameManager.NowFlashCount += 1;
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
            //Debug.Log("壊れたー");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
            
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }


    }
    private void TimeCount1()
    {
        if (gameManager.MirrorT2 >= 10)
        {
            //Debug.Log("壊れたー");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
           
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
    }
    private void TimeCount2()
    {
        if (gameManager.MirrorT3 >= 10)
        {
            //Debug.Log("壊れたー");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
            
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;
        }
    }

    private void HandleTextObject()
    {
        Text.SetActive(true);
        Panime.SetBool("Text", true);

    }
    private IEnumerator AA()
    {
        
        yield return new WaitForSeconds(4);
        Panime.SetBool("Move", false);
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