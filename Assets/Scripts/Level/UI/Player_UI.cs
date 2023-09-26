using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameUI;
    [SerializeField] private HealthBar heathBar;

    private PlayerController _playerController;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
        PlaceAnPanel();
        heathBar.Init(_playerController.MaxHealth);
        _playerController.onHealthChanged += heathBar.UpdateIndicator;
        _playerController.onCharacterDied += Detach;
    }

    private void PlaceAnPanel()
    {
        
    }

    private void Detach()
    {
        _playerController.onHealthChanged -= heathBar.UpdateIndicator;
        _playerController.onCharacterDied -= Detach;

        gameObject.SetActive(false);
    }
}
