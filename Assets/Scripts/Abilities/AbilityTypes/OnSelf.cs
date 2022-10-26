using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/OnSelf")]
public class OnSelf : Ability
{
    public enum StatType
    {
        Speed,
        JumpSpeed,
        FireRate,
        Cooldown,
    }

    [Space]
    public List<StatAndMagnitude> statAndMagnitude = new List<StatAndMagnitude>();

    [Serializable]
    public class StatAndMagnitude
    {
        [HideInInspector]
        public string inspectorName;

        public StatType stat;
        public float magnitude;
    }

    private void OnValidate()
    {
        for(int i = 0; i < statAndMagnitude.Count; i++)
        {
            statAndMagnitude[i].inspectorName = statAndMagnitude[i].stat.ToString();
        }
    }

    public override void Activate(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        foreach (StatAndMagnitude sam in statAndMagnitude) 
        {
            switch (sam.stat)
            {
                case (StatType.Speed):
                    player.Speed.AddModifier(new StatModifier(sam.magnitude, StatModType.PercentAdd, this));
                    break;
                case (StatType.JumpSpeed):
                    player.JumpSpeed.AddModifier(new StatModifier(sam.magnitude, StatModType.PercentAdd, this));
                    break;
                case (StatType.FireRate):
                    player.FireRate.AddModifier(new StatModifier(sam.magnitude, StatModType.PercentAdd, this));
                    break;
                case (StatType.Cooldown):
                    player.Cooldown.AddModifier(new StatModifier(sam.magnitude, StatModType.PercentAdd, this));
                    break;

            }
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerController player = parent.GetComponent<PlayerController>();

        player.Speed.RemoveAllModifiersFromSource(this);

        player.JumpSpeed.RemoveAllModifiersFromSource(this);

        player.FireRate.RemoveAllModifiersFromSource(this);

        player.Cooldown.RemoveAllModifiersFromSource(this);
    }
}

