using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArea : MonoBehaviour
{
    private ObjectInfoView objectInfoView;


    private void Start() {
        if (transform.parent.gameObject.TryGetComponent(out objectInfoView)) {
            Debug.Log("InfoArea が ObjectInfoView を取得しました。");           
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("侵入してきた : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log(player);
            objectInfoView.ShowItemName();
        }
    }


    private void OnTriggerStay(Collider other) {
        //Debug.Log("侵入中 : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);
            objectInfoView.LookPlayer();
        }
    }


    private void OnTriggerExit(Collider other) {
        //Debug.Log("いなくなった : " + other.name);
        if (other.TryGetComponent(out PlayerController player)) {
            //Debug.Log(player);

            objectInfoView.HideItemName();
        }
    }
}
