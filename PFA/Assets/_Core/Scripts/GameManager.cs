using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager)), RequireComponent(typeof(MultiPlayersGestion))]
public class GameManager : MonoBehaviour
{
    public List<GameObject> Player;
    public Transform[] SpawnPlayers;
    public GameObject[] Zombies;
    public int NbZombies = 0;
    public int NbManches = 0;
    private MultiPlayersGestion multiPlayersGestion;
    private Transform zombiesParents;

    private void Start()
    {
        multiPlayersGestion = GetComponent<MultiPlayersGestion>();
        zombiesParents = GameObject.Find("Spawn Zombies").transform;
    }

    private void Update()
    {
        SpawnZombies(zombiesParents);
    }

    public bool InGame()
    {
        return multiPlayersGestion.NbPlayer >= 1;
    }

    void SpawnZombies(Transform _parent)
    {
        if (InGame() && ParentZombies())
        {
            NbManches++;
            for (int i = 0; i < NbManches; i++)
            {
                Instantiate(Zombies[0], _parent);
                NbZombies++;
            }
        }
    }

    bool ParentZombies()
    {
        return NbZombies > 0;
    }
}
