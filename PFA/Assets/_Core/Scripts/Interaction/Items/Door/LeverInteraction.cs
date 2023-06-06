using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour, IInteractable
{
    private bool interactable = true;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interact(GameObject _player)
    {
        if (interactable)
        {
            gameManager.Nblever++;
            interactable = false;
            // Faire attention a ne pas pouvoir payer 2 fois...
        }
    }
}
