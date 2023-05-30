using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager)), RequireComponent(typeof(MultiPlayersGestion)), RequireComponent(typeof(SceneManager))]
public class GameManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> Player;
    public Transform[] SpawnPlayers;
    public Transform[] SpawnZombies;
    public GameObject[] Zombies;
    public Transform[] SpawnBossZombies;
    public GameObject[] BossZombies;
    public GameObject DoorFinal;
    public int NbLeverEnd;
    public string SceneGameOver;
    public string SceneWin;
    [HideInInspector] public int NbZombies = 0;
    [HideInInspector] public int NbBoss = 0;
    [HideInInspector] public int NbManches = 0;
    [HideInInspector] public int Nblever = 0;
    
    private MultiPlayersGestion multiPlayersGestion;
    private SceneManager sceneManager;

    private void Start()
    {
        multiPlayersGestion = GetComponent<MultiPlayersGestion>();
        sceneManager = GetComponent<SceneManager>();
    }

    private void Update()
    {
        SpawnZombie();
        SpawnBoss();
    }

    public bool InGame()
    {
        return multiPlayersGestion.NbPlayer >= 1;
    }

    public void StopGame()
    {
        if (InGame() && Player.Count <= 0)
        {
            sceneManager.Load(SceneGameOver);
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

    void SpawnBoss()
    {
        if (Nblever == NbLeverEnd)
        {
            Destroy(DoorFinal);
            
            for (int i = 0; i < SpawnBossZombies.Length; i++)
            {
                for (int j = 0; j < BossZombies.Length; j++)
                {
                    Instantiate(BossZombies[j], SpawnBossZombies[i]);
                    NbBoss++;
                }
            }
        }
    }
}
