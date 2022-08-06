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
        5000,
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
        20,
        50,
        150,
        500,
        1500,
        5000,
        15000,
        45000,
        150000,
        450000
    };

    [SerializeField]
    public readonly List<int> earnedMoneyTable = new()
    {
        30,
        100,
        300,
        1000,
        4000,
        15000,
        50000,
        150000,
        5000000,
        15000000,
        500000000
    };

    [SerializeField]
    public readonly List<int> healTable = new()
    {
        2,
        10,
        30,
        100,
        120,
        400,
        1200,
        3800,
        12000,
        40000,
        150000
    };

    [SerializeField]
    public readonly List<int> HPTable = new()
    {
        100,
        300,
        1500,
        4500,
        15000,
        45000,
        150000,
        450000,
        1500000,
        5000000,
        15000000
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
