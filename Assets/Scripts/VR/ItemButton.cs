using UnityEngine;
using UniRx;

public class ItemButton : MonoBehaviour
{
    private UnityEngine.UI.Button btnItem;

    [SerializeField]
    private int movieNo;


    void Start() {
        if (TryGetComponent(out btnItem)) {
            btnItem.onClick.AddListener(OnClickItemButton);

            btnItem.OnClickAsObservable()
            .ThrottleFirst(System.TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => VideoClipManager.instance.PrepareVideoClip(movieNo))
            .AddTo(this);
        } else {
            Debug.Log("Button ���擾");

            DebugUIBuilder.instance.AddLabel("Button ���擾", DebugUIBuilder.DEBUG_PANE_CENTER);
            DebugUIBuilder.instance.Show();
        }
    }

    /// <summary>
    /// �{�^���������̏���
    /// </summary>
    public void OnClickItemButton() {
        Debug.Log("�{�^��������");

        DebugUIBuilder.instance.AddLabel("�{�^��������", DebugUIBuilder.DEBUG_PANE_CENTER);
        DebugUIBuilder.instance.Show();
    }
}
