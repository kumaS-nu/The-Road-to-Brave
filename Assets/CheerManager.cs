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
    [SerializeField] private RectTransform audiences;

    private float speedUpAmount = 0.1f;
    private float damageReduceAmount = 0.03f;
    private float cheerDuration = 1.0f;

    private int clickNum = 0;
    private int clickMax = 7;
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
            clickNum++;
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
            clickNum--;
        })
        .AddTo(this);
    }


    private void Update()
    {
        var value = clickNum / (float)clickMax;
        var posY = Mathf.Lerp(-250f, -150f, value);
        Vector3 pos = audiences.anchoredPosition;
        pos.y = posY;
        audiences.anchoredPosition = pos;
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
        damageReduceAmount += 0.02f;
        cheerButton.interactable = true;
        cheerButtonText.text = "Click to cheer your Hero!";
    }
}
