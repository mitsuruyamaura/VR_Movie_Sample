using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public List<ItemData> itemDataList = new();

    public bool isDebugItemAdd;     // デバッグモード。true の際にはデバッグモードオンとして機能させる

    private const string ITEM_DATA = "ItemData_";

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }  
    }

    void Start() {
        // デバッグモードの場合(Awake メソッドでやらないこと)
        if (isDebugItemAdd) {
            // データベースにあるアイテムの情報をすべて獲得したアイテムのリストに追加
            for (int i = 0; i < DataBaseManager.instance.itemDataSO.itemDataList.Count; i++) {
                AddItemDataList(DataBaseManager.instance.itemDataSO.itemDataList[i]);
            }
        }
        
        LoadItemDatas();
    }

    /// <summary>
    /// 獲得したアイテムの情報をリストに追加
    /// </summary>
    /// <param name="itemData"></param>
    public void AddItemDataList(ItemData itemData) {
        itemDataList.Add(itemData);
    }

    /// <summary>
    /// 獲得したアイテムデータのセーブ(１つずつ)
    /// </summary>
    /// <param name="itemNo"></param>
    public void SaveItemData(int itemNo) {
        PlayerPrefs.SetInt(ITEM_DATA + itemNo, itemNo);
        PlayerPrefs.Save();
        
        Debug.Log("セーブ完了 : " + ITEM_DATA + itemNo);
    }

    /// <summary>
    /// 前回までに獲得しているアイテムデータのロード(獲得しているすべてのデータ)
    /// </summary>
    private void LoadItemDatas() {

        for (int i = 0; i < DataBaseManager.instance.itemDataSO.itemDataList.Count; i++) {
            if (PlayerPrefs.HasKey(ITEM_DATA + i)) {
                AddItemDataList(DataBaseManager.instance.GetItemData(i));
                
                Debug.Log("セーブされていたアイテムデータをリストに追加 ItemNo : " + DataBaseManager.instance.GetItemData(i).itemNo);
            }
        }
        
        Debug.Log("ロード完了");
    }
}
