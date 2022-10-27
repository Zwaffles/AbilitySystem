using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{
    public void DamageFromSource(float damage, GameObject source)
    {
        CurrentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (CurrentHealth == 0)
        {
            DieFromSource(source);
        }
    }

    public void DieFromSource(GameObject source)
    {
        base.Die();

        IExplodeOnKill explodeOnKill = source.GetComponent<IExplodeOnKill>();

        if (explodeOnKill != null)
        {
            if (!explodeOnKill.Enabled)
                return;

            float randValue = Random.value;

            if (randValue <= explodeOnKill.ChanceToExplode)
            {
                Explosion explosion = Instantiate(explodeOnKill.ExplosionPrefab, transform.position, Quaternion.identity);
                explosion.Initialize(explodeOnKill.ExplosionDamage, explodeOnKill.ExplosionRange);
            }   
        }

        Destroy(this.gameObject);
    }

    public override void Die()
    {
        base.Die();

        //Play animation, add score, add exp, etc. here later

        Destroy(this.gameObject);
    }
}
