using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class ArmesInteraction : MonoBehaviour, IInteractable
{
    public GameObject Arme;
    public bool Interactable = false;
    
    public void Interact(GameObject _player)
    {
        if (Interactable == true)
        {
            GameObject[] _tabWeapons = _player.GetComponent<Player>().Weapons;
            WeaponsOff(_tabWeapons);
            WeaponsTab(_tabWeapons).SetActive(true);
        }
    }

    void WeaponsOff(GameObject[] _weapons)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].SetActive(false);
        }
    }

    GameObject WeaponsTab(GameObject[] _weapons)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (Arme.GetComponent<WeaponsControls>().ArmsData.Name == _weapons[i].GetComponent<WeaponsControls>().ArmsData.Name)
            {
                return _weapons[i];
            }
        }
        return gameObject;
    }
}
