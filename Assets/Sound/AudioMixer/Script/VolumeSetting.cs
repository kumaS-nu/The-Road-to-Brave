using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private Slider bgm;

    [SerializeField]
    private TextMeshProUGUI bgm_text;

    [SerializeField]
    private Slider se;

    [SerializeField]
    private TextMeshProUGUI se_text;

    [SerializeField]
    private AudioController controller;

    [SerializeField]
    private AudioSource se_source;

    public void OnChangeBGM()
    {
        controller.SetBGM(ConvertToDB(bgm.value));
        bgm_text.text = ((int)(bgm.value * 100)).ToString(); 
    }

    public void OnChangeSE()
    {
        controller.SetSE(ConvertToDB(se.value));
        se_text.text = ((int)(se.value * 100)).ToString();
        se_source.Play();
    }

    private float ConvertToDB(float value)
    {
        return Mathf.Lerp(-50, 0, value);
    }
}
