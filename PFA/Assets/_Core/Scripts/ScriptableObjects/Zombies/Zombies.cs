using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    public ZombiesData DataSources;

    private void Start()
    {
        Instantiate(DataSources.Skin, transform);
    }
    /*
     * Trouver le joueur le plus proche (en boucle)
     * le suivre
     * Arrivé a une certaine distance Infliger des dégats à la personne suivit (et faire une autre animation que de marcher)
     */
}
