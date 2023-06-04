using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = Player.GetComponent<Renderer>();
    }

    private void OnBecameVisible()
    {
        // オブジェクトがカメラに写ったときの処理をここに記述します
        Debug.Log("Object became visible!");
    }

    private void OnBecameInvisible()
    {
        // オブジェクトがカメラから見えなくなったときの処理をここに記述します
        Debug.Log("Object became invisible!");
    }

    private void Update()
    {
        // オブジェクトが画面外に出たかどうかの判定を行います
        if (!objectRenderer.isVisible)
        {
            // オブジェクトが画面外に出たときの処理をここに記述します
            Debug.Log("Object went off-screen!");
        }
    }
}
