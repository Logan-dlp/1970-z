using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmeDataFiles", menuName = "Scriptable Objects/ArmeDataFiles")]
public class WeaponsData : ScriptableObject
{
    public string Name;

    public int Charge;
    
    public float RealoadTime;
    public float Damage;
    public float AutomaticTimeShoot;
    
    public bool automatic;
}
