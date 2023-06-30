using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SD_Spawn : MonoBehaviour
{
    public AudioSource SoundSpawn; 
    public AudioSource SoundRainAmbiant;
    public GameObject Collision;
    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si le tag de l'objet qui entre dans le trigger est "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Désactiver le son en définissant le volume à 0
            SoundSpawn.volume = 0f;
            SoundRainAmbiant.Play();
            gameObject.SetActive(false);
            Collision.SetActive(false);
        }
    }

}
