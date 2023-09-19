using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbyManger : MonoBehaviourPunCallbacks
{
    [SerializeField] private MatchmakerPanel matchmakerPrefab;
    private MatchmakerPanel matchmaker;

    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        
    }

    private void CreateMatchmakerPanel()
    {
        matchmaker = Instantiate(matchmakerPrefab);
        matchmaker.Init();
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
        CreateMatchmakerPanel();
    }
}
