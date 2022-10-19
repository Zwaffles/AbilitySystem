using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldown;
    float duration;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public void UseAbility()
    {
        if (state != AbilityState.ready)
            return;

        ability.Activate(gameObject);
        state = AbilityState.active;
        duration = ability.duration; 
    }

    private void Update()
    {
        switch (state)
        {
            case AbilityState.active:
                if(duration > 0)
                {
                    duration -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldown = ability.cooldown;
                }
                break;

            case AbilityState.cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
