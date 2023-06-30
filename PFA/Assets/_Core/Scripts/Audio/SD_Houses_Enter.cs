using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Houses_Enter : MonoBehaviour
{
    public AudioSource audioRainInterior;
    public AudioSource AmbiantRainSTOP;
    public GameObject Exit;

    private void OnTriggerEnter(Collider other)
    {

        // Vérifier si l'objet qui entre dans le trigger est le joueur (ou tout autre objet que vous souhaitez détecter)
        if (other.CompareTag("Player"))
        {
            AmbiantRainSTOP.Stop();
            audioRainInterior.Play();
            gameObject.SetActive(false);
            Exit.SetActive(true);
        }
    }
}
