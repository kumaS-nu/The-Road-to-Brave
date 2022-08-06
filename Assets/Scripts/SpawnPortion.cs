using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーションをスポーンさせる．
/// </summary>
public class SpawnPortion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> portions;

    [SerializeField]
    private List<Transform> spornPoints;

    /// <summary>
    /// ポーションをスポーンさせる．
    /// </summary>
    public void Spawn()
    {
        foreach(var p in spornPoints)
        {
            Instantiate(portions[StageState.Instance.EnhancementLevel[EnhancementContent.Heal]], p.position, Quaternion.identity);
        }
    }
}
