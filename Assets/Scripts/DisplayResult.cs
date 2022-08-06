using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayResult : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> score;

    [SerializeField]
    private List<TextMeshProUGUI> time;

    // Start is called before the first frame update
    void Start()
    {
        var count = ScoreLogger.m_log.Count;
        count = count > score.Count ? score.Count : count;
        for (int i = 0; i < count; i++)
        {
            score[i].text = ScoreLogger.m_log[i].Item1.ToString();
            time[i].text = ScoreLogger.m_log[i].Item2.ToString("g");
        }
    }
}
