using Cysharp.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;
using System.Linq;
using System.Threading;

/// <summary>
/// 敵のポップを制御．
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemysPrefab;

    [SerializeField]
    private List<Transform> spornPoints;

    [SerializeField]
    private float baseRepopInterval = 5;

    private int m_beforeEnemyStrength;
    private List<GameObject> m_popedEnemys = new List<GameObject>();
    private Stack<GameObject> m_stackedEnemy = new Stack<GameObject>();
    private CancellationTokenSource m_cancelSource = new CancellationTokenSource();

    private List<int> maxCount = new List<int>()
    {
        6,
        13,
        18,
        23,
        27,
        31,
        35,
        38,
        42,
        45,
        47
    };

    private void Start()
    {
        m_beforeEnemyStrength = StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength];
        _ = GenerateEnemy(m_cancelSource.Token);
    }

    private async UniTask GenerateEnemy(CancellationToken token)
    {
        while (true)
        {
            token.ThrowIfCancellationRequested();
            var baseSponeInterval = baseRepopInterval / (StageState.Instance.EnhancementLevel[EnhancementContent.EnemyEncount] + 1);
            var sponeInterval = Normal.Sample(baseSponeInterval, baseRepopInterval / 5);
            sponeInterval = sponeInterval < 0.01 ? 0.01 : sponeInterval;
            await UniTask.Delay(TimeSpan.FromSeconds(sponeInterval));
            var point = DiscreteUniform.Sample(0, spornPoints.Count - 1);
            PopEnemy(spornPoints[point].position);
;        }
    }

    private void Update()
    {
        if (m_beforeEnemyStrength != StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength])
        {
            foreach(var enemy in m_popedEnemys)
            {
                Destroy(enemy);
            }
            m_popedEnemys.Clear();

            foreach(var enemy in m_stackedEnemy)
            {
                Destroy(enemy);
            }
            m_stackedEnemy.Clear();
        }
        m_beforeEnemyStrength = StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength];
    }

    public void KillEnemy(GameObject enemy)
    {
        m_popedEnemys.Remove(enemy);
        m_stackedEnemy.Push(enemy);
        enemy.SetActive(false);
    }

    private void PopEnemy(Vector3 position)
    {
        if(m_popedEnemys.Count > maxCount[StageState.Instance.EnhancementLevel[EnhancementContent.EnemyEncount]])
        {
            return;
        }

        float scale = 1.0f + Mathf.InverseLerp(0, 10, StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength]);
        if (m_stackedEnemy.Any())
        {
            var enemy = m_stackedEnemy.Pop();
            m_popedEnemys.Add(enemy);
            enemy.transform.position = position;
            enemy.transform.localScale = new Vector3(scale, scale, scale);
            enemy.GetComponent<EnemyMove>().Init();
            enemy.SetActive(true);
            return;
        }
        else
        {
            var enemy = Instantiate(enemysPrefab[StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength]], position, Quaternion.identity);
            enemy.transform.localScale = new Vector3(scale, scale, scale);
            m_popedEnemys.Add(enemy);
            return;
        }
    }

    private void OnDestroy()
    {
        m_cancelSource.Cancel();
    }
}
