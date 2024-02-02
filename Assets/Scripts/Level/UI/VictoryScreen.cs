using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _nameUI;
    [SerializeField] TMP_Text _pointCountUI;
    [SerializeField] GameObject _panel;
    
    public void ShowPanel(string playerName, int pointCount)
    {
        _nameUI.text = $"������� �����\n{playerName}";
        _pointCountUI.text = $"����: {pointCount}";

        _panel.SetActive(true);
    }
}
