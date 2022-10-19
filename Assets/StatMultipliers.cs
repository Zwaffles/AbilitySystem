using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMultipliers
{
    public float MovementMultiplier = 1.0f,
        JumpMultiplier = 1.0f,
        FireRateMultiplier = 1.0f, 
        CooldownMultiplier = 1.0f,
        DamageMultiplier = 1.0f,
        DamageTakenMultiplier = 1.0f;

    public enum Multiplier
    {
        Movement,
        Jump,
        FireRate,
        Cooldown,
        Damage,
        DamageTaken
    }

    public float GetMultiplier(Multiplier multiplier)
    {
        switch (multiplier)
        {
            case Multiplier.Movement:
                return MovementMultiplier;

            case Multiplier.Jump:
                return JumpMultiplier;

            case Multiplier.FireRate:
                return FireRateMultiplier;

            case Multiplier.Cooldown:
                return CooldownMultiplier;

            case Multiplier.Damage:
                return DamageMultiplier;

            case Multiplier.DamageTaken:
                return DamageTakenMultiplier;
            default:
                return 0f;
        }
    } 
    
    public void SetMultiplier(Multiplier multiplier, float magnitude)
    {
        switch (multiplier)
        {
            case Multiplier.Movement:
                MovementMultiplier += magnitude;
                break;

            case Multiplier.Jump:
                MovementMultiplier += magnitude;
                break;

            case Multiplier.FireRate:
                MovementMultiplier += magnitude;
                break;

            case Multiplier.Cooldown:
                MovementMultiplier += magnitude;
                break;

            case Multiplier.Damage:
                MovementMultiplier += magnitude;
                break;

            case Multiplier.DamageTaken:
                return DamageTakenMultiplier;
            default:
                return 0f;
        }
    }
}
