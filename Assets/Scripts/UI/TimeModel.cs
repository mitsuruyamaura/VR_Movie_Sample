using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public enum GameState {
    Wait,
    Play
}

public class TimeModel : MonoBehaviour
{
    void Start()
    {
        // Interval と Timer では、最初のメッセージを発行するタイミングが異なる
        // また Interval は引数を１つしか指定できない
        // どちらも無限に繰り返すので、Dispose が必要

        // Subscribe してから1秒間隔でメッセージを発行する
        // (最初に1秒待機してから、1秒間隔でメッセージ発行)
        //Observable.Interval(System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"経過時間 : {x + 1} 秒"))    // "経過時間 : " + x + "秒")
        //    .AddTo(this);

        // Subscribe した直後にメッセージを発行し、その後、1秒間隔でメッセージ発行
        // 第1引数が最初のメッセージを発行するまでの待機時間。今回は 0 なので、待機時間なし
        // 第2引数が繰り返しのメッセージを発行する感覚
        //Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"経過時間 : { x } 秒"))
        //    .AddTo(this);
    }

    float timer = 0;
    float interval = 1.0f;
    float totalTime;
    GameState gameState;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval) {
            timer = 0;
            totalTime++;
            Debug.Log($"経過時間 : {totalTime} 秒");
        }
    }


    private IEnumerator ObserveTime() {
        //while (true) {
        //    timer += Time.deltaTime;
        //    if (timer >= interval) {
        //        timer = 0;
        //        totalTime++;
        //        Debug.Log($"経過時間 : {totalTime} 秒");
        //    }
        //    yield return null;
        //}

        while (true) {
            yield return new WaitForSeconds(1.0f);
            Debug.Log($"経過時間 : {totalTime} 秒");
        }
    }
}
