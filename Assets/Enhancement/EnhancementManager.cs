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

        healButton.onClick.AddListener(OnClickHealButton);
        healButton.interactable = false;

        enemyEnforceButton.onClick.AddListener(OnClickEnemyEnforceButton);
        enemyEnforceButton.interactable = false;

        enemyNumButton.onClick.AddListener(OnClickEnemyNumButton);
        enemyNumButton.interactable = false;

        cheerButton.onClick.AddListener(OnClickCheerButton);
        cheerButton.interactable = false;

        hpButton.onClick.AddListener(OnClickHPUpButton);
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

    private void OnDestroy()
    {
        healButton.onClick.RemoveListener(OnClickHealButton);
        enemyEnforceButton.onClick.RemoveListener(OnClickEnemyNumButton);
        enemyNumButton.onClick.RemoveListener(OnClickEnemyNumButton);
        hpButton.onClick.RemoveListener(OnClickHPUpButton);
        cheerButton.onClick.RemoveListener(OnClickCheerButton);
    }
}
