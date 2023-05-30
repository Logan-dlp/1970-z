using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieDataFiles", menuName = "Scriptable Objects/ZombieDataFiles")]
public class ZombiesData : ScriptableObject
{
    public string Name;
    public bool IsBoss;
    public GameObject Skin;

    /*
     * à modifier si il a plusieurs animation untiliser directement un tableau
     *            voir si c'est bien "Animator" à utiliser
     */
    public Animator Animation;

    public float Life;
    public float Degat;
    public float DegatTime;
    public float Speed;
    public float StopDistance;

    public int Awards;

    public AudioSource SoundZombie;
}
