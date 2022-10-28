using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    Player player;
    CharacterController characterController;

    public Camera playerCamera;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public Vector3 moveDirection = Vector2.zero;
    public Vector2 horizontalInput { get; private set; }
    private bool isJumping;
    private Vector2 look;
    private float rotationX = 0;

    void Start()
    {
        player = GetComponent<Player>();
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

        Vector2 curMovement = canMove ? player.Speed.value * horizontalInput : Vector2.zero;

        float movementDirectionY = moveDirection.y;
        moveDirection = (right * curMovement.x) + (forward * curMovement.y);

        if (isJumping && canMove && characterController.isGrounded)
        {
            moveDirection.y = player.JumpSpeed.value;
        }
        else
        {
            isJumping = false;
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

    public void ReceiveMovementInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void ReceiveMouseInput(Vector2 mouseInput)
    {
        look.x = mouseInput.x;
        look.y = mouseInput.y;
    }

    public void OnJumpPressed()
    {
        isJumping = true;
    }
}
