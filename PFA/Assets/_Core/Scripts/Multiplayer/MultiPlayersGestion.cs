using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPlayersGestion : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerInputManager inputManager;
    public int NbPlayer = 0;
    private Camera player1cam;
    private Camera player2cam;

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
        NbPlayer++;
        gameManager.Player.Add(_obj.gameObject);
        if (NbPlayer == 1)
        {
            _obj.gameObject.layer = LayerMask.NameToLayer("Player 1");
            _obj.transform.position = gameManager.SpawnPlayers[NbPlayer - 1].position;
            Debug.Log(_obj.transform.position);
            player1cam = _obj.GetComponentInChildren<Camera>();
        }else if (NbPlayer == 2)
        {
            _obj.gameObject.layer = LayerMask.NameToLayer("Player 2");
            player2cam = _obj.GetComponentInChildren<Camera>();
            player1cam.rect = new Rect(0, 0.5f, 1, 1);
            player2cam.rect = new Rect(0, -0.5f, 1, 1);
        }
    }
}
