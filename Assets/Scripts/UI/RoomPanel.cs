using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] private Button startButton;
    //[SerializeField] private Button endButton;
    [SerializeField] private RectTransform playerListUI;

    [SerializeField] private PlayerUIPanel playerUIPanelPrefab;

    private List<PlayerUIPanel> playerPanels = new List<PlayerUIPanel>();
    private Coroutine coroutine;

    private Room room;

    private float timeToUpdate = 0.5f;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }
    public void Init()
    {
        room = PhotonNetwork.CurrentRoom;
        
    }

    private IEnumerator UpdatePlayerList()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToUpdate);
            ShowPlayerList();
            CheckPlayerCount();
        }

    }

    private void CheckPlayerCount()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            startButton.interactable = true;
        }
    }

    private void StartGame()
    {
        Debug.Log("Start Game!");
        SceneManager.LoadScene("Game");
    }

    private void ShowPlayerList()
    {
        var players = room?.Players?.Values;

        ClearPlayerPanelsList();

        if (players == null) { return; }

        foreach (var p in players)
        {
            var panel = Instantiate(playerUIPanelPrefab, playerListUI);
            panel.SetName(p.NickName);
            playerPanels.Add(panel);

        }
    }

    private void ClearPlayerPanelsList()
    {
        foreach (var panel in playerPanels)
        {
            Destroy(panel.gameObject);
        }

        playerPanels.Clear();
    }

    private void OnEnable()
    {
        ShowPlayerList();
        coroutine = StartCoroutine(UpdatePlayerList());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
      
    }
}
