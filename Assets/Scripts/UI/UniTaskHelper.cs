using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

public static class UniTaskHelper {
    public static async UniTask DelaySkippable(int delay_ms, Func<bool> cond) {

        // this.GetCancellationTokenOnDestroy() は this に紐づくので、静的クラスでは利用できないため、こちらを利用
        var cts = new CancellationTokenSource();

        await UniTask.WhenAny(
            UniTask.Delay(delay_ms, cancellationToken: cts.Token),
            UniTask.WaitUntil(cond, cancellationToken: cts.Token)
        );

        cts.Cancel();
    }
}