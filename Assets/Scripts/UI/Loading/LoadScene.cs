using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;
    AsyncOperation loadingOperation;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
    }

    private void Update()
    {
        float value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        progressBar.value = value;
        progressText.text = "Loading: " + (value * 100) + "%";
    }
}
