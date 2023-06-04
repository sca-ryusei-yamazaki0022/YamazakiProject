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
        // �I�u�W�F�N�g���J�����Ɏʂ����Ƃ��̏����������ɋL�q���܂�
        Debug.Log("Object became visible!");
    }

    private void OnBecameInvisible()
    {
        // �I�u�W�F�N�g���J�������猩���Ȃ��Ȃ����Ƃ��̏����������ɋL�q���܂�
        Debug.Log("Object became invisible!");
    }

    private void Update()
    {
        // �I�u�W�F�N�g����ʊO�ɏo�����ǂ����̔�����s���܂�
        if (!objectRenderer.isVisible)
        {
            // �I�u�W�F�N�g����ʊO�ɏo���Ƃ��̏����������ɋL�q���܂�
            Debug.Log("Object went off-screen!");
        }
    }
}
