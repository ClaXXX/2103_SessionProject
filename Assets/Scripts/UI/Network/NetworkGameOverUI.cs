using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class NetworkGameOverUI : NetworkBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetWinner(string winner)
    {
        _textMeshProUGUI.text = winner + " a gagné la partie";
    }
}
