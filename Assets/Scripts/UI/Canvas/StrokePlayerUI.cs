using System.Collections;
using System.Collections.Generic;
using GamePlay;
using TMPro;
using UnityEngine;

public class StrokePlayerUI : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private GameManager _gameManager;

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        _textMeshProUGUI.text = "Player " + _gameManager.PlayerIndex;
    }
}
