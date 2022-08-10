using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    public void OnChangeBGM(float value)
    {
        controller.SetBGM(ConvertToDB(bgm.value));
        bgm_text.text = ((int)(bgm.value * 100)).ToString(); 
    }

    public void OnChangeSE(float value)
    {
        controller.SetSE(ConvertToDB(se.value));
        se_text.text = ((int)(se.value * 100)).ToString();
        se_source.Play();
    }

    private float ConvertToDB(float value)
    {
        return Mathf.Lerp(-50, 0, value);
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (nextScene.buildIndex == 0)
        {
            var root = FindObjectsOfType<Image>(true).Where(obj => obj.name == "SoundSetting").First();
            bgm = root.GetComponentsInChildren<Slider>().Where(obj => obj.name == "BGMVolume").First();
            bgm.onValueChanged.AddListener(OnChangeBGM);
            bgm_text = root.GetComponentsInChildren<TextMeshProUGUI>().Where(obj => obj.name == "BGMValue").First();
            se = root.GetComponentsInChildren<Slider>().Where(obj => obj.name == "SEVolume").First();
            se.onValueChanged.AddListener(OnChangeSE);
            se_text = root.GetComponentsInChildren<TextMeshProUGUI>().Where(obj => obj.name == "SEValue").First();
        }
    }
}
