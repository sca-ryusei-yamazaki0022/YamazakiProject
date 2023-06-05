using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public Camera cam;
    public GameObject target;

    private RectTransform rect;
    //private GameObject textObject;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //textObject = transform.Find("Text")?.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null || target == null || rect == null)
        {
            return;
        }

        rect.position = cam.WorldToScreenPoint(target.transform.position) + new Vector3(0.0f, 10.0f, 0.0f);

        var vp = cam.WorldToViewportPoint(target.transform.position);
        bool active = vp.x >= 0.0f && vp.x <= 1.0f && vp.y >= 0.0f && vp.y <= 1.0f && vp.z >= 0.0f;
        Debug.Log("ŽÊ‚Á‚Ä");
        //textObject.SetActive(active);
    }
}
