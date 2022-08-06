using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[�V�������X�|�[��������D
/// </summary>
public class SpawnPortion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> portions;

    [SerializeField]
    private List<Transform> spornPoints;

    /// <summary>
    /// �|�[�V�������X�|�[��������D
    /// </summary>
    public void Spawn()
    {
        foreach(var p in spornPoints)
        {
            Instantiate(portions[StageState.Instance.EnhancementLevel[EnhancementContent.Heal]], p.position, Quaternion.identity);
        }
    }
}
