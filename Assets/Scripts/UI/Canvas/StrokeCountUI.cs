using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrokeCountUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private StrokeManager _strokeManager;

    void Start()
    {

        if (!(_strokeManager = FindObjectOfType<StrokeManager>())
            || !(_text = GetComponent<TextMeshProUGUI>()))
        {
            Debug.LogError("An error occured, checkout if a stroke Manager and a score text has been create", gameObject);
        }
    }

    void Update()
    {
        if (_text)
        {
            _text.text = "Stroke: " + _strokeManager.StrokeCount;
        }
    }
}
