using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public List<ItemData> itemDataList = new();

    public bool isDebugItemAdd;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }  
    }

    void Start() {
        if (isDebugItemAdd) {
            for (int i = 0; i < DataBaseManager.instance.itemDataSO.itemDataList.Count; i++) {
                AddItemDataList(DataBaseManager.instance.itemDataSO.itemDataList[i]);
            }
        }
    }


    private void AddItemDataList(ItemData itemData) {
        itemDataList.Add(itemData);
    }

}
