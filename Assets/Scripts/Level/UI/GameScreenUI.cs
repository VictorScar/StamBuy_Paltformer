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

    private PlayerController _playerController;

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
