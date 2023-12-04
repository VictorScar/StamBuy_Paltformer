using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Text counterUI;
    [SerializeField] private TMP_Text logText;

    [SerializeField] private VictoryScreen _victoryScreen;

    private PlayerNetwork _player;
    private PlayerController _playerController;
    private PointCounter _pointCounter;
    private GameStateController _gameController;

    private string _playerName;

    public TMP_Text LogText { get => logText; }

    public void Init(PlayerNetwork player, GameStateController gameController)
    {
        _player = player;
        _playerController = _player.Pawn;
        _playerName = _player.PV.Owner.NickName;
        _pointCounter =_playerController.PointCounter;
        _gameController = gameController;

        _playerController.PointCounter.onPointCountChanged += UpdateCountText;
        _gameController.onPlayerWon += ShowVictoryPanel;

        exitButton.onClick.AddListener(ExitTheGame);
    }

    private void ShowVictoryPanel()
    {
        _victoryScreen.ShowPanel(_playerName, _pointCounter.Points);
    }

    private void UpdateCountText(int value)
    {
        counterUI.text = value.ToString();
    }

    private void ExitTheGame()
    {
        Debug.Log("LeaveBut!");
        _player.PlayerLeave();
    }


}
