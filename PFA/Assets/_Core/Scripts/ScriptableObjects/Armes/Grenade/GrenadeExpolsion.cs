using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExpolsion : MonoBehaviour
{
    [SerializeField] private float secondes = 2f;
    private bool boom = false;
    [SerializeField] private GameObject particle;
    private Zombies Zombies;
    private int damage = 100;
    private int Life;

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(Explosion());
    }

    private void OnTriggerStay(Collider other)
    {
        if(!boom) return;
        if (other.CompareTag("Zombies"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(secondes);
        boom = true;
    }
}
