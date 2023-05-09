using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmeDataFiles", menuName = "Scriptable Objects/ArmeDataFiles")]
public class WeaponsData : ScriptableObject
{
    public string Name;

    public Vector3 Position;
    public Vector3 Rotation;
    
    public GameObject Skin;

    public int Charge;
    
    public float RealoadTime;
    public float Damage;
    public float AutomaticTimeShoot;
    
    public bool automatic;
}
