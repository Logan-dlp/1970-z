using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataFiles", menuName = "Scriptable Objects/WeaponDataFiles")]
public class WeaponData : ScriptableObject
{
    public string Name;
    
    public GameObject Skin;
    public GameObject ParticuleShoot;
    public GameObject SoundShoot;
    public List<string> Zombies;

    public float BulletSpeed;
    public float BulletDamage;
    //Combien de balles par secondes
    public float BulletTime;
    public float RealoadTime;
    //Recharge automactiquement à 0
    public int Reload;
}
