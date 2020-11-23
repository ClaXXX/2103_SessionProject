using System.Collections;
using System.Collections.Generic;
using Gameplay.Stroke_Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StrokeForceUI : MonoBehaviour
{
    public StrokeManager strokeManager;
    private Image _image;

    void Start()
    {
        if (!(_image = GetComponent<Image>()))
        {
            Debug.LogError("No Stroke Manager found", this);
        }
    }

    void Update()
    {
        _image.fillAmount = strokeManager.StrokeForcePercentage;
    }
}
