using UnityEngine;
using TMPro;
using DG.Tweening;

public class ObjectInfoView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TMP_Text txtInfo;


    // デバッグ用
    //private void Start() {
    //    SetUpObjectInfoDetail();
    //}

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="itemName"></param>
    public void SetUpObjectInfoDetail(string itemName) {
        canvasGroup.alpha = 0;
        txtInfo.text = itemName;

        // foward(z軸)の方を向けることで文字が反転するのを補正
        txtInfo.transform.localScale = new Vector3(-1, 1, 1);
    }

    /// <summary>
    /// アイテム名の画面表示
    /// </summary>
    public void ShowItemName() {
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// アイテム名の画面非表示
    /// </summary>
    public void HideItemName() {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// Text をプレイヤー(カメラ)の方向に向け、角度によって見えなくなってしまうことを防ぐ
    /// </summary>
    public void LookPlayer() {
        txtInfo.transform.LookAt(Camera.main.transform.position);
    }
}
