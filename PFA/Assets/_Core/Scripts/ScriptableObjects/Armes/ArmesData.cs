using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmeDataFiles", menuName = "Scriptable Objects/ArmeDataFiles")]
public class ArmesData : ScriptableObject
{
    public string Name;
    
    public GameObject Skin;
    public ParticleSystem ParticuleShoot;
    
    public float BulletSpeed;
    public float RealoadTime;
    
    /// <summary>
    /// Au bout de combien de balles l'arme se recharge automatiquement.
    /// </summary>
    public int Reaload;

    public AudioSource SoundShoot;
}
