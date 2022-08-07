using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;
using Cysharp.Threading.Tasks;

/// <summary>
/// 応援ボタンに関することを取り扱う．
/// </summary>
public class CheerManager : MonoBehaviour
{
    [SerializeField] private Button cheerButton;

    [SerializeField] private TextMeshProUGUI cheerButtonText;
    [SerializeField] private RectTransform audiences;

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle2;

    [SerializeField] private float cheerDuration = 1.0f;

    private int m_cpm = 0;
    private void Start()
    {
        cheerButton.OnClickAsObservable().Do(_ => {
            m_cpm++;
            particle.Play();
            particle2.Play();
        })
        .Delay(TimeSpan.FromSeconds(1.0f))
        .Subscribe(_ => {
            m_cpm--;
        })
        .AddTo(this);
    }


    private void Update()
    {
        var posY = Mathf.Lerp(-250f, -150f, GetCheerPower());
        Vector3 pos = audiences.anchoredPosition;
        pos.y = posY;
        audiences.anchoredPosition = pos;
    }

    public float GetCheerPower()
    {
        return 1 - Mathf.Exp(-m_cpm * 0.2f);
    }


    public async void OnClikCheerLevelUp()
    {
        cheerButtonText.text = "応援の効果UP！！";
        cheerButton.interactable = false;
        await UniTask.Delay(1000);
        cheerButton.interactable = true;
        cheerButtonText.text = "クリックで勇者を応援！！";
    }
}
