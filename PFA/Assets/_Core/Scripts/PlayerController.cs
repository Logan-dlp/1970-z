using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Settings Player")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private Vector2 sensitivity = Vector2.one;
    [SerializeField] private Transform Playercam;

    private Vector3 velocity;

    private Vector2 moveInputs, lookInputs, runInputs;
    private bool jumpPerformed;

    private CharacterController characterController;

    public float Valeur
    {
        get { return speed; }
        set { speed = value; }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
       // Playercam = GetComponent<>();
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

        // Calcul du déplacement du joueur
        Vector3 _move = transform.forward * velocity.z + transform.right * velocity.x + transform.up * velocity.y;
        characterController.Move(_move * Time.deltaTime);
    }

    private void Look()
    {
        Debug.Log("Void Look " + "Camera rotation " + lookInputs);

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

    public void RunPerformed(InputAction.CallbackContext _ctx)
    {
        
           // runInputs = _ctx.ReadValueAsButton<Button>();
            speed = 10f;
            float speedRun = Valeur * 2;


        if (!Input.GetButton("Run"))
        {
            speed = Valeur;
        }
    }

    public void LookPerformed(InputAction.CallbackContext _ctx)
    {
        lookInputs = _ctx.ReadValue<Vector2>();
    }

    public void JumpPerformed(InputAction.CallbackContext _ctx) => jumpPerformed = _ctx.performed;
}
