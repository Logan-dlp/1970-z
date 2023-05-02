using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPlayersGestion : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerInputManager inputManager;
    private int nbPlayer = 0;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        inputManager.onPlayerJoined += OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput _obj)
    {
        Debug.Log("Player Joined !");
        nbPlayer++;
        _obj.gameObject.transform.position = gameManager.SpawnPlayers[nbPlayer - 1].position;
        gameManager.Player.Add(_obj.gameObject);
        // _obj.gameObject.layer = LayerMask.NameToLayer("Player " + nbPlayer); (à tester)
        if (nbPlayer == 1)
        {
            _obj.gameObject.layer = LayerMask.NameToLayer("Player 1");
        }else if (nbPlayer == 2)
        {
            _obj.gameObject.layer = LayerMask.NameToLayer("Player 2");
        }
    }
}
