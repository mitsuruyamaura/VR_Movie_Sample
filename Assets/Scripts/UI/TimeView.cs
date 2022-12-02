using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TimeView : MonoBehaviour
{
    [SerializeField]
    private Text txtTime;

    [SerializeField]
    private TMP_Text tmpTime;

    [SerializeField]
    private Ease ease;

    private int oldTime = 0;
    private float duration = 0.5f;


    public void DisplayTime(int time) {
        //txtTime.text = time.ToString();

       ///tmpTime.text = time.ToString();

        txtTime.DOCounter(oldTime, time, duration).SetEase(Ease.Linear);

        tmpTime.DOCounter(oldTime, time, duration).SetEase(ease);
        oldTime = time;
    }


    public void DisplayTimeTween(int oldTime, int newTime) {
        txtTime.DOCounter(oldTime, newTime, duration).SetEase(Ease.Linear);

        tmpTime.DOCounter(oldTime, newTime, duration).SetEase(ease);
    }
}
