using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour
{
    public string targetScene;

    public void LoadScene()
    {
        LoadingData.sceneToLoad = targetScene;
        SceneManager.LoadScene("Loading");
    }
}
