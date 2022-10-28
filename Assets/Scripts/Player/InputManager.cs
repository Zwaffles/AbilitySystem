using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerMovement movement;

    PlayerInputs inputs;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        inputs = new PlayerInputs();

        inputs.Gameplay.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        inputs.Gameplay.Look.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

        inputs.Gameplay.Jump.performed += _ => movement.OnJumpPressed();

        inputs.Gameplay.Shoot.performed += _ => player.currentWeapon.OnFirePressed();
        inputs.Gameplay.Shoot.canceled += _ => player.currentWeapon.OnFireReleased();

        inputs.Gameplay.Ability1.performed += _ => player.abilityHolder.UseAbility(0);
        inputs.Gameplay.Ability2.performed += _ => player.abilityHolder.UseAbility(1);
        inputs.Gameplay.Ability3.performed += _ => player.abilityHolder.UseAbility(2);
        inputs.Gameplay.Ability4.performed += _ => player.abilityHolder.UseAbility(3);
    }

    private void Update()
    {
        movement.ReceiveMovementInput(horizontalInput);
        movement.ReceiveMouseInput(mouseInput);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
