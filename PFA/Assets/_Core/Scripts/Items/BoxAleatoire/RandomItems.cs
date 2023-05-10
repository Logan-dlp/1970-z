using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ItemsSettings))]
public class RandomItems : MonoBehaviour, IInteractable
{
    public GameObject[] Armes;
    public float TimeWait = .2f;
    public int NbBoucle = 4;
    public bool Interactable = false;

    private void Start()
    {
        UnSpawnArmes();
    }

    /// Retourne un objet aléatoire du tableau "Weapons"
    GameObject Rand()
    {
        int rand = Random.Range(0, Armes.Length);
        return Armes[rand];
    }
    
    void UnSpawnArmes()
    {
        for (int i = 0; i < Armes.Length; i++)
        {
            Armes[i].SetActive(false);
        }
    }

    IEnumerator AnimationAleatoire()
    {
        ResetInteractable();
        UnSpawnArmes();
        for (int i = 0; i < NbBoucle; i++)
        {
            for (int j = 0; j < Armes.Length; j++)
            {
                Armes[j].SetActive(true);
                yield return new WaitForSeconds(TimeWait);
                Armes[j].SetActive(false);
            }
        }
        GameObject _arme = Rand();
        _arme.GetComponent<ArmesInteraction>().Interactable = true;
        _arme.SetActive(true);
    }

    void ResetInteractable()
    {
        for (int i = 0; i < Armes.Length; i++)
        {
            Armes[i].GetComponent<ArmesInteraction>().Interactable = false;
        }
    }

    public void Interact(GameObject _player)
    {
        StartCoroutine("AnimationAleatoire");
    }
}
