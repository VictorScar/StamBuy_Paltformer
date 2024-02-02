using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private GameLevel _level;

    public event Action onPlayerWon;

    public void Init(GameLevel level)
    {
        _level = level;
        _level.onPlayerCountChanged += CheckPlayerCount;
    }
     

    private void CheckPlayerCount(int playerCount)
    {
        if (playerCount < 2)
        {
            onPlayerWon?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _level.onPlayerCountChanged -= CheckPlayerCount;
    }
}