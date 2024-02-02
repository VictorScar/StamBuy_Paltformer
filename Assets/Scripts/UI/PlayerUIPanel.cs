using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameUI;
    private string nameText;

    public void SetName(string playerName)
    {
        nameUI.text = playerName;
    }
}
