using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractable : MonoBehaviour
{
    public float InteractRange;
    public GameObject InteractUI;
    private PlayerInput playerInput;
    private Player player;
    private bool interact = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        InputAction _interact = playerInput.actions["Interact"];
        _interact.performed += InteractPerformed;
        InteractUI.SetActive(false);
    }

    private void Update()
    {
        Ray _r = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(_r, out RaycastHit _hit, InteractRange))
            {
                if (_hit.collider.gameObject.TryGetComponent(out IInteractable _interactable))
                {
                    InteractUI.SetActive(true);
                }
                else
                {
                    InteractUI.SetActive(false);
                }
            }
            else
            {
                InteractUI.SetActive(false);
            }

            if (interact == true)
            {
                if (_hit.collider.gameObject.TryGetComponent(out IInteractable _interactable) && _hit.collider.gameObject.TryGetComponent(out ItemsSettings _itemsSettings))
                {
                    if (player.Coin >= _itemsSettings.Price)
                    {
                        _interactable.Interact();
                        interact = false;
                    }
                    else
                    {
                        interact = false;
                    }
                }
                else
                {
                    interact = false;
                }
            }
    }

    public void InteractPerformed(InputAction.CallbackContext _ctx)
    {
        interact = _ctx.performed;
    }
}
