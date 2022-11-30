using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class VideoClipManager : MonoBehaviour {

    public static VideoClipManager instance;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public VideoClip clip;

    public bool IsVideoPlaying { get => videoPlayer.isPlaying; }

    private float fadeDuration = 1.0f;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // 初期化
        Initialize();
    }

    /// <summary>
    /// Video 処理の初期化
    /// </summary>
    private void Initialize() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        videoPlayer.clip = null;

        // 解放して前のテクスチャを削除
        videoPlayer.targetTexture.Release();
    }

    /// <summary>
    /// デバッグ用(Awake でやらないこと。DataBaseManager の準備が競合して間に合わない)
    /// </summary>
    /// <returns></returns>
    IEnumerator Start() {

        yield return null;

        // VideoClip の準備
        //PrepareVideoClip(1);
    }

    /// <summary>
    /// VideoClip の準備
    /// </summary>
    /// <param name="videoNo"></param>
    public void PrepareVideoClip(int setVideoNo, VideoClip sourceVideoClip = null) {

        // VideoClip が未設定なら
        if (videoPlayer.clip == null) {

            // VideoClip の引数情報がない場合
            if (sourceVideoClip == null) {

                // 対象の VideoClip を検索して設定
                videoPlayer.clip = DataBaseManager.instance.GetVideoData(setVideoNo).videoClip;
            } else {

                // VideoClip の情報がある場合は、それを使う
                videoPlayer.clip = sourceVideoClip;
            }
        }

        // 読み込み後のイベントのコールバック登録
        videoPlayer.prepareCompleted += OnCompletePrepare;

        // 読み込み開始
        videoPlayer.Prepare();

        Debug.Log("VideoClip ロード開始");

        // TODO フェイドインと合わせる



        /// <summary>
        /// Prepare 完了時に呼ばれるコールバック
        /// </summary>
        /// <param name="vp"></param>
        void OnCompletePrepare(VideoPlayer vp) {

            // イベントのコールバックから削除(残っていると次回も実行されるため)
            videoPlayer.prepareCompleted -= OnCompletePrepare;

            Debug.Log("VideoClip ロード完了");

            // 再生
            StartCoroutine(PlayVideo());
        }

        //// 読み込むまで待機(videoPlayer.prepareCompleted を使わない場合)
        //while (!videoPlayer.isPrepared)
        //    yield return null;
    }

    /// <summary>
    /// VideoClip の再生
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayVideo() {
        canvasGroup.blocksRaycasts = true;

        // TODO フェードインして再生(簡易。後でトランジションと合わせる)
        canvasGroup.DOFade(1.0f, fadeDuration);   // OnComplete で Video の Play するとダメ

        videoPlayer.Play();

        Debug.Log("VideoClip 再生");

        // 再生が終了するか、スキップするまで待機(一時停止の処理と、一時停止した途中からの再生処理はないので、それらを実装したい場合にはこの条件式を変更する必要がある)
        while (videoPlayer.isPlaying) {

            // 再生中にタップしたら再生停止してスキップ
            if (Input.GetMouseButtonDown(0)) {
                SkipVideo();
            }
            yield return null;
        }

        // 停止
        StopVideo();
    }

    /// <summary>
    /// VideoClip の一時停止
    /// </summary>
    public void SkipVideo() {

        // 再生中の VideoClip がある場合
        if (videoPlayer.isPlaying) {

            // 内部的には一時停止(isPlayng が false になる)だが、今回は PlayVideo メソッドの while 文の条件式において、この処理のみでスキップするように条件設定している
            videoPlayer.Pause();
            Debug.Log("VideoClip スキップ");
        }
    }

    /// <summary>
    /// VideoClip の停止
    /// </summary>
    public void StopVideo() {

        // 停止
        videoPlayer.Stop();

        // フェードアウトして初期化
        canvasGroup.DOFade(0, fadeDuration)
            .OnComplete(() => {
                Initialize();
            });

        Debug.Log("VideoClip 停止");
    }


//***************  未使用  ******************//


    public void PauseVideo() {
        // 再生中の VideoClip がある場合
        if (videoPlayer.isPlaying) {

            videoPlayer.Pause();
            Debug.Log("VideoClip 一時停止");
        }
    }


    public void ResumeVideo() {
        // 再生中の VideoClip がある場合
        if (videoPlayer.clip) {

            // isPlayng が true になる
            videoPlayer.Play();
            Debug.Log("VideoClip 一時停止した部分から再生");
        }
    }
}