using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private BraveHp braveHp;
    [SerializeField] private CheerManager cheermanager;

    [SerializeField] private Button healButton;
    [SerializeField] private Button enemyEnforceButton;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button enemyNumButton;
    [SerializeField] private Button cheerButton;

    [SerializeField] private TextMeshProUGUI hPText;
    [SerializeField] private TextMeshProUGUI enemyEnforceText;
    [SerializeField] private TextMeshProUGUI healText;
    [SerializeField] private TextMeshProUGUI enemyNumText;
    [SerializeField] private TextMeshProUGUI cheerText;

    [SerializeField] private List<GameObject> lawnList;

    private bool healMax = false;
    private bool enemyEnforceMax = false;
    private bool hPMax = false;
    private bool enemyNumMax = false;
    private bool cheerMax = false;

    private const int maxLevel = 9;


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

        hPText.text = $"Lv 1 <sprite=0>{stageState?.costTable[0]}";
        enemyEnforceText.text = $"Lv 1 <sprite=0>{stageState?.costTable[0]}";
        healText.text = $"Lv 1 <sprite=0>{stageState?.costTable[0]}";
        cheerText.text = $"Lv 1 <sprite=0>{stageState?.costTable[0]}";
        enemyNumText.text = $"Lv 1 <sprite=0>{stageState?.costTable[0]}";

        foreach(var lawn in lawnList)
        {
            lawn.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (stageState == null)
        {
            return;
        }

        if (stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Heal) && !healMax)
        {
            healButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.EnemyStrength) && !enemyEnforceMax)
        {
            enemyEnforceButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.EnemyEncount) && !enemyNumMax)
        {
            enemyNumButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Cheer) && !cheerMax)
        {
            cheerButton.interactable = true;
        }
        if(stageState.IsAvailableUpgradeEnhancement(EnhancementContent.Armor)&& !hPMax)
        {
            hpButton.interactable = true;
        }
    }

    private void OnClickHealButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Heal);
        healButton.interactable = false;
        var level = stageState?.EnhancementLevel[EnhancementContent.Heal];
        healText.text = $"Lv {level+1} <sprite=0>{stageState?.costTable[(int)level]}";
        lawnList[(int)level - 1].gameObject.SetActive(true);
        if (level > maxLevel)
        {
            healMax = true;
            healText.text = "Level Max";
        }
    }

    private void OnClickEnemyEnforceButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.EnemyStrength);
        enemyEnforceButton.interactable = false;
        var level = stageState?.EnhancementLevel[EnhancementContent.EnemyStrength];
        enemyEnforceText.text = $"Lv {level + 1} <sprite=0>{stageState?.costTable[(int)level]}";
        if (level > maxLevel)
        {
            enemyEnforceMax = true;
            enemyEnforceText.text = "Level Max";
        }
    }
    private void OnClickEnemyNumButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.EnemyEncount);
        enemyNumButton.interactable = false;
        var level = stageState?.EnhancementLevel[EnhancementContent.EnemyEncount];
        enemyNumText.text = $"Lv {level + 1} <sprite=0>{stageState?.costTable[(int)level]}";
        if (level > maxLevel)
        {
            enemyNumMax = true;
            enemyNumText.text = "Level Max";
        }
    }

    private void OnClickHPUpButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Armor);
        hpButton.interactable = false;
        var level = stageState?.EnhancementLevel[EnhancementContent.Armor];
        hPText.text = $"Lv {level + 1} <sprite=0>{stageState?.costTable[(int)level]}";
        if (level > maxLevel)
        {
            hPMax = true;
            hPText.text = "Level Max";
        }
        braveHp.HpUpdate();
    }

    private void OnClickCheerButton()
    {
        stageState?.UpgradeEnhancement(EnhancementContent.Cheer);
        cheerButton.interactable = false;
        var level = stageState?.EnhancementLevel[EnhancementContent.Cheer];
        cheerText.text = $"Lv {level + 1} <sprite=0>{stageState?.costTable[(int)level]}";
        if (level > maxLevel)
        {
            cheerMax = true;
            cheerText.text = "Level Max";
        }
        if (cheermanager != null)
        {
           cheermanager.OnClikCheerLevelUp();
        }
    }
}
