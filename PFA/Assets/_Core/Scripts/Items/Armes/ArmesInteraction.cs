using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class ArmesInteraction : MonoBehaviour, IInteractable
{
    public WeaponsData Data;
    public bool Interactable = false;
    
    public void Interact(GameObject _player)
    {
        if (Interactable == true)
        {
            _player.GetComponentInChildren<WeaponsControls>().ArmsData = Data;
            _player.GetComponentInChildren<WeaponsControls>().Reload();
            Debug.Log("Tu as une arme ! " + gameObject.GetComponent<WeaponsControls>().ArmsData.Name);
        }
    }
}
