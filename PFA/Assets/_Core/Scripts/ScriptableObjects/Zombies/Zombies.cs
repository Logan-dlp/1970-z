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
    
    private GameObject Player;
    private NavMeshAgent agent;

    private float life;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 1;
        agent.speed = DataSources.Speed;
        agent.angularSpeed = 100;
        agent.acceleration = 100;
        agent.stoppingDistance = DataSources.StopDistance;

        life = DataSources.Life;
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        Instantiate(DataSources.Skin, transform);
    }

    private void Update()
    {
        GoToPlayer();
        StartCoroutine("DegatTime");
        Death();
    }

    /*
     * Arrivé a une certaine distance Infliger des dégats à la personne suivit (et faire une autre animation que de marcher)
     */
    void GoToPlayer()
    {
        if (Player)
        {
            foreach (GameObject _player in gameManager.Player)
            {
                if (Vector3.Distance(_player.transform.position, transform.position) < Vector3.Distance(Player.transform.position, transform.position))
                {
                    Player = _player;
                }
            }
        }
        else
        {
            foreach (GameObject _player in gameManager.Player)
            {
                Player = _player;
            }
        }
        
        agent.SetDestination(Player.transform.position);
    }

    void Death()
    {
        if (life <= 0)
        {
            gameManager.NbZombies--;
            Destroy(gameObject);
        }
    }

    IEnumerator DegatTime()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= agent.stoppingDistance)
        {
            yield return new WaitForSeconds(DataSources.DegatTime);
            float _playerLife = Player.GetComponent<Player>().PlayerLife;
            _playerLife -= DataSources.Degat;
            StopCoroutine("DegatTime");
        }
    }
}
