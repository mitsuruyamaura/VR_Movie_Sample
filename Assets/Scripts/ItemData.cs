[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;      // ���[�r�[�̔ԍ��ƕR�Â���

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="name"></param>
    /// <param name="no"></param>
    public ItemData(string name, int no) {
        itemName = name;
        itemNo = no;
    }
}
