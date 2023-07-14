using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        // もし衝突した相手オブジェクトの名前が"Cube"ならば
        if (other.gameObject.name == "Player")
        {           
            SceneManager.LoadScene("EpilogueScene");
        }
    }
}
