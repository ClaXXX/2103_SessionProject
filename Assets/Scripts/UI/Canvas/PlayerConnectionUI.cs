using GamePlay;
using TMPro;
using UnityEngine;

public class PlayerConnectionUI : MonoBehaviour
{
    private GameManager gameManager;
    private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        _textMeshProUGUI.text = gameManager.PlayerNbr +  " Players connected";
    }
}
