using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ItemDetail itemDetail;


    private void Start() {
        if (TryGetComponent(out itemDetail)) {
            Debug.Log("ItemDetail æ“¾‚µ‚Ü‚µ‚½B");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("N“ü‚µ‚Ä‚«‚½ : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("N“ü’† : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        Debug.Log("‚¢‚È‚­‚È‚Á‚½ : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            itemDetail.HideItemName();
        }
    }
}
