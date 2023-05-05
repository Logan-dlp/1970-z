using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WeaponsControls : MonoBehaviour
{

    public WeaponsData armsData;
    private Transform cam;
    private Camera camPlayer;

    public bool TouchActivate = false;
    
    private void Start()
    {
        Camera _cam = GetComponentInParent<Camera>();
        cam = _cam.gameObject.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, armsData.range))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red);
        }

        if (armsData.automatic == true)
        {
            if (TouchActivate == true)
            {
                Shoot();
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

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, armsData.range))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.GetComponent<Zombies>())
            {
                TakeDamage(hit.transform.gameObject.GetComponent<Zombies>());
                Debug.Log("hitNoAutomatic");
            }
        }
    }

    public void Aim()
    {
        camPlayer = cam.GetComponent<Camera>();
        camPlayer.fieldOfView = 40;
    }
    public void NoAim()
    {
        camPlayer.fieldOfView = 60;
    }

    private void TakeDamage(Zombies _zombie)
    {
        _zombie.Life -= armsData.Damage;
        _zombie.Death();
    }

    IEnumerator LapsTimeToShoot()
    {
        TouchActivate = false;
        yield return new WaitForSeconds(armsData.AutomaticTimeShoot);
        TouchActivate = true;
    }
}
