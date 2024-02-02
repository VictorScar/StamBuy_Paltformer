using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbyManger : MonoBehaviourPunCallbacks
{
    [SerializeField] private MatchmakerPanel matchmakerPrefab;
    [SerializeField] private RoomPanel roomPanelPrefab;
    private MatchmakerPanel matchmaker;
    private RoomPanel roomPanel;

    void Start()
    {
        CreateUIElements();

        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join to lobby");
        base.OnJoinedLobby();
        //CreateMatchmakerPanel();
        ShowMatchmakerPanel();
    }

    public void ShowMatchmakerPanel()
    {
        matchmaker.gameObject.SetActive(true);
    }

    public void ShowRoomPanel()
    {
        roomPanel.Init();
        matchmaker.gameObject.SetActive(false);
        roomPanel.gameObject.SetActive(true);
    }

    private void CreateUIElements()
    {
        CreateMatchmakerPanel();
        roomPanel = Instantiate(roomPanelPrefab);
        roomPanel.gameObject.SetActive(false);
    }

    private void CreateMatchmakerPanel()
    {
        matchmaker = Instantiate(matchmakerPrefab);
        matchmaker.Init(this);
        matchmaker.gameObject.SetActive(false);
    }
}
