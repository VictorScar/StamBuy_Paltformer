using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private PhotonView pv;

    private PlayerController _pawn;

    public PhotonView PV { get => pv; }
    public PlayerController Pawn { get => _pawn; }

    private void Start()
    {
        GameLevel.Instance.AddPlayerNetworking(this);
    }

    public void Init(PlayerController pawn)
    {
        _pawn = pawn;
        _pawn.onCharacterDied += PlayerLeave;
    }

    private void PlayerLeave()
    {
        Debug.Log("Dis");
        if (pv.IsMine)
        {
            PhotonNetwork.Disconnect();
        }
    }
}
