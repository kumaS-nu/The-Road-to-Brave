using Cysharp.Threading.Tasks;

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreLogger : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private TextMeshProUGUI mesh;

    [SerializeField]
    private Image background; 

    public static List<(int, TimeSpan)> m_log;

    private int m_nextCheckPoint = 100;

    private void Awake()
    {
        m_log = new();
    }

    private void Update()
    {
        if (m_nextCheckPoint <= StageState.Instance.Money)
        {
            m_log.Add((m_nextCheckPoint, timer.ElapsedTime));
            Display(m_nextCheckPoint, m_log.Last().Item2.ToString("g")).Forget();
            m_nextCheckPoint *= 100;
        }
    }

    private async UniTask Display(int checkPoint, string time)
    {
        background.color = Color.white;
        mesh.color = Color.white;
        mesh.text = "Earn " + checkPoint + " : " + time;
        background.gameObject.SetActive(true);
        mesh.gameObject.SetActive(true);
        await UniTask.Delay(3000);
        float spend = 0;
        while(spend < 5)
        {
            await UniTask.Yield();
            spend += Time.deltaTime;
            background.color = new Color(1, 1, 1, (5.0f - spend) / 5);
            mesh.color = new Color(1, 1, 1, (5.0f - spend) / 5);
        }
        background.gameObject.SetActive(false);
        mesh.gameObject.SetActive(false);
    }
}
