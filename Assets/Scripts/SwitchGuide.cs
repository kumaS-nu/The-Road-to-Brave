using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGuide : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> guideObject;

    [SerializeField]
    private List<GameObject> titleObject;


    public void OnGuide()
    {
        foreach(var guide in guideObject)
        {
            guide.SetActive(true);
        }

        foreach(var title in titleObject)
        {
            title.SetActive(false);
        }
    }

    public void OnBack()
    {
        foreach(var guide in guideObject)
        {
            guide.SetActive(false);
        }

        foreach(var title in titleObject)
        {
            title.SetActive(true);
        }
    }
}
