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
        // Interval �� Timer �ł́A�ŏ��̃��b�Z�[�W�𔭍s����^�C�~���O���قȂ�
        // �܂� Interval �͈������P�����w��ł��Ȃ�
        // �ǂ���������ɌJ��Ԃ��̂ŁADispose ���K�v

        // Subscribe ���Ă���1�b�Ԋu�Ń��b�Z�[�W�𔭍s����
        // (�ŏ���1�b�ҋ@���Ă���A1�b�Ԋu�Ń��b�Z�[�W���s)
        //Observable.Interval(System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"�o�ߎ��� : {x + 1} �b"))    // "�o�ߎ��� : " + x + "�b")
        //    .AddTo(this);

        // Subscribe ��������Ƀ��b�Z�[�W�𔭍s���A���̌�A1�b�Ԋu�Ń��b�Z�[�W���s
        // ��1�������ŏ��̃��b�Z�[�W�𔭍s����܂ł̑ҋ@���ԁB����� 0 �Ȃ̂ŁA�ҋ@���ԂȂ�
        // ��2�������J��Ԃ��̃��b�Z�[�W�𔭍s���銴�o
        //Observable.Timer(System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1))
        //    .Subscribe(x => Debug.Log($"�o�ߎ��� : { x } �b"))
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
            Debug.Log($"�o�ߎ��� : {totalTime} �b");
        }
    }


    private IEnumerator ObserveTime() {
        //while (true) {
        //    timer += Time.deltaTime;
        //    if (timer >= interval) {
        //        timer = 0;
        //        totalTime++;
        //        Debug.Log($"�o�ߎ��� : {totalTime} �b");
        //    }
        //    yield return null;
        //}

        while (true) {
            yield return new WaitForSeconds(1.0f);
            Debug.Log($"�o�ߎ��� : {totalTime} �b");
        }
    }
}
