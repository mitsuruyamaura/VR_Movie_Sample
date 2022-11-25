using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ObjectInfoView objectInfoView;


    private void Start() {
        if (transform.parent.gameObject.TryGetComponent(out objectInfoView)) {
            Debug.Log("InfoArea ‚ª ObjectInfoView ‚ğæ“¾‚µ‚Ü‚µ‚½B");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("N“ü‚µ‚Ä‚«‚½ : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            objectInfoView.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        //Debug.Log("N“ü’† : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);
            objectInfoView.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        //Debug.Log("‚¢‚È‚­‚È‚Á‚½ : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);

            objectInfoView.HideItemName();
        }
    }
}
