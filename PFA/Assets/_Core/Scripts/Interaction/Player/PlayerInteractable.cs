using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractable : MonoBehaviour
{
    public float InteractRange;
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
        if (interact == true)
        {
            Ray _r = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(_r, out RaycastHit _hit, InteractRange))
            {
                if (_hit.collider.gameObject.TryGetComponent(out IInteractable _interactable))
                {
                    _interactable.Interact();
                    interact = false;
                }
            }
        }
    }

    public void InteractPerformed(InputAction.CallbackContext _ctx)
    {
        interact = _ctx.performed;
    }
}
