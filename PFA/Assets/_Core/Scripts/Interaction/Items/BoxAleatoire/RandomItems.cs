using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ItemsSettings))]
public class RandomItems : MonoBehaviour, IInteractable
{
    public GameObject[] Weapons;
    public float TimeWait = .2f;
    public int NbBoucle = 4;

    private void Start()
    {
        UnSpawnArmes();
    }
    
    GameObject Rand()
    {
        int rand = Random.Range(0, Weapons.Length);
        return Weapons[rand];
    }
    
    void UnSpawnArmes()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }
    }

    IEnumerator AnimationAleatoire()
    {
        ResetInteractable();
        UnSpawnArmes();
        for (int i = 0; i < NbBoucle; i++)
        {
            for (int j = 0; j < Weapons.Length; j++)
            {
                Weapons[j].SetActive(true);
                yield return new WaitForSeconds(TimeWait);
                Weapons[j].SetActive(false);
            }
        }
        GameObject _arme = Rand();
        _arme.GetComponent<ArmesInteraction>().Interactable = true;
        _arme.SetActive(true);
    }

    void ResetInteractable()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].GetComponent<ArmesInteraction>().Interactable = false;
        }
    }

    public void Interact(GameObject _player)
    {
        StartCoroutine("AnimationAleatoire");
    }
}
