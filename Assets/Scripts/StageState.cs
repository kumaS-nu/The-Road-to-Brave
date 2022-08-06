using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// ステージの状態（お金，強化レベル）を保持するクラス．
/// </summary>
public sealed class StageState
{
    public static StageState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StageState();
            }
            return instance;
        }
    }

    private static StageState instance;

    public long Money { get; private set; }

    public Dictionary<EnhancementContent, int> EnhancementLevel { get; private set; }

    [SerializeField]
    public readonly List<long> costTable = new()
    { 
        1000,
        10000,
        100000,
        1000000,
        10000000,
        100000000,
        1000000000,
        10000000000,
        100000000000,
        1000000000000,
        10000000000000
    };

    public StageState()
    {
        Money = 6667776666666;
        EnhancementLevel = new Dictionary<EnhancementContent, int>();
        foreach(EnhancementContent content in Enum.GetValues(typeof(EnhancementContent)))
        {
            EnhancementLevel[content] = 0;
        }
    }

    /// <summary>
    /// （敵を倒した結果）お金を得る．
    /// </summary>
    /// <param name="earn">得たお金．</param>
    /// <exception cref="ArgumentException">お金が負．</exception>
    public void EarnedMoney(int earn)
    {
        if (earn < 0)
        {
            throw new ArgumentException("Earn is negative.");
        }
        Money += earn;
        Debug.Log("Earned money : " + earn + ", now money = " + Money);
    }

    /// <summary>
    /// 強化のレベルアップが利用可能か．
    /// </summary>
    /// <param name="enhancement">問い合わせるレベルアップ．</param>
    /// <returns>true = 利用可能．</returns>
    /// <exception cref="ArgumentException">その強化は存在しない．</exception>
    public bool IsAvailableUpgradeEnhancement(EnhancementContent enhancement)
    {
        if(EnhancementLevel.TryGetValue(enhancement, out int ret)){
            return Money >= costTable[ret];
        }
        throw new ArgumentException("Given Enhancement is Invalid.");
    }

    /// <summary>
    /// 強化をレベルアップする．
    /// </summary>
    /// <param name="enhancement">レベルアップする強化内容．</param>
    /// <returns>true = 強化成功．</returns>
    /// <exception cref="ArgumentException">その強化は存在しない．</exception>
    public bool UpgradeEnhancement(EnhancementContent enhancement)
    {
        if (IsAvailableUpgradeEnhancement(enhancement)){
            Money -= costTable[EnhancementLevel[enhancement]];
            EnhancementLevel[enhancement]++;
            Debug.Log("Upgrade : " + enhancement + " , " + (EnhancementLevel[enhancement] - 1) + " to " + EnhancementLevel[enhancement]);
            Debug.Log("Money : " + (Money + costTable[EnhancementLevel[enhancement] - 1]) + " to " + Money);
            return true;
        }
        return false;
    }
}
