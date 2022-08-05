using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLogger : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    public static Dictionary<int, TimeSpan> m_log;

    private int m_nextCheckPoint = 100;

    private void Awake()
    {
        m_log = new();
    }

    private void Update()
    {
        if (m_nextCheckPoint <= StageState.Instance.Money)
        {
            m_log[m_nextCheckPoint] = timer.ElapsedTime;
            m_nextCheckPoint *= 10;
        }
    }
}
