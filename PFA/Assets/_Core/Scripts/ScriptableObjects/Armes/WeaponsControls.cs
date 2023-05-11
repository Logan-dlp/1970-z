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
    public WeaponsData ArmsData;
    private Camera camPlayer;
    public bool TouchActivate = false;
    private int charge;

    private void Start()
    {
        camPlayer = GetComponentInParent<Camera>();
        RealoadArmes();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, 100))
        {
            Debug.DrawRay(camPlayer.transform.position, camPlayer.transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red);
        }

        if (charge > 0)
        {
            if (ArmsData.automatic == true)
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
        camPlayer.fieldOfView = 60;
    }

    public void RealoadArmes()
    {
        charge = ArmsData.Charge;
    }

    public IEnumerator RealoadCharger()
    {
        Debug.Log("Je rechage !");
        yield return new WaitForSeconds(ArmsData.RealoadTime);
        RealoadArmes();
        Debug.Log("c'est recharger !");
    }

    private void TakeDamage(Zombies _zombie)
    {
        _zombie.Life -= ArmsData.Damage;
        Debug.Log(_zombie.Life);
        Debug.Log(charge);
        _zombie.Death(gameObject.GetComponentInParent<Player>());
    }

    IEnumerator LapsTimeToShoot()
    {
        yield return new WaitForSeconds(ArmsData.AutomaticTimeShoot);
        Shoot();
        StopCoroutine("LapsTimeToShoot");
    }
}
