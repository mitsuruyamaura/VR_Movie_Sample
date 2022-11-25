using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ObjectInfoView objectInfoView;


    private void Start() {
        if (transform.parent.gameObject.TryGetComponent(out objectInfoView)) {
            Debug.Log("InfoArea �� ObjectInfoView ���擾���܂����B");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("�N�����Ă��� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            objectInfoView.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        //Debug.Log("�N���� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);
            objectInfoView.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        //Debug.Log("���Ȃ��Ȃ��� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);

            objectInfoView.HideItemName();
        }
    }
}
