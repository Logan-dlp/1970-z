using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(PlayerController)), RequireComponent(typeof(PlayerInteractable)), RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public GameObject[] Weapons;
    public WeaponsData[] WeaponsDatas;

    [Header("UI")]
    public GameObject[] WeaponsUis;
    public Texture2D[] HpImg;
    public RawImage HpUi;
    public Text[] UIText;

    [HideInInspector] public float PlayerLife = 100;
    
    public int Coin = 0;
    
    private GameManager gameManager;
    private WeaponsControls weaponsControls;
    private PlayerController playerController;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        weaponsControls = GetComponentInChildren<WeaponsControls>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Death();
        UpdateUI();
    }

    void Death()
    {
        if (PlayerLife <= 0)
        {
            gameManager.Player.Remove(gameObject);
            gameManager.StopGame();
            Destroy(gameObject);
        }
    }

    void UpdateUI()
    {
        WeaponsUI();
        HPUI();
        TextUI();
    }

    void WeaponsUI()
    {
        for (int i = 0; i < WeaponsDatas.Length; i++)
        {
            if (WeaponsDatas[i] == weaponsControls.WeaponsData)
            {
                ResetWeaponsUI();
                WeaponsUis[i].SetActive(true);
            }
        }
    }

    void ResetWeaponsUI()
    {
        for (int i = 0; i < WeaponsUis.Length; i++)
        {
            WeaponsUis[i].SetActive(false);
        }
    }

    void HPUI()
    {
        if (PlayerLife >= 75)
        {
            HpUi.texture = HpImg[0];
        }else if (PlayerLife >= 50)
        {
            HpUi.texture = HpImg[1];
        }else if (PlayerLife >= 25)
        {
            HpUi.texture = HpImg[2];
        }else
        {
            HpUi.texture = HpImg[3];
        }
    }

    void TextUI()
    {
        UIText[0].text = Coin.ToString();
        UIText[1].text = weaponsControls.Charge.ToString() + "/∞";
        UIText[2].text = weaponsControls.NbGrenade.ToString();
    }
}
