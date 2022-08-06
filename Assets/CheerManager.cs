using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;
public class CheerManager : MonoBehaviour
{
    [SerializeField] private Button cheerButton;
    [SerializeField] private BraveMove braveMove;
    [SerializeField] private BraveHp braveHp;

    [SerializeField] private TextMeshProUGUI cheerButtonText;

    private float speedUpAmount = 0.1f;
    private float damageReduceAmount = 0.02f;
    private float cheerDuration = 1.0f;

    private void Start()
    {
        cheerButton.OnClickAsObservable().Do(_ => {
            if(braveMove.MoveTween != null)
            {
                braveMove.MoveTween.timeScale +=speedUpAmount;
            }

            if(braveHp != null)
            {
                braveHp._currentDamageDown -= damageReduceAmount;
            }
        })
        .Delay(TimeSpan.FromSeconds(1.0f))
        .Subscribe(_ => {
            if (braveMove.MoveTween != null)
            {
                braveMove.MoveTween.timeScale -= speedUpAmount;
            }

            if (braveHp != null)
            {
                braveHp._currentDamageDown += damageReduceAmount;
            }
        })
        .AddTo(this);
    }



    public void OnClikCheerLevelUp()
    {
        cheerButtonText.text = "Cheer Level Up!!!";
        cheerButton.interactable = false;
        StartCoroutine(nameof(EnableCheerButton));
    }

    private IEnumerator EnableCheerButton()
    {
        yield return new WaitForSeconds(1.0f);
        speedUpAmount += 0.1f;
        damageReduceAmount += 0.01f;
        cheerButton.interactable = true;
        cheerButtonText.text = "Click to cheer your Hero!";
    }
}
