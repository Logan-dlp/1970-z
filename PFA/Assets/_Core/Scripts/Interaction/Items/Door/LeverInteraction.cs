using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool interactable = true;
    private GameManager gameManager;
    private ItemsSettings price;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator.GetComponent<Animator>();
        price = GetComponent<ItemsSettings>();
    }

    public void Interact(GameObject _player)
    {
        if (interactable)
        {
            animator.SetBool("On", true);
            gameManager.Nblever++;
            interactable = false;
            price.Price = 0;
        }
    }
}
