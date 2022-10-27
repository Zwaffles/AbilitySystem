using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Projectile : MonoBehaviour, IExplodeOnHit
{
    [field: SerializeField]
    public bool Enabled { get; set; }

    [field: SerializeField]
    public float ChanceToExplode { get; set; }

    [field: SerializeField]
    public float ExplosionDamage { get; set; }

    [field: SerializeField]
    public float ExplosionRange { get; set; }

    [field: SerializeField]
    public Explosion ExplosionPrefab { get; set; }

    public void Awake()
    {
        Enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IExplodeOnHit explodeOnKHit = GetComponent<IExplodeOnHit>();

        if (explodeOnKHit != null)
        {
            if (!explodeOnKHit.Enabled)
                return;

            float randValue = Random.value;

            if (randValue <= explodeOnKHit.ChanceToExplode)
            {
                Explosion explosion = Instantiate(explodeOnKHit.ExplosionPrefab, transform.position, Quaternion.identity);
                explosion.Initialize(explodeOnKHit.ExplosionDamage, explodeOnKHit.ExplosionRange);
            }
        }

        Destroy(gameObject);
    }
}
