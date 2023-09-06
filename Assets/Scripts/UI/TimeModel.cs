using System.Collections;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx.Triggers;　　// UpdateAsObservable() の際に必要
using InputAsRx;         // InputAsObservable 


// UniRx のInput の拡張
// キー入力、マウス入力をIObservable<T>に変換して使う
// https://qiita.com/Euglenach/items/29833548294a8b822b79


public enum GameState {
    Wait,
    Play
}

public class TimeModel : MonoBehaviour
{
    public ReactiveProperty<int> TotalTime = new();


    void Start()
    {
        // Interval と Timer では、最初のメッセージを発行するタイミングが異なる
        // また Interval は引数を１つしか指定できない
        // どちらも無限に繰り返すので、Dispose が必要


        // Obsevable.Interval は AddTo が必要
        // Subscribe してから1秒間隔でメッセージを発行する
        // (最初に1秒待機してから、1秒間隔でメッセージ発行)
        //Observable.Interval(System.TimeSpan.FromSeconds(1))
        //    .Where(_ => gameState == GameState.Play)   // このパターンだと、途中で再開した場合にも、時間は経過し続けている
        //    .Subscribe(x => Debug.Log($"経過時間 : {x + 1} 秒"))    // "経過時間 : " + x + "秒")
        //    .AddTo(this);

        // Obsevable.Timer は AddTo が必要
        // Subscribe した直後にメッセージを発行し、その後、1秒間隔でメッセージ発行
        // 第1引数が最初のメッセージを発行するまでの待機時間。今回は 0 なので、待機時間なし
        // 第2引数が繰り返しのメッセージを発行する感覚
        //Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"経過時間 : {x} 秒"))
        //    .AddTo(this);

        //StartCoroutine(ObserveTime());
        //TimerAsync(this.GetCancellationTokenOnDestroy()).Forget();

        //// OnCompleted があり、メッセージの発行を行うので、Dispose(AddTo)不要
        //this.UpdateAsObservable()
        //    .Subscribe(_ => {
        //        TimerUpdate();
        //        if (Input.GetMouseButtonDown(0)) {
        //            gameState = gameState == GameState.Play ? GameState.Wait : GameState.Play;
        //        }
        //    });

        // 挙動的には UpdateAsObservable と同じだが、MonoBehaviour に紐づかなくても利用できる
        // ただし OnComplete メッセージを発行しないので、UpdateAsObservable の方が安全(手動で Dispose(AddTo) しないといけない。)
        // そのため MonoBehaviour に紐づけない場合のみ利用する方がいい
        Observable.EveryUpdate()
            .Subscribe(_ => {
                TimerUpdate();
                //if (Input.GetMouseButtonDown(0)) {
                //    gameState = gameState == GameState.Play ? GameState.Wait : GameState.Play;
                //}
            })
            .AddTo(this);


        // 上記の拡張メソッド
        InputAsObservable.GetMouseButtonDown(0)
            .Subscribe(_ => gameState = gameState == GameState.Play ? GameState.Wait : GameState.Play)
            .AddTo(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async UniTask TimerAsync(CancellationToken token) {
        //var token = this.GetCancellationTokenOnDestroy();

        // キャンセル処理が発生するまで繰り返す
        while (!token.IsCancellationRequested) {

            if (gameState == GameState.Wait) {
                //await UniTask.Yield();
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                continue;
            }

            totalTime++;
            TotalTime.Value += 10;

            // 1秒待つ => 途中で GameState が切り替わっても、次の処理までは止まらないので、+1秒される
            // いやな場合には、変数で加算した方がよい
            await UniTask.Delay(1000, cancellationToken: token);
            Debug.Log($"経過時間 : {totalTime} 秒");
        }

        // 指定秒(ここでは1秒)待機するか、指定した処理(ここではマウスクリックする)まで待機
        //await UniTaskHelper.DelaySkippable(1000, () => Input.GetMouseButtonDown(0));
    }

    float timer = 0;
    float interval = 1.0f;
    float totalTime;
    GameState gameState;

    void Update() {
        //TimerUpdate();
    }

    /// <summary>
    /// 時間の測定
    /// </summary>
    public void TimerUpdate() {
        if (gameState == GameState.Wait) {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= interval) {
            timer = 0;
            totalTime++;
            TotalTime.Value++;
            //Debug.Log($"経過時間 : {totalTime} 秒");
        }
    }


    public void PrepareObserveTime() {
        StartCoroutine(ObserveTime());
    }

    /// <summary>
    /// 時間の測定
    /// </summary>
    private IEnumerator ObserveTime() {
        while (true) {
            if (gameState == GameState.Wait) {
                yield return null;
                continue;
            }
            timer += Time.deltaTime;
            if (timer >= interval) {
                timer = 0;
                totalTime++;
                TotalTime.Value++;
                Debug.Log($"経過時間 : {totalTime} 秒");
            }
            yield return null;
        }

        //while (true) {
        //    yield return new WaitForSeconds(1.0f);
        //    totalTime++;
        //    TotalTime.Value++;
        //    Debug.Log($"経過時間 : {totalTime} 秒");
        //}
    }
}
