using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    public float PlayerLife = 100;

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
            Destroy(gameObject);
        }
    }
}
