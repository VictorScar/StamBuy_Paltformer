using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private PhotonView pv;

    // private Player _player;
    private PlayerController _pawn;

    public PhotonView PV { get => pv; }
    public PlayerController Pawn { get => _pawn; }

    private void Start()
    {
        GameLevel.Instance.AddPlayerNetworking(this);
        Debug.Log("PlayerNetwork!");
    }

    public void Init(PlayerController pawn)
    {
        _pawn = pawn;
    }
}
