using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class DoorInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _player)
    {
        Destroy(gameObject);
    }
}
