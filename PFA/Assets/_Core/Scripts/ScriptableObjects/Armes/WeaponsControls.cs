using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WeaponsControls : MonoBehaviour
{

    [SerializeField] private WeaponsData armsData;
    [SerializeField] private ZombiesData zombiesData;
    [SerializeField] private Zombies zombies;
    private Transform cam;
    private Camera camPlayer;
    [SerializeField] float range = 100f;
    private float damage;

    public bool TouchActivate = false;

 

    private void Start()
    {
        Camera _cam = GetComponentInParent<Camera>();
        cam = _cam.gameObject.transform;
        range = armsData.range;
        damage = armsData.Damage;
        armsData.Cartridges = 600;
        armsData.BulletsCount = 0;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red);
        }

        if (TouchActivate == true)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.GetComponent<Zombies>())
            {
                
                TakeDamage(hit.transform.gameObject);
                Debug.Log("hitNoAutomatic");
            }
            else
            {
                TouchActivate = false;
            }
        }
        else
        {
            TouchActivate = false;
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

    private void TakeDamage(GameObject _zombie)
    {
        if (armsData.automatic == false)
        {
            _zombie.GetComponent<Zombies>().Life -= damage;
            _zombie.GetComponent<Zombies>().Death();
            Debug.Log(_zombie.GetComponent<Zombies>().Life);
            TouchActivate = false;
        }

        if (armsData.automatic == true)
        {

        }
    }
}
