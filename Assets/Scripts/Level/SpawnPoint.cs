using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool isBusy = false;
    public Vector2 Position => transform.position;

    public bool IsBusy
    {
        get => isBusy;
        set
        {
            isBusy = value;

        }
    }
}
