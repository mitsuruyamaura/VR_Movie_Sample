using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public List<ItemData> itemDataList = new();

    public bool isDebugItemAdd;     // �f�o�b�O���[�h�Btrue �̍ۂɂ̓f�o�b�O���[�h�I���Ƃ��ċ@�\������

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
        // �f�o�b�O���[�h�̏ꍇ(Awake ���\�b�h�ł��Ȃ�����)
        if (isDebugItemAdd) {
            // �f�[�^�x�[�X�ɂ���A�C�e���̏������ׂĊl�������A�C�e���̃��X�g�ɒǉ�
            for (int i = 0; i < DataBaseManager.instance.itemDataSO.itemDataList.Count; i++) {
                AddItemDataList(DataBaseManager.instance.itemDataSO.itemDataList[i]);
            }
        }
        
        LoadItemDatas();
    }

    /// <summary>
    /// �l�������A�C�e���̏������X�g�ɒǉ�
    /// </summary>
    /// <param name="itemData"></param>
    public void AddItemDataList(ItemData itemData) {
        itemDataList.Add(itemData);
    }

    /// <summary>
    /// �l�������A�C�e���f�[�^�̃Z�[�u(�P����)
    /// </summary>
    /// <param name="itemNo"></param>
    public void SaveItemData(int itemNo) {
        PlayerPrefs.SetInt(ITEM_DATA + itemNo, itemNo);
        PlayerPrefs.Save();
        
        Debug.Log("�Z�[�u���� : " + ITEM_DATA + itemNo);
    }

    /// <summary>
    /// �O��܂łɊl�����Ă���A�C�e���f�[�^�̃��[�h(�l�����Ă��邷�ׂẴf�[�^)
    /// </summary>
    private void LoadItemDatas() {

        for (int i = 0; i < DataBaseManager.instance.itemDataSO.itemDataList.Count; i++) {
            if (PlayerPrefs.HasKey(ITEM_DATA + i)) {
                AddItemDataList(DataBaseManager.instance.GetItemData(i));
                
                Debug.Log("�Z�[�u����Ă����A�C�e���f�[�^�����X�g�ɒǉ� ItemNo : " + DataBaseManager.instance.GetItemData(i).itemNo);
            }
        }
        
        Debug.Log("���[�h����");
    }
}
