using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private MatchmakerPanel matchmakerPrefab;
    private MatchmakerPanel matchmaker;

    void Start()
    {
        CreateMatchmakerPanel();
    }

    private void CreateMatchmakerPanel()
    {
        matchmaker = Instantiate(matchmakerPrefab);
    }
}
