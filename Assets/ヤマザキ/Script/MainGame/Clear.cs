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
        this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // もし衝突した相手オブジェクトの名前が"Cube"ならば
        if (collision.gameObject.name == "Player")
        {           
            SceneManager.LoadScene("EpilogueScene");
        }
    }
}
