using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class LeverInteraction : MonoBehaviour, IInteractable
{
    private Material material;
    private Animator animator;
    private bool interactable = true;
    private GameManager gameManager;
    private ItemsSettings price;

    private void Start()
    {
        material = GetComponentInChildren<Material>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator.GetComponent<Animator>();
        price = GetComponent<ItemsSettings>();
        material.SetFloat("_Power", 2);
    }

    public void Interact(GameObject _player)
    {
        if (interactable)
        {
            animator.SetBool("On", true);
            gameManager.Nblever++;
            interactable = false;
            material.SetFloat("_Power", 100);
            price.Price = 0;
        }
    }
}
