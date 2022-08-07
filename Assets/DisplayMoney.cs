using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{

    private StageState stageState;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        stageState = StageState.Instance;
    }

    private void Update()
    {
        if (stageState == null) return;
        text.text = $"      ‚¨‹à:\n<sprite=0>{stageState.Money.ToString()}";

    }
}
