using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager)), RequireComponent(typeof(MultiPlayersGestion))]
public class GameManager : MonoBehaviour
{
    public List<GameObject> Player;
    public Transform[] SpawnPlayers;

    public void Debug()
    {
        UnityEngine.Debug.Log(Player.Count);
    }
}
