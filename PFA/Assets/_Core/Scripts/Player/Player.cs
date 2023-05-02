using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(PlayerController)), RequireComponent(typeof(PlayerInteractable)), RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private MultiPlayersGestion multiPlayersGestion;
    public float PlayerLife = 100;
    public int Coin = 0;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Death();
    }

    void Death()
    {
        if (PlayerLife <= 0)
        {
            gameManager.Player.Remove(gameObject);
            gameManager.StopGame();
            Destroy(gameObject);
        }
    }
}