using UnityEngine;
using TMPro;
using DG.Tweening;

public class ObjectInfoView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TMP_Text txtInfo;


    // �f�o�b�O�p
    //private void Start() {
    //    SetUpObjectInfoDetail();
    //}

    /// <summary>
    /// �����ݒ�
    /// </summary>
    /// <param name="itemName"></param>
    public void SetUpObjectInfoDetail(string itemName) {
        canvasGroup.alpha = 0;
        txtInfo.text = itemName;

        // foward(z��)�̕��������邱�Ƃŕ��������]����̂�␳
        txtInfo.transform.localScale = new Vector3(-1, 1, 1);
    }

    /// <summary>
    /// �A�C�e�����̉�ʕ\��
    /// </summary>
    public void ShowItemName() {
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// �A�C�e�����̉�ʔ�\��
    /// </summary>
    public void HideItemName() {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// Text ���v���C���[(�J����)�̕����Ɍ����A�p�x�ɂ���Č����Ȃ��Ȃ��Ă��܂����Ƃ�h��
    /// </summary>
    public void LookPlayer() {
        txtInfo.transform.LookAt(Camera.main.transform.position);
    }
}
