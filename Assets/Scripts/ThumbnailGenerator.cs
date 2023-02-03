using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailGenerator : MonoBehaviour
{
    [SerializeField]
    private ThumbnailView thumbnailViewPrefab;

    [SerializeField]
    private Transform thumbnailCanvasTran;

    public List<ThumbnailView> thumbnailViewList = new();


    void Start()
    {
        // Debug用
        //thumbnailViewList = GenerateThumbnailViews();
    }

    /// <summary>
    /// 初期設定
    /// サムネイルのリストを追加
    /// </summary>
    public void SetupThumbnailViewList() {

        if (thumbnailViewList.Count > 0) {
            for (int i = 0; i < thumbnailViewList.Count; i++) {
                Destroy(thumbnailViewList[i].gameObject);
            }
            thumbnailViewList.Clear();
        }
        
        thumbnailViewList = GenerateThumbnailViews();
    }

    /// <summary>
    /// 所持しているアイテムに紐づくムービーの情報を持つサムネイルのボタンを作成
    /// </summary>
    /// <returns></returns>
    public List<ThumbnailView> GenerateThumbnailViews() {

        List<ThumbnailView> thumbnailViewList = new();
        
        for (int i = 0; i < UserData.instance.itemDataList.Count; i++) {
            ThumbnailView thumbnailView = Instantiate(thumbnailViewPrefab, thumbnailCanvasTran, false);
            thumbnailView.SetUpThumbnailView(UserData.instance.itemDataList[i]);
            thumbnailViewList.Add(thumbnailView);
        }
        return thumbnailViewList;
    }
}
