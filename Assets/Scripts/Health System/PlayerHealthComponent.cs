using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    [Header("UI"), SerializeField]
    private RectTransform healthBar;

    /* For Testing Only */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(20);
        }
    }

    public override void Awake()
    {
        base.Awake();
        UpdateHealthBar();
    }

    public override void Heal(float value)
    {
        base.Heal(value);
        UpdateHealthBar();
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        /* Placeholder until we figure out healthbar stuff */
        float percentage = CurrentHealth / MaxHealth;
        healthBar.localScale = new Vector3(percentage, 1, 1);
    }

    public override void Die()
    {
        base.Die();
        /* Change Game State, Disable Controls, Load Leaderboard etc... */
        GameManager.Instance.SetGameState(GameState.GameOver);
    }
}