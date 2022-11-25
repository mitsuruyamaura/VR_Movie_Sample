using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ItemDetail : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TMP_Text txtInfo;

    private ItemData itemData;


    private void Start() {
        SetUpItemDetail();
    }


    public void SetUpItemDetail() {
        itemData = new ItemData("aaa", 0);
        canvasGroup.alpha = 0;
        txtInfo.text = itemData.itemName;
    }

    public void ShowItemName() {
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }


    public void HideItemName() {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear);
    }


    public void LookPlayer() {
        txtInfo.transform.LookAt(Camera.main.transform);
    }
}
