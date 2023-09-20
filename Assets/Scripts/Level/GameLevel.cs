using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private PlayerSpawner spawner;

    void Start()
    {
        spawner.SpawnPlayer();
    }

  
}
