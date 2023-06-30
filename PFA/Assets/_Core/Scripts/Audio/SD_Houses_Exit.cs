using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SD_Houses_Exit : MonoBehaviour
{
    private SD_Houses_Enter HousesEnter;
    public AudioSource AudioRainSTOP;
    public AudioSource AmbiantRain;
    public GameObject Enter;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet qui entre dans le trigger est le joueur (ou tout autre objet que vous souhaitez détecter)
        if (other.CompareTag("Player"))
        {
            AudioRainSTOP.Stop();
            AmbiantRain.Play();
            Enter.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
