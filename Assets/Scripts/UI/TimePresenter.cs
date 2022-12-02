using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class TimePresenter : MonoBehaviour
{
    [SerializeField]
    private TimeModel timeModel;

    [SerializeField]
    private TimeView timeView;

    
    void Start()
    {
        //timeModel.TotalTime.Subscribe(x => timeView.DisplayTime(x)).AddTo(this);

        timeModel.TotalTime.Zip(timeModel.TotalTime.Skip(1), (oldTime, newTime) => (oldTime, newTime))
            .Subscribe(x => timeView.DisplayTimeTween(x.oldTime, x.newTime))
            .AddTo(this);

        timeModel.TimerAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }
}