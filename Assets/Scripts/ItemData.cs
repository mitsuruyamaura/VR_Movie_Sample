using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;              // ���[�r�[�̔ԍ��ƕR�Â���
    public Sprite itemSprite;       // �A�C�e���̉摜

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="name"></param>
    /// <param name="no"></param>
    public ItemData(string name, int no, Sprite sprite) {
        itemName = name;
        itemNo = no;
        itemSprite = sprite;
    }
}
