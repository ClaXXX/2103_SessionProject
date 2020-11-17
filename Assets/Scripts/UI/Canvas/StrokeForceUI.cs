using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrokeForceUI : MonoBehaviour
{
    private StrokeManager _strokeManager;
    private Image _image;

    void Start()
    {
        if (!(_strokeManager = FindObjectOfType<StrokeManager>())
            || !(_image = GetComponent<Image>()))
        {
            Debug.LogError("No Stroke Manager found", this);
        }
    }

    void Update()
    {
        _image.fillAmount = _strokeManager.StrokeForcePercentage;
    }
}
