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
        // Debug—p
        //thumbnailViewList = GenerateThumbnailViews();
    }


    public void SetupThumbnailViewList() {
        thumbnailViewList = GenerateThumbnailViews();
    }

    /// <summary>
    /// 
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
