using Cysharp.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;

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

    private void Start()
    {
        m_beforeEnemyStrength = StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength];
        _ = GenerateEnemy();
    }

    private async UniTask GenerateEnemy()
    {
        while (true)
        {
            var baseSponeInterval = baseRepopInterval / (StageState.Instance.EnhancementLevel[EnhancementContent.EnemyEncount] + 1);
            var sponeInterval = Normal.Sample(baseSponeInterval, baseRepopInterval / 5);
            sponeInterval = sponeInterval < 0.01 ? 0.01 : sponeInterval;
            await UniTask.Delay(TimeSpan.FromSeconds(sponeInterval));
            var point = DiscreteUniform.Sample(0, spornPoints.Count - 1);
            var enemy = Instantiate(enemysPrefab[StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength]], spornPoints[point].position, Quaternion.identity);
            float scale = 1.0f + Mathf.InverseLerp(0, 10, StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength]);
            enemy.transform.localScale = new Vector3(scale,scale,scale);
            m_popedEnemys.Add(enemy);
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
        }
        m_beforeEnemyStrength = StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength];
    }
}
