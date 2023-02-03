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
    private Button btnThumbnail;   // Presenter でイベント Bind

    [SerializeField]
    private VideoData videoData;


    /// <summary>
    /// ThumbnailView の初期設定
    /// </summary>
    /// <param name="itemData"></param>
    public void SetUpThumbnailView(ItemData itemData) {
        // 設定
        txtTitle.text = itemData.itemName;
        imgThumbnail.sprite = itemData.itemSprite;

        // ムービー情報の設定(このサムネイルと紐づくムービーの情報を登録)
        videoData = DataBaseManager.instance.GetVideoData(itemData.itemNo);
    }

    /// <summary>
    /// サムネイルをクリックした際の処理
    /// </summary>
    public void ShowMovie() {

        // このサムネイルのムービー再生
        VideoClipManager.instance.PrepareVideoClip(videoData.videoNo);

        Debug.Log("サムネイル動画再生");
    }

    /// <summary>
    /// ボタンの取得
    /// </summary>
    /// <returns></returns>
    public Button GetThumbnailButton() {
        return btnThumbnail;
    }
}
