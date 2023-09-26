using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<PlayerController> playerPrefabs = new List<PlayerController>();
    [SerializeField] private PhotonView pv;


    public PlayerController SpawnPlayer(PlayerNetwork playerNetwork)
    {
        SpawnPoint spawnPoint = GetFreeSpawnPoint();

        if (spawnPoint == null) return null;

        var playerObject = PhotonNetwork.Instantiate(playerPrefabs[0].name, spawnPoint.Position, Quaternion.identity);
        PlayerController player = playerObject.GetComponent<PlayerController>();

        return player;
    }

    private SpawnPoint GetFreeSpawnPoint()
    {
        return spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
    }
}
