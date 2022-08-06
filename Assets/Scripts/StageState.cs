using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// �X�e�[�W�̏�ԁi�����C�������x���j��ێ�����N���X�D
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

    [SerializeField]
    public readonly List<int> damageTable = new()
    {
        5,
        10,
        15,
        20,
        25,
        30,
        35,
        40,
        45,
        50,
        55
    };

    [SerializeField]
    public readonly List<int> earnedMoneyTable = new()
    {
        30,
        60,
        100,
        200,
        1000,
        8000,
        10000,
        25000,
        100000,
        600000,
        1000000
    };

    [SerializeField]
    public readonly List<int> healTable = new()
    {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11
    };

    public StageState()
    {
        Money = 0;
        EnhancementLevel = new Dictionary<EnhancementContent, int>();
        foreach(EnhancementContent content in Enum.GetValues(typeof(EnhancementContent)))
        {
            EnhancementLevel[content] = 0;
        }
    }

    /// <summary>
    /// �i�G��|�������ʁj�����𓾂�D
    /// </summary>
    /// <param name="earn">���������D</param>
    /// <exception cref="ArgumentException">���������D</exception>
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
    /// �E�҂����񂾂Ƃ��ɏ������𔼕��ɂ���
    /// </summary>
    public void BraveDeath()
    {
        Money /= 2;
        Debug.Log("lost half money : " + Money);
    }

    /// <summary>
    /// �����̃��x���A�b�v�����p�\���D
    /// </summary>
    /// <param name="enhancement">�₢���킹�郌�x���A�b�v�D</param>
    /// <returns>true = ���p�\�D</returns>
    /// <exception cref="ArgumentException">���̋����͑��݂��Ȃ��D</exception>
    public bool IsAvailableUpgradeEnhancement(EnhancementContent enhancement)
    {
        if(EnhancementLevel.TryGetValue(enhancement, out int ret)){
            return Money >= costTable[ret];
        }
        throw new ArgumentException("Given Enhancement is Invalid.");
    }

    /// <summary>
    /// ���������x���A�b�v����D
    /// </summary>
    /// <param name="enhancement">���x���A�b�v���鋭�����e�D</param>
    /// <returns>true = ���������D</returns>
    /// <exception cref="ArgumentException">���̋����͑��݂��Ȃ��D</exception>
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
