using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnSelf;

[RequireComponent(typeof(Player))]
public class AbilityHolder : MonoBehaviour
{
    public enum AbilityState
    {
        ready,
        active,
        cooldown,
    }

    public List<AbilityStatus> abilities = new List<AbilityStatus>();

    [Serializable]
    public class AbilityStatus
    {
        [HideInInspector]
        public string inspectorName;

        public Ability _ability;

        [HideInInspector]
        public AbilityState _state = AbilityState.ready;

        [HideInInspector]
        public float _cooldown;

        [HideInInspector]
        public float _duration;
    }

    private void OnValidate()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].inspectorName = abilities[i]._ability.name;
        }
    }

    public void UseAbility(int index)
    {
        if (abilities[index]._state != AbilityState.ready)
            return;

        abilities[index]._ability.Activate(gameObject);
        abilities[index]._state = AbilityState.active;
        abilities[index]._duration = abilities[index]._ability.duration; 
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            switch (ability._state)
            {
                case AbilityState.active:
                    if (ability._duration > 0)
                    {
                        ability._duration = (float)Math.Round(ability._duration - Time.deltaTime, 3);
                    }
                    else
                    {
                        ability._ability.BeginCooldown(gameObject);
                        ability._state = AbilityState.cooldown;
                        ability._cooldown = ability._ability.cooldown;
                    }
                    break;

                case AbilityState.cooldown:
                    if (ability._cooldown > 0)
                    {
                        ability._cooldown = (float)Math.Round(ability._cooldown - Time.deltaTime, 4);
                    }
                    else
                    {
                        ability._state = AbilityState.ready;
                    }
                    break;
            }
        }
    }
}
