using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private Button healButton;
    [SerializeField] private Button enemyEnforceButton;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button enemyNumButton;
    [SerializeField] private Button cheerButton;


    private StageState stageState;
    private void Start()
    {
        stageState = StageState.Instance;

        healButton.OnClickAsObservable().Subscribe(_=>OnClickHealButton()).AddTo(this);
        healButton.interactable = false;

        enemyEnforceButton.OnClickAsObservable().Subscribe(_ => OnClickEnemyEnforceButton()).AddTo(this);
        enemyEnforceButton.interactable = false;

        enemyNumButton.OnClickAsObservable().Subscribe(_ => OnClickEnemyNumButton()).AddTo(this);
        enemyNumButton.interactable = false;

        cheerButton.OnClickAsObservable().Subscribe(_ => OnClickCheerButton()).AddTo(this);
        cheerButton.interactable = false;

        hpButton.OnClickAsObservable().Subscribe(_ => OnClickHPUpButton()).AddTo(this);
        hpButton.interactable = false;

    }

    private void Update()
    {
        if (stageState == null)
        {
            return;
        }

        if (stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Heal))
        {
            healButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.EnemyStrength))
        {
            enemyEnforceButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.EnemyEncount))
        {
            enemyNumButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Cheer))
        {
            cheerButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Armor))
        {
            hpButton.interactable = true;
        }
    }

    private void OnClickHealButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Heal);
        healButton.interactable = false;
    }

    private void OnClickEnemyEnforceButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.EnemyStrength);
        enemyEnforceButton.interactable = false;
    }
    private void OnClickEnemyNumButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.EnemyEncount);
        enemyNumButton.interactable = false;
    }

    private void OnClickHPUpButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Armor);
        hpButton.interactable = false;
    }

    private void OnClickCheerButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Cheer);
        cheerButton.interactable = false;
    }
}
