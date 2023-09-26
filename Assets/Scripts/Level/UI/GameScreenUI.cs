using Photon.Pun;
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

    private PlayerController _playerController;

    public TMP_Text LogText { get => logText; }

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.PointCounter.onPointCountChanged += UpdateCountText;

        exitButton.onClick.AddListener(ExitTheGame);
    }

    private void UpdateCountText(int value)
    {
        counterUI.text = value.ToString();
    }

    private void ExitTheGame()
    {
        PhotonNetwork.Disconnect();
    }


}
