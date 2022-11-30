using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class MoviePlayTest : MonoBehaviour
{
    [SerializeField]
    private Button btnPlayTest;

    [SerializeField]
    private int movieNo;


    void Start()
    {
        // ���ꂾ�����ƘA���^�b�v�ŏd�����肵�Ă��܂��̂ŁAUniRx �g������������
        //btnPlayTest.onClick.AddListener(() => VideoClipManager.instance.PrepareVideoClip(movieNo));

        btnPlayTest.OnClickAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => VideoClipManager.instance.PrepareVideoClip(movieNo))
            .AddTo(this);
    }
}