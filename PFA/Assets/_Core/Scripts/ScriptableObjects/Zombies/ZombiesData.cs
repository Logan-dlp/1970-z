using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieDataFiles", menuName = "Scriptable Objects/ZombieDataFiles")]
public class ZombiesData : ScriptableObject
{
    public string Name;
    
    public bool IsBoss;

    public float Life;
    public float Degat;
    public float DegatTime;
    public float Speed;
    public float StopDistance;

    public int Awards;
}
