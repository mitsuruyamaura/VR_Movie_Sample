using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

public static class UniTaskHelper {
    public static async UniTask DelaySkippable(int delay_ms, Func<bool> cond) {

        // this.GetCancellationTokenOnDestroy() �� this �ɕR�Â��̂ŁA�ÓI�N���X�ł͗��p�ł��Ȃ����߁A������𗘗p
        var cts = new CancellationTokenSource();

        await UniTask.WhenAny(
            UniTask.Delay(delay_ms, cancellationToken: cts.Token),
            UniTask.WaitUntil(cond, cancellationToken: cts.Token)
        );

        cts.Cancel();
    }
}