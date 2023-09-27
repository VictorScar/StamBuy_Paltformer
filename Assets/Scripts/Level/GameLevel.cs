using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private PlayerSpawner spawner;
    [SerializeField] private GameScreenUI gameScreenPrefab;
    [SerializeField] private Player_UI playerUIPrefab;
    [SerializeField] private PlayerNetwork playerNetwork;
    [SerializeField] private PlayerController pawnPrefab;

    [SerializeField] private PhotonView pv;

    [SerializeField] private float offset = 2f;

    private PlayerController currentPlayer;

    public static GameLevel Instance;

    private List<PlayerNetwork> players = new List<PlayerNetwork>();
    private List<PlayerController> playerPawns = new List<PlayerController>();
    public PlayerController CurrentPlayer { get => currentPlayer; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(LevelInitialization());
    }


    public void AddPlayerNetworking(PlayerNetwork playerNetwork)
    {
        players.Add(playerNetwork);
    }

    public void AddPlayerPawn(PlayerController playerController)
    {
        playerPawns.Add(playerController);
    }



    private void CreatePawn()
    {
        spawner.SpawnPlayer(playerNetwork);
    }

    private IEnumerator LevelInitialization()
    {
        CreatePlayer();
        CreatePawn();

        yield return new WaitForSeconds(1f);

        PlayersInitialization();
    }

    private void CreatePlayer()
    {
        var instance = PhotonNetwork.Instantiate(playerNetwork.name, Vector3.zero, Quaternion.identity);
        playerNetwork = instance.GetComponent<PlayerNetwork>();
    }

    private void PlayersInitialization()
    {
        foreach (var player in players)
        {
            foreach (var pawn in playerPawns)
            {
                if (player.PV.Owner == pawn.PV.Owner)
                {
                    player.Init(pawn);
                    break;
                }
            }
        }

        CreateUIScreen();
        CreatePawnsUI();
    }

    private void CreatePawnsUI()
    {
        foreach (var player in players)
        {
            var pawn = player.Pawn;
            Vector3 uiPos = pawn.transform.position + new Vector3(0, offset, 0);
            var pawnUI = Instantiate(playerUIPrefab, uiPos, Quaternion.identity, pawn.transform);
            pawnUI.Init(pawn);
        }
    }

    private void CreateUIScreen()
    {
        var currentPlayer = players.First(p => p.PV.Owner == PhotonNetwork.LocalPlayer);

        var gameScreen = Instantiate(gameScreenPrefab);
        gameScreen.Init(currentPlayer.Pawn);
        RunTimeLogger.AttachUIText(gameScreen.LogText);
    }
}
