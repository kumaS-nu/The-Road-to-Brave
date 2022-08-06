using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSpecifiedScene : MonoBehaviour
{
    [SerializeField]
    private int sceneNo;

    public void OnClicked()
    {
        SceneManager.LoadScene(sceneNo);
    }
}
