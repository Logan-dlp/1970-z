using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    public float Speed = 4f;
    public float JumpForce = 5f;
    public Vector2 Sensitivity = Vector2.one;
    
    private Vector3 velocity;
    private Vector2 moveInputs, lookInputs;
    
    [SerializeField] private Transform Playercam;
    private CharacterController characterController;
    private WeaponsControls Arms;
    
    private bool jumpPerformed;
    private bool sprint = false;
    private bool AimActive = false;
    
    //Animatons:
    public Animator animator;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Arms = GetComponentInChildren<WeaponsControls>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        // Calcul de la vitesse horizontale et verticale du joueur
        Vector3 _horizontalVelocity = Speed * new Vector3(moveInputs.x, 0, moveInputs.y);
        float _gravityVelocity = Gravity(velocity.y);

        // Calcul de la vitesse totale du joueur
        velocity = _horizontalVelocity + _gravityVelocity * Vector3.up;

        TryJump();
        
        Vector3 _move = transform.forward * velocity.z + transform.right * velocity.x + transform.up * velocity.y;

       if (sprint)
        {
            // Calcul du déplacement du joueur quand il court
            characterController.Move(_move * 2 * Time.deltaTime);
            animator.SetBool("IsRunning", true);
            animator.SetBool("Idle", false);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            // Calcul du déplacement du joueur quand il marche
            characterController.Move(_move * Time.deltaTime);
            animator.SetBool("IsRunning", false);
            animator.SetBool("Idle", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsJumping", false);
            if (moveInputs.magnitude > 0)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("Idle", false);
                animator.SetBool("IsJumping", false);

            }
            Debug.Log("Magnitude: " + moveInputs.magnitude);
        }
    }

    private void Look()
    {
        // Rotation horizontale du joueur
        transform.Rotate(lookInputs.x * Sensitivity.x * Time.deltaTime * Vector3.up);
        // Calcul de la rotation verticale de la caméra
        float _camAngleX = Playercam.localEulerAngles.x - lookInputs.y * Time.deltaTime * Sensitivity.y;

        // Limite la rotation verticale de la caméra à un certain angle
        if (_camAngleX <= 90f)
        {
            _camAngleX = _camAngleX > 0 ? Mathf.Clamp(_camAngleX, 0f, 85f) : _camAngleX;
        }

        if (_camAngleX > 270f)
        {
            _camAngleX = Mathf.Clamp(_camAngleX, 275f, 360f);
        }

        // Ajout d'une interpolation pour la rotation verticale de la caméra
        float targetCamAngleX = _camAngleX;
        float joystickMagnitude = lookInputs.magnitude;
        if (joystickMagnitude > 0.2f)
        {
            // Modifier la vitesse maximale d'interpolation si nécessaire
            float maxSpeed = 5f;
            float speed = maxSpeed * joystickMagnitude;
            targetCamAngleX = Mathf.Lerp(Playercam.localEulerAngles.x, _camAngleX, Time.fixedDeltaTime * speed);
        }

        // Applique la rotation à la caméra
        Playercam.localEulerAngles = Vector3.right * _camAngleX;
    }

    private float Gravity(float _verticalVelocity)
    {
        if (characterController.isGrounded) return 0f;

        _verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;

        return _verticalVelocity;
    }

    private void TryJump()
    {
        if (!jumpPerformed || !characterController.isGrounded) return;
        velocity.y += JumpForce;
        jumpPerformed = false;
        animator.SetBool("IsJumping", false);
    }

    public void MovePerformed(InputAction.CallbackContext _ctx) => moveInputs = _ctx.ReadValue<Vector2>();
    public void RunPerformed(InputAction.CallbackContext _ctx) => sprint = _ctx.ReadValue<float>() > 0;
    public void LookPerformed(InputAction.CallbackContext _ctx) => lookInputs = _ctx.ReadValue<Vector2>();
    public void JumpPerformed(InputAction.CallbackContext _ctx)
    {
        Debug.Log("input");
        jumpPerformed = _ctx.performed;
        animator.SetBool("IsJumping", true);
        animator.SetBool("Idle", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsWalking", false);
    } 
    public void ShootPerformed(InputAction.CallbackContext _ctx)
    {
        Arms.TouchActivate = _ctx.performed;
        if (AimActive && Arms.TouchActivate)
        {
            animator.SetBool("ShootAim", true);
            animator.SetBool("ShootNoAim", false);
        }
        else
        {
            animator.SetBool("ShootNoAim", true);
            animator.SetBool("ShootAim", false);
            animator.SetBool("Idle", false);
        }
    } 
    public void AimPerformed(InputAction.CallbackContext _ctx)
    {
        Arms.Aim();
        AimActive = true;
        animator.SetBool("Aim", true);
    } 
    public void NoAimPerformed(InputAction.CallbackContext _ctx)
    {
        Arms.NoAim();
        AimActive = false;
        animator.SetBool("Aim", false);
    }

    private void OnDisable()
    {
        AimPerformed();
    }
    
    private void OnEnable()
    {
        AimPerformed();
    }

    private void AimPerformed()
    {
        Arms.Aim();
    }
}