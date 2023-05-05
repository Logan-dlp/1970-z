using System.Collections;
using System.Collections.Generic;
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
    
    private void Start()
    {
        camPlayer = GetComponentInParent<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, ArmsData.range))
        {
            Debug.DrawRay(camPlayer.transform.position, camPlayer.transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red);
        }

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

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, ArmsData.range))
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

    private void TakeDamage(Zombies _zombie)
    {
        _zombie.Life -= ArmsData.Damage;
        _zombie.Death();
    }

    IEnumerator LapsTimeToShoot()
    {
        yield return new WaitForSeconds(ArmsData.AutomaticTimeShoot);
        Shoot();
        StopCoroutine("LapsTimeToShoot");
    }
}
