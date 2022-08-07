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
            var portion = Instantiate(portions[StageState.Instance.EnhancementLevel[EnhancementContent.Heal]], p.position, Quaternion.identity);
            float scale = 0.4f + Mathf.InverseLerp(0, 10, StageState.Instance.EnhancementLevel[EnhancementContent.Heal]) * 0.3f;
            portion.transform.localScale = new Vector3(scale, scale, scale);

        }
    }
}
