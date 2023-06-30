using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class GrenadeExpolsion : MonoBehaviour
{
    [SerializeField] private float secondes = 2f;
    private bool boom = false;
    private Zombies Zombies;
    private int damage = 100;
    private int Life;
    [SerializeField] GameObject explosionVFX;
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    [HideInInspector] public Player Player;
    public AudioSource SD_Explosion;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        explosionVFX.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(Explosion());
    }

    private void OnTriggerStay(Collider other)
    {
        if (boom)
        {
            if (other.CompareTag("Zombies"))
            {
                other.GetComponent<Zombies>().Life -= 100;
                other.GetComponent<Zombies>().Death(Player);
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(secondes);
        boom = true;
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        explosionVFX.SetActive(true);
        SD_Explosion.Play();
        Destroy(gameObject, .5235f);
    }
}