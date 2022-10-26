using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterStat Speed;
    public CharacterStat JumpSpeed;
    public CharacterStat FireRate;
    public CharacterStat Cooldown;

    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Weapon currentWeapon;

    CharacterController characterController;
    public Vector3 moveDirection = Vector2.zero;
    float rotationX = 0;

    PlayerInputs inputs;

    public Vector2 movement { get; private set; }
    public float movementMultiplier = 1;

    [HideInInspector]
    public bool canMove = true;
    public bool isRunning { get; private set; }
    private bool isJumping;
    private Vector2 look;

    private void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputs.Gameplay.Enable();

        inputs.Gameplay.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        inputs.Gameplay.Movement.canceled += _ => movement = Vector2.zero;

        inputs.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        inputs.Gameplay.Look.canceled += _ => look = Vector2.zero;

        inputs.Gameplay.Jump.performed += _ => isJumping = true;
        inputs.Gameplay.Jump.canceled += _ => isJumping = false;

        inputs.Gameplay.Run.performed += _ => isRunning = true;
        inputs.Gameplay.Run.canceled += _ => isRunning = false;

        inputs.Gameplay.Shoot.performed += _ => currentWeapon.active = true;
        inputs.Gameplay.Shoot.canceled += _ => currentWeapon.active = false;

        inputs.Gameplay.Ability1.performed += _ => GetComponent<AbilityHolder>().UseAbility(0);
        inputs.Gameplay.Ability2.performed += _ => GetComponent<AbilityHolder>().UseAbility(1);
        inputs.Gameplay.Ability3.performed += _ => GetComponent<AbilityHolder>().UseAbility(2);
        inputs.Gameplay.Ability4.performed += _ => GetComponent<AbilityHolder>().UseAbility(3);
    }

    private void OnDisable()
    {
        inputs.Gameplay.Disable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // recalculate move direction based on input axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector2 curMovement = canMove ? (isRunning ? Speed.value : Speed.value) * movement * movementMultiplier : Vector2.zero;

        float movementDirectionY = moveDirection.y;
        moveDirection = (right * curMovement.x) + (forward * curMovement.y);

        if (isJumping && canMove && characterController.isGrounded)
        {
            moveDirection.y = JumpSpeed.value;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -look.y * Time.deltaTime * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, look.x * Time.deltaTime * lookSpeed, 0);
        }
    }
}
