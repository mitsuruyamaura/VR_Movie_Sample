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

        // ������
        Initialize();
    }

    /// <summary>
    /// Video �����̏�����
    /// </summary>
    private void Initialize() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        videoPlayer.clip = null;

        // ������đO�̃e�N�X�`�����폜
        videoPlayer.targetTexture.Release();
    }

    /// <summary>
    /// �f�o�b�O�p(Awake �ł��Ȃ����ƁBDataBaseManager �̏������������ĊԂɍ���Ȃ�)
    /// </summary>
    /// <returns></returns>
    IEnumerator Start() {

        yield return null;

        // VideoClip �̏���
        //PrepareVideoClip(1);
    }

    /// <summary>
    /// VideoClip �̏���
    /// </summary>
    /// <param name="videoNo"></param>
    public void PrepareVideoClip(int setVideoNo, VideoClip sourceVideoClip = null) {

        // VideoClip �����ݒ�Ȃ�
        if (videoPlayer.clip == null) {

            // VideoClip �̈�����񂪂Ȃ��ꍇ
            if (sourceVideoClip == null) {

                // �Ώۂ� VideoClip ���������Đݒ�
                videoPlayer.clip = DataBaseManager.instance.GetVideoData(setVideoNo).videoClip;
            } else {

                // VideoClip �̏�񂪂���ꍇ�́A������g��
                videoPlayer.clip = sourceVideoClip;
            }
        }

        // �ǂݍ��݌�̃C�x���g�̃R�[���o�b�N�o�^
        videoPlayer.prepareCompleted += OnCompletePrepare;

        // �ǂݍ��݊J�n
        videoPlayer.Prepare();

        Debug.Log("VideoClip ���[�h�J�n");

        // TODO �t�F�C�h�C���ƍ��킹��



        /// <summary>
        /// Prepare �������ɌĂ΂��R�[���o�b�N
        /// </summary>
        /// <param name="vp"></param>
        void OnCompletePrepare(VideoPlayer vp) {

            // �C�x���g�̃R�[���o�b�N����폜(�c���Ă���Ǝ�������s����邽��)
            videoPlayer.prepareCompleted -= OnCompletePrepare;

            Debug.Log("VideoClip ���[�h����");

            // �Đ�
            StartCoroutine(PlayVideo());
        }

        //// �ǂݍ��ނ܂őҋ@(videoPlayer.prepareCompleted ���g��Ȃ��ꍇ)
        //while (!videoPlayer.isPrepared)
        //    yield return null;
    }

    /// <summary>
    /// VideoClip �̍Đ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayVideo() {
        canvasGroup.blocksRaycasts = true;

        // TODO �t�F�[�h�C�����čĐ�(�ȈՁB��Ńg�����W�V�����ƍ��킹��)
        canvasGroup.DOFade(1.0f, fadeDuration);   // OnComplete �� Video �� Play ����ƃ_��

        videoPlayer.Play();

        Debug.Log("VideoClip �Đ�");

        // �Đ����I�����邩�A�X�L�b�v����܂őҋ@(�ꎞ��~�̏����ƁA�ꎞ��~�����r������̍Đ������͂Ȃ��̂ŁA�����������������ꍇ�ɂ͂��̏�������ύX����K�v������)
        while (videoPlayer.isPlaying) {

            // �Đ����Ƀ^�b�v������Đ���~���ăX�L�b�v
            if (Input.GetMouseButtonDown(0)) {
                SkipVideo();
            }
            yield return null;
        }

        // ��~
        StopVideo();
    }

    /// <summary>
    /// VideoClip �̈ꎞ��~
    /// </summary>
    public void SkipVideo() {

        // �Đ����� VideoClip ������ꍇ
        if (videoPlayer.isPlaying) {

            // �����I�ɂ͈ꎞ��~(isPlayng �� false �ɂȂ�)�����A����� PlayVideo ���\�b�h�� while ���̏������ɂ����āA���̏����݂̂ŃX�L�b�v����悤�ɏ����ݒ肵�Ă���
            videoPlayer.Pause();
            Debug.Log("VideoClip �X�L�b�v");
        }
    }

    /// <summary>
    /// VideoClip �̒�~
    /// </summary>
    public void StopVideo() {

        // ��~
        videoPlayer.Stop();

        // �t�F�[�h�A�E�g���ď�����
        canvasGroup.DOFade(0, fadeDuration)
            .OnComplete(() => {
                Initialize();
            });

        Debug.Log("VideoClip ��~");
    }


//***************  ���g�p  ******************//


    public void PauseVideo() {
        // �Đ����� VideoClip ������ꍇ
        if (videoPlayer.isPlaying) {

            videoPlayer.Pause();
            Debug.Log("VideoClip �ꎞ��~");
        }
    }


    public void ResumeVideo() {
        // �Đ����� VideoClip ������ꍇ
        if (videoPlayer.clip) {

            // isPlayng �� true �ɂȂ�
            videoPlayer.Play();
            Debug.Log("VideoClip �ꎞ��~������������Đ�");
        }
    }
}