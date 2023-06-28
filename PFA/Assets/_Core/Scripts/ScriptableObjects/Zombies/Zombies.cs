using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombies : MonoBehaviour
{
    public ZombiesData DataSources;
    public float Life;
    
    private GameManager gameManager;
    private GameObject Player;
    private NavMeshAgent agent;

    private Animator ZombieDebout;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 0;
        agent.speed = DataSources.Speed;
        agent.angularSpeed = 100;
        agent.acceleration = 100;
        agent.stoppingDistance = DataSources.StopDistance;
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ZombieDebout = GetComponent<Animator>();
    }

    private void Start()
    {
        Life = DataSources.Life;
    }

    private void Update()
    {
        GoToPlayer();
        StartCoroutine("DegatTime");
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
                    ZombieDebout.SetBool("Run", true);
                    ZombieDebout.SetBool("Attack", false);
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

    public void Death(Player _player)
    {
        if (Life <= 0)
        {
            gameManager.NbZombies--;
            _player.Coin += DataSources.Awards;
            if (DataSources.IsBoss)
            {
                gameManager.NbBoss--;
            }
            // annimation de mort...
            Destroy(gameObject);
        }
    }

    IEnumerator DegatTime()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= 3)
        {
            ZombieDebout.SetBool("Attack", true);
            ZombieDebout.SetBool("Run", false);
            yield return new WaitForSeconds(DataSources.DegatTime);
            Player.GetComponent<Player>().HaveDamage(DataSources.Degat);
            StopCoroutine("DegatTime");
        }
        else
        {
            ZombieDebout.SetBool("Attack", false);
            ZombieDebout.SetBool("Run", true);
            StopCoroutine("DegatTime");
        }
    }
}
