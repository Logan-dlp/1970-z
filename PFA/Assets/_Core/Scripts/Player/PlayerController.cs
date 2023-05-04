using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool sprint = false;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector2 sensitivity = Vector2.one;
    [SerializeField] private Transform Playercam;

    private Vector3 velocity;

    private Vector2 moveInputs, lookInputs;
    private bool jumpPerformed;

    private CharacterController characterController;

    public WeaponsControls Arms;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Playercam = transform.Find("Player_cam");
    }

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        // Calcul de la vitesse horizontale et verticale du joueur
        Vector3 _horizontalVelocity = speed * new Vector3(moveInputs.x, 0, moveInputs.y);
        float _gravityVelocity = Gravity(velocity.y);

        // Calcul de la vitesse totale du joueur
        velocity = _horizontalVelocity + _gravityVelocity * Vector3.up;

        TryJump();

        Vector3 _move = transform.forward * velocity.z + transform.right * velocity.x + transform.up * velocity.y;

        if (sprint)
        {
            // Calcul du déplacement du joueur quand il court
            characterController.Move(_move * 2 * Time.deltaTime);
        }
        else
        {
            // Calcul du déplacement du joueur quand il marche
            characterController.Move(_move * Time.deltaTime);
        }
    }

    private void Look()
    {
        // Rotation horizontale du joueur
        transform.Rotate(lookInputs.x * sensitivity.x * Time.deltaTime * Vector3.up);
        // Calcul de la rotation verticale de la caméra
        float _camAngleX = Playercam.localEulerAngles.x - lookInputs.y * Time.deltaTime * sensitivity.y;

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
        velocity.y += jumpForce;
        jumpPerformed = false;
    }

    public void MovePerformed(InputAction.CallbackContext _ctx) => moveInputs = _ctx.ReadValue<Vector2>();
    public void RunPerformed(InputAction.CallbackContext _ctx) => sprint = _ctx.ReadValue<float>() > 0;
    public void LookPerformed(InputAction.CallbackContext _ctx) => lookInputs = _ctx.ReadValue<Vector2>();
    public void JumpPerformed(InputAction.CallbackContext _ctx) => jumpPerformed = _ctx.performed;
    public void ShootPerformed(InputAction.CallbackContext _ctx) => Arms.Shoot();
    public void AimPerformed(InputAction.CallbackContext _ctx) => Arms.Aim();
    public void NoAimPerformed(InputAction.CallbackContext _ctx) => Arms.NoAim();

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
