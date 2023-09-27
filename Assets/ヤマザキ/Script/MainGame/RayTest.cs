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
    private GameObject previousHitObject; // �O��̓����蔻��Ŏg�p�����I�u�W�F�N�g��ێ�����ϐ�
    private bool Mirror;
    private bool Map;
    private GameObject ChestBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject Text;
    
    [SerializeField] private GameObject Text2;
    [SerializeField] private GameObject Text3;
    [SerializeField] private GameObject Text4;
    [SerializeField] private GameObject EUseText;
    [SerializeField] private GameObject S;
    //[SerializeField] private GameObject TextC;
    private  Animator anim;
    private Animator Panime;
    private Animator Panime2;
    private Animator Panime3;
    private Animator Panime4;
    [SerializeField] private AudioClip MirrorBreak;//���������
    [SerializeField] private AudioClip Item;//�A�C�e���E��
    //[SerializeField] private AudioClip ItemUse;//�g��
    [SerializeField] private AudioClip Light;//�}�b�`
    bool OneCount=true;
    bool LightOneCount=true;
    bool D;
    [SerializeField] private GameObject A;
    //[SerializeField] private GameObject B;
    //[SerializeField] private GameObject C;
    //[SerializeField] private GameObject DD;
    //[SerializeField] private GameObject E;
    bool DoorOutLine;
    GameObject DoorOut;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        
        rayDistance = 0.1f;
        previousHitObject = null;
        Panime=GameObject.Find("P��pCanvas").GetComponent<Animator>();
        Panime2 = GameObject.Find("TextPlayer").GetComponent<Animator>();
        Panime3 = GameObject.Find("���}���u").GetComponent<Animator>();
        Panime4 = GameObject.Find("Canvas").GetComponent<Animator>();
        Debug.Log(Panime2);
        A.SetActive(true);
        //Panime.SetBool("Move",true);
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
        //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, 7))
        {


            //GameObject currentHitObject = hit.collider.gameObject;
            GameObject currentHitObject = hit.collider.gameObject;
            tagname = hit.collider.gameObject.tag;

            if (currentHitObject != previousHitObject)
            {
                OneCount = true; Panime.SetBool("Item", false);
                LightOneCount=true;Panime.SetBool("Match",false);
                
                if(DoorOutLine)
                {
                    DoorOut.layer=9;
                    DoorOutLine = false;
                }

                if (previousHitObject != null && previousHitObject.layer != 8) // Layer��8�łȂ��ꍇ�ɂ̂�Layer��ύX����
                {
                    //Debug.Log(previousHitObject.layer);
                    previousHitObject.layer = 0;
                }
                previousHitObject = currentHitObject;
                if (previousHitObject != null && previousHitObject.layer == 9 && !DoorOutLine)
                {
                    DoorOutLine = true;
                    Debug.Log("Door");
                    DoorOut=previousHitObject;
                }
                

                if (previousHitObject.layer != 8) // Layer��8�łȂ��ꍇ�ɂ̂�Layer��ύX����
                {
                    previousHitObject.layer = 3;
                    Debug.Log("OK");
                }
                else
                {
                    // Ray�����ɂ�������Ȃ��ꍇ�ApreviousHitObject��null�ɐݒ肵�܂��B
                    previousHitObject = null;
                }
            }

            switch (tagname)
            {
                case "Door":
                    Door();
                    break;
                case "OneSideDoor":
                    OneSide();
                    break;
                case "Light":
                    if (LightOneCount)
                    {
                        Panime.SetBool("Match", true);
                        LightOneCount = false;
                    }
                    HandleLightObject();
                    break;

                case "Chest":
                    if (OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount = false;
                    }

                    HandleChestObject();
                    break;

                case "Weapon":
                    if (OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount = false;
                    }

                    HandleWeaponObject();
                    break;

                case "Match":
                    if (OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount = false;
                    }

                    HandleMatchObject();
                    break;
                
                case "Crystal":
                    if (OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount = false;
                    }

                    HandleFlashItemObject();
                    break;
                case "Mirror1":
                case "Mirror2":
                case "Mirror3":
                    HandleMirrorObject();
                    break;
                case "Mirror":
                    if (OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount = false;
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        audioSource.PlayOneShot(Item);
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        mirror = true;
                        Panime.SetBool("Mirror", true);
                    }
                    break;

                case "Map":
                    if(OneCount)
                    {
                        Panime.SetBool("Item", true);
                        OneCount=false;
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        audioSource.PlayOneShot(Item);
                        hit.collider.gameObject.SetActive(false);
                        hit.collider.gameObject.layer = 0;
                        map = true;
                    }
                    break;
                case "Text":
                case "����2":
                case "����3":
                case "����4":
                    if (Input.GetKey(KeyCode.E))
                    {
                        
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
            if (previousHitObject != null && previousHitObject.layer != 8) // Layer��8�łȂ��ꍇ�ɂ̂�Layer��ύX����
            {
                previousHitObject.layer = 0;
            }
            previousHitObject = null;
        }
    }

    private void Door()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("�h�A�̃X�N���v�g");
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
    private void OneSide()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("�h�A�̃X�N���v�g");
            var Animetor1 = hit.collider.transform.parent.gameObject.GetComponent<Animator>();
            anim = hit.collider.transform.parent.gameObject.GetComponent<Animator>();
            D = Animetor1.GetBool("Door");
            if (D)
            {
                anim.SetBool("Door", false);
            }
            else
            {
                anim.SetBool("Door", true);
            }
            //Debug.Log(anim);
        }
    }
    private void HandleLightObject()
    {
        if (Input.GetKey(KeyCode.E) && gameManager.NowMatchCount != 0)
        {
            hit.collider.gameObject.tag = "LightOn";
            audioSource.PlayOneShot(Light);
            gameManager.NowMatchCount -= 1;
            hit.collider.gameObject.layer = 0;
            //Debug.Log(gameManager.NowMatchCount);
        }
        else if (Input.GetMouseButtonDown(0) && gameManager.NowMatchCount == 0)
        {
            //Debug.Log("�}�b�`���Ȃ��悤��");
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
        if (Input.GetKey(KeyCode.E))
        {
            
            if (gameManager.NowMatchCount < 5)
            {
                audioSource.PlayOneShot(Item);
                gameManager.NowMatchCount += 2;
                hit.collider.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("����ȏ㎝�ĂȂ�");
            }
            //else { //Debug.Log("5�����Ă����");}
            
            //Debug.Log(gameManager.NowMatchCount);
        }
    }

    private void HandleFlashItemObject()
    {
        
        if (Input.GetKey(KeyCode.E))
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
            //Debug.Log("��ꂽ�[");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
            
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;

            Panime4.SetTrigger("Count");
        }


    }
    private void TimeCount1()
    {
        if (gameManager.MirrorT2 >= 10)
        {
            //Debug.Log("��ꂽ�[");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
           
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;

            Panime4.SetTrigger("Count");
        }
    }
    private void TimeCount2()
    {
        if (gameManager.MirrorT3 >= 10)
        {

            //Debug.Log("��ꂽ�[");
            audioSource.PlayOneShot(MirrorBreak);
            gameManager.MBreak += 1;
            gameManager.Clear();
            GameObject obj = hit.collider.gameObject.transform.root.gameObject;
            obj.SetActive(false);
            
            gameManager.MirrorUi = false;
            gameManager.Pstop = false;

            Panime4.SetTrigger("Count");
        }
    }

    private void HandleTextObject()
    {
        switch(hit.collider.gameObject.tag)
        {
            case "Text":
                Text.SetActive(true);
                //EUseText.SetActive(true);
                Panime.SetBool("Move", false); Panime.SetBool("Item", false); Panime.SetBool("Mirror", false); Panime.SetBool("Match", false);
                Panime2.SetBool("Text", true);
                Panime3.SetBool("EText",true); Panime3.SetBool("ETextDown", false);
                
                hit.collider.gameObject.SetActive(false);
                //TextC.SetActive(false);
                gameManager.Pstop = true; S.SetActive(false);
                break;
            case "����2":
                Text2.SetActive(true);
                //EUseText.SetActive(true);
                Panime2.SetBool("Text2", true);
                Panime3.SetBool("EText", true); Panime3.SetBool("ETextDown", false);
                Panime.SetBool("Move", false); Panime.SetBool("Item", false); Panime.SetBool("Mirror", false); Panime.SetBool("Match", false);
                hit.collider.gameObject.SetActive(false);
                //TextC.SetActive(false);
                gameManager.Pstop = true; S.SetActive(false);
                break;
            case "����3":
                Text3.SetActive(true);
                //Debug.Log("HAitteru");
                EUseText.SetActive(true);
                Panime2.SetBool("Text3", true);
                Panime3.SetBool("EText", true); Panime3.SetBool("ETextDown", false);
                Panime.SetBool("Move", false); Panime.SetBool("Item", false); Panime.SetBool("Mirror", false); Panime.SetBool("Match", false);
                hit.collider.gameObject.SetActive(false);
                //TextC.SetActive(false);
                gameManager.Pstop = true; S.SetActive(false);
                break;
            case "����4":
                Text4.SetActive(true);
                //EUseText.SetActive(true);
                Panime2.SetBool("Text4", true);
                Panime3.SetBool("EText", true); Panime3.SetBool("ETextDown", false);
                Panime.SetBool("Move", false); Panime.SetBool("Item", false); Panime.SetBool("Mirror", false); Panime.SetBool("Match", false);
                hit.collider.gameObject.SetActive(false);
                //TextC.SetActive(false);
                gameManager.Pstop = true; S.SetActive(false);
                break;
        }
        

    }

   

    private IEnumerator AA()
    {
        
        yield return new WaitForSeconds(1.5f);
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