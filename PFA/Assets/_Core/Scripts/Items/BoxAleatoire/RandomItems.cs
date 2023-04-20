using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomItems : MonoBehaviour
{
    public GameObject[] Armes;
    public KeyCode KeyToOpen = KeyCode.E;
    public float TimeWait = .2f;
    public int NbBoucle = 4;

    private void Start()
    {
        UnSpawnArmes();
    }

    // Interaction intégré directement sur la box
    private void Update()
    {
        if (Input.GetKeyUp(KeyToOpen))
        {
            BoxSysteme();
        }
    }

    void BoxSysteme()
    {
        Debug.Log("Box OK !");
        UnSpawnArmes();
        StartCoroutine("AnimationAleatoire");
    }

    /// Retourne un objet aléatoire du tableau "Armes"
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
        for (int i = 0; i < NbBoucle; i++)
        {
            for (int j = 0; j < Armes.Length; j++)
            {
                Armes[j].SetActive(true);
                yield return new WaitForSeconds(TimeWait);
                Armes[j].SetActive(false);
            }
        }
        Rand().SetActive(true);
    }
}
