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
        btnPlayTest.onClick.AddListener(() => VideoClipManager.instance.PrepareVideoClip(movieNo));
    }
}