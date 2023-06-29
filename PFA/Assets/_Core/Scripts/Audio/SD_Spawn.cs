using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Spawn : MonoBehaviour
{
    public AudioSource SoundSpawn;
    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si le tag de l'objet qui entre dans le trigger est "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Désactiver le son en définissant le volume à 0
            SoundSpawn.volume = 0f;
        }
    }

}
