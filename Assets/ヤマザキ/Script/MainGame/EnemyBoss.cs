using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public GameObject test;

    private RectTransform rect;
    //private GameObject textObject;
    // Start is called before the first frame update

    //Vector3 direction = new Vector3(0, 0, -1);
    //Vector3 origwin = new Vector3(0, 3, 0);

    float distance = 100; // 飛ばす&表示するRayの長さ
    float duration = 3;   // 表示期間（秒）



    void Start()
    {
        rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
    }

    void FixedUpdate()
    {
        PLRay();
        PVCamera();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PLRay()
    {
        Ray ray = new Ray(test.transform.position, test.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
        }
    }
    void PVCamera()
    {
        if (cam == null || target == null || rect == null)
        {
            return;
        }
        rect.position = cam.WorldToScreenPoint(target.transform.position) + new Vector3(0.0f, 10.0f, 0.0f);
        var vp = cam.WorldToViewportPoint(target.transform.position);
        bool active = vp.x >= 0.0f && vp.x <= 1.0f && vp.y >= 0.0f && vp.y <= 1.0f && vp.z >= 0.0f;
        Debug.Log("写ってる");

    }
}
