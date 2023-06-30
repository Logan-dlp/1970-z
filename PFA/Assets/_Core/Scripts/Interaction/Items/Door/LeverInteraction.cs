using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemsSettings))]
public class LeverInteraction : MonoBehaviour, IInteractable
{
    private Material material;
    private Animator animator;
    private GameManager gameManager;
    private BoxCollider collider;
    public AudioSource audioSource;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        material = GetComponentInChildren<MeshRenderer>().material;
        material.SetFloat("_Power", 2);
    }

    public void Interact(GameObject _player)
    {
        animator.SetBool("On", true);
        gameManager.Nblever++;
        material.SetFloat("_Power", 100);
        collider.enabled = false;
        audioSource.Play();
    }
}
