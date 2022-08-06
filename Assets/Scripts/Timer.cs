using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 経過時間計測＆表示用スクリプト．
/// </summary>
public class Timer : MonoBehaviour
{
    public TimeSpan ElapsedTime { get => DateTime.Now - m_startTime; }

    [SerializeField]
    private TextMeshProUGUI textMesh;

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
        return ElapsedTime.ToString("hh\\:mm\\:ss\\.f");
    }
}
