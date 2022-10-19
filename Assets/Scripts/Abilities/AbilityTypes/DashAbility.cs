using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/OnSelf")]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerController movement = parent.GetComponent<PlayerController>();

        movement.movementMultiplier += dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerController movement = parent.GetComponent<PlayerController>();

        movement.movementMultiplier -= dashVelocity;
    }
}

