using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailModel : MonoBehaviour
{
    // Presenter ���쐬��́A���̕����̖�����n��
    private ObjectInfoView objectInfoView;

    private ItemData itemData;

    public int setItemNo;


    private void Start() {
        //itemData = new ItemData("aaa", 0);

        itemData = DataBaseManager.instance.GetItemData(setItemNo);

        if (transform.parent.gameObject.TryGetComponent(out objectInfoView)) {
            Debug.Log("ItemDetail �� ObjectInfoView ���擾���܂����B");
            objectInfoView.SetUpObjectInfoDetail(itemData.itemName);          
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerController player)) {
            Debug.Log("���[�r�[�Đ�");

            VideoClipManager.instance.PrepareVideoClip(itemData.itemNo);

            Debug.Log("�l�������A�C�e���ꗗ�ɒǉ�");

            Destroy(transform.parent.gameObject);
        }
    }
}
