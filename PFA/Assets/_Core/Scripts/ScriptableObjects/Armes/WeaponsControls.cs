using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WeaponsControls : MonoBehaviour
{
    public WeaponsData WeaponsData;
    [HideInInspector] public bool TouchActivate = false;
    
    private int charge;
    private Camera camPlayer;

    private void Start()
    {
        camPlayer = GetComponent<Camera>();
        RealoadArmes();
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(camPlayer.transform.position, camPlayer.transform.forward * 100);
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, 100))
        {
            Debug.DrawRay(camPlayer.transform.position, camPlayer.transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red);
        }

        if (charge > 0)
        {
            if (WeaponsData.automatic == true)
            {
                if (TouchActivate == true)
                {
                    StartCoroutine("LapsTimeToShoot");
                }
            }
            else
            {
                if (TouchActivate == true)
                {
                    Shoot();
                    TouchActivate = false;
                }
            }
        }
        else
        {
            StartCoroutine("RealoadCharger");
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        charge--;
        RaycastHit hit;
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<Zombies>())
            {
                TakeDamage(hit.transform.gameObject.GetComponent<Zombies>());
            }
        }
    }

    public void Aim()
    {
        camPlayer.fieldOfView = 40;
    }
    
    public void NoAim()
    {
        camPlayer.fieldOfView = 50;
    }

    public void RealoadArmes()
    {
        charge = WeaponsData.Charge;
    }

    public IEnumerator RealoadCharger()
    {
        Debug.Log("Je rechage !");
        yield return new WaitForSeconds(WeaponsData.RealoadTime);
        RealoadArmes();
        Debug.Log("c'est recharger !");
    }

    private void TakeDamage(Zombies _zombie)
    {
        _zombie.Life -= WeaponsData.Damage;
        Debug.Log(_zombie.Life);
        Debug.Log(charge);
        _zombie.Death(gameObject.GetComponentInParent<Player>());
    }

    IEnumerator LapsTimeToShoot()
    {
        yield return new WaitForSeconds(WeaponsData.AutomaticTimeShoot);
        Shoot();
        StopCoroutine("LapsTimeToShoot");
    }
}
