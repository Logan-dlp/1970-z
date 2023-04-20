using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomItems : MonoBehaviour
{
    public GameObject[] Armes;
    public float TimeWait = .2f;
    
    private void Start()
    {
        UnSpawnArmes();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //UnSpawnArmes();
            AnimationAleatoire();
            //Rand().SetActive(true);
        }
    }

    /// Retourne un objet aléatoire du tableau "Armes"
    GameObject Rand()
    {
        int rand = Random.Range(0, Armes.Length);
        return Armes[rand];
    }

    void AnimationAleatoire()
    {
        StartCoroutine("pause");
    }

    void UnSpawnArmes()
    {
        for (int i = 0; i < Armes.Length; i++)
        {
            Armes[i].SetActive(false);
        }
    }

    IEnumerator pause()
    {
        for (int i = 0; i < Armes.Length; i++)
        {
            Armes[i].SetActive(true);
            yield return new WaitForSeconds(TimeWait);
            Armes[i].SetActive(false);
        }
    }
}
