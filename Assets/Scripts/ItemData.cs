[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;      // ���[�r�[�̔ԍ��ƕR�Â���

    public ItemData(string name, int no) {
        itemName = name;
        itemNo = no;
    }
}
