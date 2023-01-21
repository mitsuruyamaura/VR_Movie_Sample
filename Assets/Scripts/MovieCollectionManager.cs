using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieCollectionManager : MonoBehaviour
{
    [SerializeField]
    private Button btnShowCollection;

    [SerializeField]
    private Button btnClose;

    [SerializeField]
    private CanvasGroup movieCollectionCanvasGroup;

    void Start()
    {
        movieCollectionCanvasGroup.gameObject.SetActive(false);
        movieCollectionCanvasGroup.alpha = 0;
        btnShowCollection.onClick.AddListener(ShowMovieCollectionPopup);
        btnClose.onClick.AddListener(CloseMovieCollectionPopup);
    }

    
    private void ShowMovieCollectionPopup() {
        movieCollectionCanvasGroup.gameObject.SetActive(true);
        movieCollectionCanvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }


    private void CloseMovieCollectionPopup() {
        movieCollectionCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.Linear)
            .OnComplete(() => movieCollectionCanvasGroup.gameObject.SetActive(false));
    }
}
