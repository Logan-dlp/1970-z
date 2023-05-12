using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class ArmesInteraction : MonoBehaviour, IInteractable
{
    private WeaponsControls weaponsControls;
    
    public GameObject Weapon;
    public WeaponsData Data;
    public bool Interactable = false;
    

    public void Interact(GameObject _player)
    {
        if (Interactable == true)
        {
            GameObject[] _tabWeapons = _player.GetComponent<Player>().Weapons;
            WeaponsOff(_tabWeapons);
            WeaponsTab(_tabWeapons).SetActive(true);
            _player.GetComponentInChildren<WeaponsControls>().WeaponsData = Data;
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
            if (Weapon.name == _weapons[i].name)
            {
                return _weapons[i];
            }
        }
        return gameObject;
    }
}
