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
    public GameObject GrenadeFbx;
    private int grenadeForce = 800;
    public int NbGrenade = 3;
    [HideInInspector] public bool TouchActivate = false;
    private bool reloadweapons;
    
    [HideInInspector] public int Charge;
    private Camera camPlayer;
    private Animator animator;

    private void Start()
    {
        camPlayer = GetComponent<Camera>();
        animator = GetComponentInParent<Animator>();

        InputAction _grenade = GetComponentInParent<PlayerInput>().actions["Grenade"];
        _grenade.performed += GrenadePerformed;

        InputAction _Reaload = GetComponentInParent<PlayerInput>().actions["Reload"];
        _Reaload.performed += RealoadWeaponsPerformed;
        
        UpdateWeapons();
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

        if (Charge > 0)
        {
            if (WeaponsData.automatic)
            {
                if (TouchActivate)
                {
                    StartCoroutine("LapsTimeToShoot");
                }
            }
            else
            {
                if (TouchActivate)
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

        if (reloadweapons)
        {
            StartCoroutine("RealoadCharger");
        }
    }

    private void Shoot()
    {
        Charge--;
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

    public void UpdateWeapons()
    {
        RealoadArmes();
        animator.SetBool("IsAutomatic", WeaponsData.automatic);
        // animation changement d'arme
    }

    public void RealoadArmes()
    {
        Charge = WeaponsData.Charge;
    }

    public IEnumerator RealoadCharger()
    {
        yield return new WaitForSeconds(WeaponsData.RealoadTime);
        RealoadArmes();
        reloadweapons = false;
    }

    private void TakeDamage(Zombies _zombie)
    {
        _zombie.Life -= WeaponsData.Damage;
        _zombie.Death(gameObject.GetComponentInParent<Player>());
    }

    IEnumerator LapsTimeToShoot()
    {
        yield return new WaitForSeconds(WeaponsData.AutomaticTimeShoot);
        Shoot();
        StopCoroutine("LapsTimeToShoot");
    }

    void GrenadePerformed(InputAction.CallbackContext _ctx)
    {
        if (NbGrenade > 0)
        {
            NbGrenade--;
            GameObject _instance = Instantiate(GrenadeFbx, transform.position, Quaternion.identity);
            _instance.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * grenadeForce);
        }
    }

    void RealoadWeaponsPerformed(InputAction.CallbackContext _ctx)
    {
        reloadweapons = true;
    }
}
