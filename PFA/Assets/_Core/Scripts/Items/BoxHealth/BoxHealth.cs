using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _player)
    {
        Player _players = _player.GetComponent<Player>();
        _players.PlayerLife = 100;
        Debug.Log(_players.PlayerLife);
    }
}
