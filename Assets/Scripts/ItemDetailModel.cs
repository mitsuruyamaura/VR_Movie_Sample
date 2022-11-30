using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailModel : MonoBehaviour
{
    // Presenter を作成後は、この部分の役割を渡す
    private ObjectInfoView objectInfoView;

    private ItemData itemData;

    public int setItemNo;


    private void Start() {
        //itemData = new ItemData("aaa", 0);

        itemData = DataBaseManager.instance.GetItemData(setItemNo);

        if (transform.parent.gameObject.TryGetComponent(out objectInfoView)) {
            Debug.Log("ItemDetail が ObjectInfoView を取得しました。");
            objectInfoView.SetUpObjectInfoDetail(itemData.itemName);          
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log("ムービー再生");

            VideoClipManager.instance.PrepareVideoClip(itemData.itemNo);

            Debug.Log("獲得したアイテム一覧に追加");

            Destroy(transform.parent.gameObject);
        }
    }
}
