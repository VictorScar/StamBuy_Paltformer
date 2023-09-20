using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<PlayerController> playerPrefabs = new List<PlayerController>();


    public void SpawnPlayer()
    {
        SpawnPoint spawnPoint = GetFreeSpawnPoint();

        if (spawnPoint == null) return;

        PhotonNetwork.Instantiate(playerPrefabs[0].name, spawnPoint.Position, Quaternion.identity);
    }

    private SpawnPoint GetFreeSpawnPoint()
    {    

        return spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber-1];

       
    }
}
