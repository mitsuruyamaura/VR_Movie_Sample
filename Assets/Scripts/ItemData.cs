[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;      // ムービーの番号と紐づける

    public ItemData(string name, int no) {
        itemName = name;
        itemNo = no;
    }
}
