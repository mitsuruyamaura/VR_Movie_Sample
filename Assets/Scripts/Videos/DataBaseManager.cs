using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public VideoDataSO videoDataSO;
    public ItemDataSO itemDataSO;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �����̔ԍ��Ŏw�肵�� VideoData ���擾
    /// </summary>
    /// <param name="searchVideoNo"></param>
    /// <returns></returns>
    public VideoData GetVideoData(int searchVideoNo) {
        return videoDataSO.videoDatasList.Find(x => x.videoNo == searchVideoNo);
    }


    public ItemData GetItemData(int searchItemNo) {
        return itemDataSO.itemDataList.Find(x => x.itemNo == searchItemNo);
    }
}
