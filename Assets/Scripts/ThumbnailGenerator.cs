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
        // Debug�p
        //thumbnailViewList = GenerateThumbnailViews();
    }

    /// <summary>
    /// �����ݒ�
    /// �T���l�C���̃��X�g��ǉ�
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
    /// �������Ă���A�C�e���ɕR�Â����[�r�[�̏������T���l�C���̃{�^�����쐬
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
