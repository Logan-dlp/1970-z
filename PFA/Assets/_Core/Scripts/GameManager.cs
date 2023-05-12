using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager)), RequireComponent(typeof(MultiPlayersGestion))]
public class GameManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Player;
    public Transform[] SpawnPlayers;
    public Transform[] SpawnZombies;
    public GameObject[] Zombies;
    [HideInInspector] public int NbZombies = 0;
    [HideInInspector] public int NbManches = 0;
    
    private MultiPlayersGestion multiPlayersGestion;

    private void Start()
    {
        multiPlayersGestion = GetComponent<MultiPlayersGestion>();
    }

    private void Update()
    {
        SpawnZombie();
    }

    public bool InGame()
    {
        return multiPlayersGestion.NbPlayer >= 1;
    }

    public void StopGame()
    {
        if (InGame() && Player.Count <= 0)
        {
            // changement de scènes à mettre a la mort des players
            Debug.Log("GameOver !");
        }
    }

    void SpawnZombie()
    {
        if (InGame() && NbZombies <= 0)
        {
            NbManches++;
            foreach (Transform _spawnZombie in SpawnZombies)
            {
                for (int i = 0; i < NbManches; i++)
                {
                    Instantiate(Zombies[0], _spawnZombie);
                    NbZombies++;
                }
            }
        }
    }
}
