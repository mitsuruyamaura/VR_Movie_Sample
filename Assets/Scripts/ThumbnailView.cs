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


    /// <summary>
    /// ThumbnailView �̏����ݒ�
    /// </summary>
    /// <param name="itemData"></param>
    public void SetUpThumbnailView(ItemData itemData) {
        // �ݒ�
        txtTitle.text = itemData.itemName;
        imgThumbnail.sprite = itemData.itemSprite;

        // ���[�r�[���̐ݒ�(���̃T���l�C���ƕR�Â����[�r�[�̏���o�^)
        videoData = DataBaseManager.instance.GetVideoData(itemData.itemNo);
    }

    /// <summary>
    /// �T���l�C�����N���b�N�����ۂ̏���
    /// </summary>
    public void ShowMovie() {

        // ���̃T���l�C���̃��[�r�[�Đ�
        VideoClipManager.instance.PrepareVideoClip(videoData.videoNo);

        Debug.Log("�T���l�C������Đ�");
    }

    /// <summary>
    /// �{�^���̎擾
    /// </summary>
    /// <returns></returns>
    public Button GetThumbnailButton() {
        return btnThumbnail;
    }
}
