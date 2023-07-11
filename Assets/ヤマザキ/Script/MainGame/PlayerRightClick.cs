using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRightClick : MonoBehaviour
{
    [SerializeField] GameObject Mirror;
    [SerializeField] Light light;
    [SerializeField] GameObject Map;
    int mouseDownCount=0;
    RayTest rayTest;
    //int Position = 5;
    // Start is called before the first frame update
    void Start()
    {
        Mirror.SetActive(false);
        Map.SetActive(false);
        //light.gameObject.GetComponent<Light>();
        light.range=0.0f;
        rayTest= GameObject.FindWithTag("Player").GetComponent<RayTest>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mirror.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+Position);
        if (Input.GetMouseButtonDown(1))
        {
            if (rayTest.mirror && rayTest.map)
            {
                Mirror.SetActive(true); light.range = 5.0f; Map.SetActive(true);
            }
            else if (rayTest.mirror)
            {
                Mirror.SetActive(true); light.range = 5.0f; Map.SetActive(false);
            }
            else if (rayTest.map)
            {
                Mirror.SetActive(false); light.range = 5.0f; Map.SetActive(true);
            }
           
            else
            {
                Mirror.SetActive(false); light.range = 0.0f; mouseDownCount = 0; Map.SetActive(false);
            }

        }
    }
}
