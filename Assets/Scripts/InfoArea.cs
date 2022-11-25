using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ItemDetail itemDetail;


    private void Start() {
        if (TryGetComponent(out itemDetail)) {
            Debug.Log("ItemDetail 取得しました。");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("侵入してきた : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("侵入中 : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        Debug.Log("いなくなった : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.HideItemName();
        }
    }
}
