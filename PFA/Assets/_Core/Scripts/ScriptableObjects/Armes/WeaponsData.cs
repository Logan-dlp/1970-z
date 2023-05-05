using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmeDataFiles", menuName = "Scriptable Objects/ArmeDataFiles")]
public class WeaponsData : ScriptableObject
{
    public string Name;
    
    public GameObject Skin;
    public ParticleSystem ParticuleShoot;
    
    public float RealoadTime; // Temps de recharges -> animations /// Faire une couroutine (à tester)
    public float Damage;
    
    /// <summary>
    /// Au bout de combien de balles l'arme se recharge automatiquement.
    /// </summary>
    public int Reaload; //recharge � 0 quand il y a plus de balles

    public AudioSource SoundShoot;

    public Transform Player_cam;
    public float range;
    
    public bool automatic;
    public float AutomaticTimeShoot;
}
