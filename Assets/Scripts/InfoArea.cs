using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ItemDetail itemDetail;


    private void Start() {
        if (TryGetComponent(out itemDetail)) {
            Debug.Log("ItemDetail �擾���܂����B");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("�N�����Ă��� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("�N���� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        Debug.Log("���Ȃ��Ȃ��� : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.HideItemName();
        }
    }
}
