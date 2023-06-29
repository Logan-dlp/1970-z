using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Houses : MonoBehaviour
{
    public AudioSource audioSourceLeft;
    public AudioSource audioSourceRight;
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {

        // Vérifier si l'objet qui entre dans le trigger est le joueur (ou tout autre objet que vous souhaitez détecter)
        if (other.CompareTag("Player"))
        {
            if (!hasEntered)
            {
                // Si l'AudioSource n'a pas encore été démarré, démarrer la lecture
                audioSourceLeft.Play();
                audioSourceRight.Play();
                hasEntered = true;
            }
            else
            {
                // Si l'AudioSource a déjà été démarré, arrêter la lecture
                audioSourceLeft.Stop();
                audioSourceRight.Play();
                hasEntered = false;
            }
        }
    }
}
