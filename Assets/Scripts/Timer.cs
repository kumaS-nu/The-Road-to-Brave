using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 経過時間計測＆表示用スクリプト．
/// </summary>
public class Timer : MonoBehaviour
{
    public TimeSpan ElapsedTime { get => DateTime.Now - m_startTime; }

    [SerializeField]
    private TextMesh textMesh;

    private DateTime m_startTime;

    void Start()
    {
        m_startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (textMesh != null)
        {
            textMesh.text = ToString();
        }
    }

    public override string ToString()
    {
        return ElapsedTime.ToString("g");
    }
}
