using System;
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
    public GameObject[] VfxFire;
    [HideInInspector] public bool TouchActivate = false;
    private bool reloadweapons;
    
    [HideInInspector] public int Charge;
    [HideInInspector] public Camera camPlayer;
    private Animator animator;
    private Player player;

    private void Awake()
    {
        camPlayer = GetComponent<Camera>();
    }

    private void Start()
    {
        player = GetComponentInParent<Player>();
        animator = GetComponentInParent<Animator>();

        InputAction _grenade = GetComponentInParent<PlayerInput>().actions["Grenade"];
        _grenade.performed += GrenadePerformed;

        InputAction _Reaload = GetComponentInParent<PlayerInput>().actions["Reload"];
        _Reaload.performed += RealoadWeaponsPerformed;
        
        UpdateWeapons();
        
        player.HitMarker.SetActive(false);
    }

    private void Update()
    {
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
        StartCoroutine("VFXFire");
        Charge--;
        RaycastHit hit;
        if (Physics.Raycast(camPlayer.transform.position, camPlayer.transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<Zombies>())
            {
                StartCoroutine(HitMarker());
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
            _instance.GetComponent<GrenadeExpolsion>().Player = player;
            _instance.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * grenadeForce);
        }
    }

    void RealoadWeaponsPerformed(InputAction.CallbackContext _ctx)
    {
        reloadweapons = true;
    }

    IEnumerator VFXFire()
    {
        for (int i = 0; i < VfxFire.Length; i++)
        {
            VfxFire[i].SetActive(true);
        }
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < VfxFire.Length; i++)
        {
            VfxFire[i].SetActive(false);
        }
    }

    IEnumerator HitMarker()
    {
        player.HitMarker.SetActive(true);
        yield return new WaitForSeconds(.3f);
        player.HitMarker.SetActive(false);
        StopCoroutine(HitMarker());
    }
}
