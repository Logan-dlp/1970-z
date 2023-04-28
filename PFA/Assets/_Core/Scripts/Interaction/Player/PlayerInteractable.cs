using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractable : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool interact = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        InputAction _interact = playerInput.actions["Interact"];
        _interact.performed += InteractPerformed;
    }

    private void Update()
    {
        // détecter un gameObject en question
        if (interact == true)
        {
            var interactable = gameObject.GetComponent<IInteractable>();
            if (interactable ==  null) return;
            interactable.Interact();
        }
    }

    public void InteractPerformed(InputAction.CallbackContext _ctx)
    {
        interact = _ctx.performed;
    }
}
