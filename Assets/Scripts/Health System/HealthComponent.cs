using UnityEngine;

public abstract class HealthComponent : MonoBehaviour
{
    [Header("Health"), SerializeField]
    private float maxHealth = 100.0f;

    [Header("Sound"), SerializeField, Stem.SoundID]
    private Stem.ID healSoundID;

    [SerializeField, Stem.SoundID]
    private Stem.ID damageSoundID;

    [SerializeField, Stem.SoundID]
    private Stem.ID deathSoundID;

    private float currentHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Heal(float value)
    {
        Stem.SoundManager.Play3D(healSoundID, transform.position);
        CurrentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
    }

    public virtual void Damage(float value)
    {
        Stem.SoundManager.Play3D(damageSoundID, transform.position);
        CurrentHealth = Mathf.Clamp(currentHealth - value, 0, maxHealth);
        
        if(CurrentHealth == 0) 
        { 
            Die(); 
        }
    }

    public virtual void Die()
    {
        Stem.SoundManager.Play3D(deathSoundID, transform.position);
    }

    public virtual void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}