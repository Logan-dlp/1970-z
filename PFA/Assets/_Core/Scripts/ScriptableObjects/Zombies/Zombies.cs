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

    public float Life;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 1;
        agent.speed = DataSources.Speed;
        agent.angularSpeed = 100;
        agent.acceleration = 100;
        agent.stoppingDistance = DataSources.StopDistance;
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        Instantiate(DataSources.Skin, transform);
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
            Destroy(gameObject);
        }
    }

    IEnumerator DegatTime()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= agent.stoppingDistance)
        {
            yield return new WaitForSeconds(DataSources.DegatTime);
            Player.GetComponent<Player>().PlayerLife -= DataSources.Degat;
            StopCoroutine("DegatTime");
        }
    }
}
