using UnityEngine;
using System.Collections;

public class TEST : MonoBehaviour
{

    //���C���J�����ɕt���Ă���^�O��
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    //�J�����ɕ\������Ă��邩
    private bool _isRendered = false;

    private void Update() {

        if(_isRendered) {
            Debug.Log("�J�����ɉf���Ă��I");
        }

        _isRendered = false;
    }

    //�J�����ɉf���Ă�ԂɌĂ΂��
    private void OnWillRenderObject() {
        Debug.Log("Toooooo");
        //���C���J�����ɉf����������_isRendered��L����
        if(Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
            _isRendered = true;
        }
        
    }

}
