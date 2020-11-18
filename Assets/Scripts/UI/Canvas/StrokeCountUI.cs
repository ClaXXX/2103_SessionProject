using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StrokeCountUI : MonoBehaviour
{
    public StrokeManager strokeManager;
    private TextMeshProUGUI _text;

    void Start()
    {
        if (!(_text = GetComponent<TextMeshProUGUI>()))
        {
            Debug.LogError("An error occured, checkout if a stroke Manager and a score text has been create", gameObject);
        }
    }

    void Update()
    {
        if (_text)
        {
            _text.text = "Stroke: " + strokeManager.StrokeCount;
        }
    }
}
