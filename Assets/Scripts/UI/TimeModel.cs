using System.Collections;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx.Triggers;�@�@// UpdateAsObservable() �̍ۂɕK�v
using InputAsRx;         // InputAsObservable 


// UniRx ��Input �̊g��
// �L�[���́A�}�E�X���͂�IObservable<T>�ɕϊ����Ďg��
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
        // Interval �� Timer �ł́A�ŏ��̃��b�Z�[�W�𔭍s����^�C�~���O���قȂ�
        // �܂� Interval �͈������P�����w��ł��Ȃ�
        // �ǂ���������ɌJ��Ԃ��̂ŁADispose ���K�v


        // Obsevable.Interval �� AddTo ���K�v
        // Subscribe ���Ă���1�b�Ԋu�Ń��b�Z�[�W�𔭍s����
        // (�ŏ���1�b�ҋ@���Ă���A1�b�Ԋu�Ń��b�Z�[�W���s)
        //Observable.Interval(System.TimeSpan.FromSeconds(1))
        //    .Where(_ => gameState == GameState.Play)   // ���̃p�^�[�����ƁA�r���ōĊJ�����ꍇ�ɂ��A���Ԃ͌o�߂������Ă���
        //    .Subscribe(x => Debug.Log($"�o�ߎ��� : {x + 1} �b"))    // "�o�ߎ��� : " + x + "�b")
        //    .AddTo(this);

        // Obsevable.Timer �� AddTo ���K�v
        // Subscribe ��������Ƀ��b�Z�[�W�𔭍s���A���̌�A1�b�Ԋu�Ń��b�Z�[�W���s
        // ��1�������ŏ��̃��b�Z�[�W�𔭍s����܂ł̑ҋ@���ԁB����� 0 �Ȃ̂ŁA�ҋ@���ԂȂ�
        // ��2�������J��Ԃ��̃��b�Z�[�W�𔭍s���銴�o
        //Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"�o�ߎ��� : {x} �b"))
        //    .AddTo(this);

        //StartCoroutine(ObserveTime());
        //TimerAsync(this.GetCancellationTokenOnDestroy()).Forget();

        //// OnCompleted ������A���b�Z�[�W�̔��s���s���̂ŁADispose(AddTo)�s�v
        //this.UpdateAsObservable()
        //    .Subscribe(_ => {
        //        TimerUpdate();
        //        if (Input.GetMouseButtonDown(0)) {
        //            gameState = gameState == GameState.Play ? GameState.Wait : GameState.Play;
        //        }
        //    });

        // �����I�ɂ� UpdateAsObservable �Ɠ��������AMonoBehaviour �ɕR�Â��Ȃ��Ă����p�ł���
        // ������ OnComplete ���b�Z�[�W�𔭍s���Ȃ��̂ŁAUpdateAsObservable �̕������S(�蓮�� Dispose(AddTo) ���Ȃ��Ƃ����Ȃ��B)
        // ���̂��� MonoBehaviour �ɕR�Â��Ȃ��ꍇ�̂ݗ��p�����������
        Observable.EveryUpdate()
            .Subscribe(_ => {
                TimerUpdate();
                //if (Input.GetMouseButtonDown(0)) {
                //    gameState = gameState == GameState.Play ? GameState.Wait : GameState.Play;
                //}
            })
            .AddTo(this);


        // ��L�̊g�����\�b�h
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

        // �L�����Z����������������܂ŌJ��Ԃ�
        while (!token.IsCancellationRequested) {

            if (gameState == GameState.Wait) {
                //await UniTask.Yield();
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                continue;
            }

            totalTime++;
            TotalTime.Value += 10;

            // 1�b�҂� => �r���� GameState ���؂�ւ���Ă��A���̏����܂ł͎~�܂�Ȃ��̂ŁA+1�b�����
            // ����ȏꍇ�ɂ́A�ϐ��ŉ��Z���������悢
            await UniTask.Delay(1000, cancellationToken: token);
            Debug.Log($"�o�ߎ��� : {totalTime} �b");
        }

        // �w��b(�����ł�1�b)�ҋ@���邩�A�w�肵������(�����ł̓}�E�X�N���b�N����)�܂őҋ@
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
    /// ���Ԃ̑���
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
            //Debug.Log($"�o�ߎ��� : {totalTime} �b");
        }
    }


    public void PrepareObserveTime() {
        StartCoroutine(ObserveTime());
    }

    /// <summary>
    /// ���Ԃ̑���
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
                Debug.Log($"�o�ߎ��� : {totalTime} �b");
            }
            yield return null;
        }

        //while (true) {
        //    yield return new WaitForSeconds(1.0f);
        //    totalTime++;
        //    TotalTime.Value++;
        //    Debug.Log($"�o�ߎ��� : {totalTime} �b");
        //}
    }
}
