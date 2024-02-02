using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView pv;

    private PlayerController _pawn;

    public PhotonView PV { get => pv; }
    public PlayerController Pawn { get => _pawn; }

    public event Action <PlayerNetwork> onPlayerHasExited;

    private void Start()
    {
        GameLevel.Instance.AddPlayerNetworking(this);
    }

    public void Init(PlayerController pawn)
    {
        _pawn = pawn;
        _pawn.onCharacterDied += PlayerLeave;
    }

    public void PlayerLeave()
    {
       Debug.Log("Dis");

        if (pv.IsMine)
        {
            _pawn.onCharacterDied -= PlayerLeave;
            PhotonNetwork.LeaveRoom(this);
            SceneManager.LoadScene(0);
        }

        onPlayerHasExited?.Invoke(this);
       
    }

}
