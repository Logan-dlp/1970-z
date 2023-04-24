using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombies : MonoBehaviour
{
    private GameManager gameManager;
    
    public ZombiesData DataSources;

    private float posPlusProche;
    private GameObject Player;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 1;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        Instantiate(DataSources.Skin, transform);
        gameManager.Debug();
    }

    private void Update()
    {
        LePlusProche();
    }

    /*
     * Trouver le joueur le plus proche (en boucle) ✅
     * le suivre
     * Arrivé a une certaine distance Infliger des dégats à la personne suivit (et faire une autre animation que de marcher)
     */
    void LePlusProche()
    {
        foreach (GameObject _player in gameManager.Player)
        {
            float _posP = Mathf.Abs(_player.transform.position.x + _player.transform.position.y);
            float _pos = Mathf.Abs(transform.position.x + transform.position.y);
            if (posPlusProche == null || _pos - _posP < posPlusProche)
            {
                posPlusProche = _pos + _posP;
                Player = _player;
            }
        }
    }
}
