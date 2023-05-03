using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class ArmesInteraction : MonoBehaviour, IInteractable
{
    public bool Interactable = false;
    
    public void Interact(GameObject _player)
    {
        if (Interactable == true)
        {
            Debug.Log("Tu as une arme ! " + gameObject);
        }
        return;
    }
}
