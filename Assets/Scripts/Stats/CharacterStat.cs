using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{
    protected bool isDirty = true;
    protected float lastBaseValue = float.MinValue;
    protected float _value;

    protected readonly List<StatModifier> _statModifiers;

    public readonly ReadOnlyCollection<StatModifier> statModifiers;

    public float baseValue;

    public virtual float value
    {
        get
        {
            if (isDirty || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    public CharacterStat()
    {
        _statModifiers = new List<StatModifier>();
        statModifiers = _statModifiers.AsReadOnly();
    }

    public CharacterStat(float _baseValue) : this()
    {
        baseValue = _baseValue;
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        _statModifiers.Add(mod);
        _statModifiers.Sort(CompareModifierOrder);
    }

    public bool RemoveModifier(StatModifier mod)
    {
        if(_statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object _source)
    {
        bool didRemove = false;

        for(int i = _statModifiers.Count - 1; i >= 0; i--)
        {
            if(_statModifiers[i].source == _source)
            {
                isDirty = true;
                didRemove = true;
                _statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for(int i = 0; i < _statModifiers.Count; i++)
        {
            StatModifier mod = _statModifiers[i];

            if(mod.type == StatModType.Flat)
            {
                finalValue += mod.value;
            }

            else if(mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;

                if(i + 1 >= _statModifiers.Count || _statModifiers[i + 1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }

            else if(mod.type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }
}
