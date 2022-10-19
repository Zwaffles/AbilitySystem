using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : HealthComponent
{
    public override void Die()
    {
        base.Die();

        //Play animation, add score, add exp, etc. here later

        Destroy(this.gameObject);
    }
}
