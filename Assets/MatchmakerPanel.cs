using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakerPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private Button createButton; 
    [SerializeField] private Button joinButton;
    [SerializeField] private Button joinRandomRoomButton;

    [SerializeField] private RectTransform content;

    //private TypedLobby lobby = new TypedLobby("lobby", LobbyType.Default);

    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
        Init();
    }

    private void Init()
    {
        createButton.onClick.AddListener(CreateRoom);
        //joinButton.onClick.AddListener(JoinRoom);
        joinRandomRoomButton.onClick.AddListener(JoinRandomRoom);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        RoomInfo room = PhotonNetwork.CurrentRoom;
        
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom(LobbyRoom room)
    {
        if (room == null) return;
        PhotonNetwork.JoinRoom(room.RoomName);
    }

    public override void OnJoinedRoom()
    {
        Log("Joinred the room");
        //PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    private void Log(string message)
    {
        Debug.Log(message);
        logText.text += "\n";
        logText.text += message;
    }
}
