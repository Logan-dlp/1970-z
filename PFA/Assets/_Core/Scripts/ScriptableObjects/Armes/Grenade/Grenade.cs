using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject grenadeFbx;
    [SerializeField] private int force = 800;
    [SerializeField] private int NbGrenade = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (NbGrenade > 0))
        {
            NbGrenade--;
            GameObject Go = Instantiate(grenadeFbx, transform.position, Quaternion.identity);
            Go.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * force);

            if (NbGrenade == 0)
            {
                NbGrenade = 0;
            }
        }
    }
}

