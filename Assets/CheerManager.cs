using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class CheerManager : MonoBehaviour
{
    [SerializeField] private Button cheerButton;

    //player の変数に変更する必要あり
    public float speed;
    public float damageCut;

    public float speedUpAmount = 0.1f;
    public float damageReduceAmount = 0.1f;
    public float cheerDuration = 1.0f;

    private void Start()
    {
        cheerButton.OnClickAsObservable().Do(_ => {
            damageCut += damageReduceAmount;
            speed += speedUpAmount;
        })
        .Delay(TimeSpan.FromSeconds(1.0f))
        .Subscribe(_ => {
            damageCut -= damageReduceAmount;
            speed -= speedUpAmount;
        })
        .AddTo(this);
    }
}
