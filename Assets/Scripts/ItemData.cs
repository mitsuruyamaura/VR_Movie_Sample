using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int itemNo;              // ムービーの番号と紐づける
    public Sprite itemSprite;       // アイテムの画像

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name"></param>
    /// <param name="no"></param>
    public ItemData(string name, int no, Sprite sprite) {
        itemName = name;
        itemNo = no;
        itemSprite = sprite;
    }
}
