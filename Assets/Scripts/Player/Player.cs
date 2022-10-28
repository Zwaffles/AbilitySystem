using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStat Speed;
    public CharacterStat JumpSpeed;
    public CharacterStat FireRate;
    public CharacterStat Cooldown;

    public Weapon currentWeapon;

    public AbilityHolder abilityHolder;
}
