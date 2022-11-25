[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;      // ムービーの番号と紐づける

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name"></param>
    /// <param name="no"></param>
    public ItemData(string name, int no) {
        itemName = name;
        itemNo = no;
    }
}
