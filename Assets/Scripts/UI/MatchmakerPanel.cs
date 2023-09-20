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

    private LobbyManger lobbyManger;


    //public override void OnConnectedToMaster()
    //{
    //    Log("Connected to Master");
    //    Init();
    //}

    public void Init(LobbyManger lobbyManger)
    {
        this.lobbyManger = lobbyManger;

        createButton.onClick.AddListener(CreateRoom);
        //joinButton.onClick.AddListener(JoinRoom);
        joinRandomRoomButton.onClick.AddListener(JoinRandomRoom);
    }

    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(0, 50);
        PhotonNetwork.CreateRoom("roomName", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        RoomInfo room = PhotonNetwork.CurrentRoom;
        Log("Create the room");


    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //public void JoinRoom(LobbyRoom room)
    //{
    //    if (room == null) return;
    //    PhotonNetwork.JoinRoom(room.RoomName);

    //}

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        // Log(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Log("Player "+ PhotonNetwork.NickName + " Joined the room " + PhotonNetwork.CurrentRoom.Name);
        lobbyManger.ShowRoomPanel();
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

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        //Debug.Log(PhotonNetwork.CountOfRooms);
        Log(PhotonNetwork.CountOfRooms.ToString());
        foreach (var room in roomList)
        {
            Log(room.Name);
        }
    }

}
