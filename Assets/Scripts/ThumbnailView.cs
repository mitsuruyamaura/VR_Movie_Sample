using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ThumbnailView : MonoBehaviour
{
    [SerializeField]
    private Image imgThumbnail;

    [SerializeField]
    private Text txtTitle;
    //private TMP_Text txtTitle;

    [SerializeField]
    private Button btnThumbnail;   // Presenter �ŃC�x���g Bind

    [SerializeField]
    private VideoData videoData;

    void Start()
    {
        
    }


    public void SetUpThumbnailView(ItemData itemData) {
        txtTitle.text = itemData.itemName;
        imgThumbnail.sprite = itemData.itemSprite;

        videoData = DataBaseManager.instance.GetVideoData(itemData.itemNo);
    }

    public void ShowMovie() {

        VideoClipManager.instance.PrepareVideoClip(videoData.videoNo);

        Debug.Log("�T���l�C������Đ�");
    }

    public Button GetThumbnailButton() {
        return btnThumbnail;
    }
}
