using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    //�Q�[���I��:�{�^������Ăяo��
     void EndGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        #else
        Application.Quit();//�Q�[���v���C�I��
        #endif
    }
}
