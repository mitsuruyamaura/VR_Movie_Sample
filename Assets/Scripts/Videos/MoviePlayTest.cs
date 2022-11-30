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
        // これだけだと連続タップで重複判定してしまうので、UniRx 使った方がいい
        //btnPlayTest.onClick.AddListener(() => VideoClipManager.instance.PrepareVideoClip(movieNo));

        btnPlayTest.OnClickAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => VideoClipManager.instance.PrepareVideoClip(movieNo))
            .AddTo(this);
    }
}