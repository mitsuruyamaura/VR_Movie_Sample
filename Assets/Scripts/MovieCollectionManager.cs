using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

// Presenter として利用
public class MovieCollectionManager : MonoBehaviour
{
    [SerializeField]
    private Button btnShowCollection;

    [SerializeField]
    private Button btnClose;

    [SerializeField]
    private CanvasGroup movieCollectionCanvasGroup;

    private ThumbnailGenerator thumbnailGenerator;

    private ReactiveProperty<bool> IsSharedGate = new(true);

    void Start()
    {
        movieCollectionCanvasGroup.gameObject.SetActive(false);
        movieCollectionCanvasGroup.alpha = 0;

        btnShowCollection?.onClick.AddListener(ShowMovieCollectionPopup);
        btnClose?.onClick.AddListener(CloseMovieCollectionPopup);

        if (TryGetComponent(out thumbnailGenerator)) {
            InitMovieCollection();
        }
    }

    /// <summary>
    /// ムービー一覧を作成し、各サムネイルのボタンにムービー再生する機能を登録
    /// </summary>
    private void InitMovieCollection() {
        thumbnailGenerator.SetupThumbnailViewList();

        for (int i = 0; i < thumbnailGenerator.thumbnailViewList.Count; i++) {
            int index = i;
            thumbnailGenerator.thumbnailViewList[index].GetThumbnailButton()
                .OnClickAsObservable().Subscribe(_ => thumbnailGenerator.thumbnailViewList[index].ShowMovie()).AddTo(this);

            thumbnailGenerator.thumbnailViewList[index].GetThumbnailButton()
                .BindToOnClick(this.IsSharedGate, _ => 
                {
                    return Observable.Timer(System.TimeSpan.FromSeconds(1)).ForEachAsync(_ => Debug.Log("再クリック可能"));
                });
        }
    }

    /// <summary>
    /// ムービー一覧の表示
    /// </summary>
    private void ShowMovieCollectionPopup() {
        InitMovieCollection();
            
        movieCollectionCanvasGroup.gameObject.SetActive(true);
        movieCollectionCanvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// ムービー一覧の非表示
    /// </summary>
    private void CloseMovieCollectionPopup() {
        movieCollectionCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.Linear)
            .OnComplete(() => movieCollectionCanvasGroup.gameObject.SetActive(false));
    }
}
