using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface IExplodeOnKill
{
    public bool Enabled { get; set; }
    public float ChanceToExplode { get; set; }
    public float ExplosionDamage { get; set; }
    public float ExplosionRange { get; set; }
    public Explosion ExplosionPrefab { get; set; }
}

public interface IExplodeOnHit
{
    public bool Enabled { get; set; }
    public float ChanceToExplode { get; set; }
    public float ExplosionDamage { get; set; }
    public float ExplosionRange { get; set; }
    public Explosion ExplosionPrefab { get; set; }
}

public class RaycastShoot : Weapon, IExplodeOnKill, IExplodeOnHit
{
    [SerializeField] private Transform weaponEnd;

    private Camera fpsCam;

    private WaitForSeconds effectDuration = new WaitForSeconds(.07f);

    private AudioSource weaponAudio;

    private LineRenderer hitscanLine;

    [field: SerializeField]
    public bool Enabled { get; set; }

    [field: SerializeField]
    public float ChanceToExplode { get; set;}
    
    [field: SerializeField]
    public float ExplosionDamage { get; set;}

    [field: SerializeField]
    public float ExplosionRange { get; set; }

    [field: SerializeField]
    public Explosion ExplosionPrefab { get; set;}

    void Start()
    {
        hitscanLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();

        fpsCam = GetComponentInParent<Camera>();
    }

    public override void Fire()
    {
        StartCoroutine(FireEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        hitscanLine.SetPosition(0, weaponEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
        {
            hitscanLine.SetPosition(1, hit.point);
            EnemyHealthComponent health = hit.collider.GetComponent<EnemyHealthComponent>();

            if (health != null)
                health.DamageFromSource(damage, this.gameObject);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * hitForce);

            IExplodeOnHit explodeOnHit = GetComponent<IExplodeOnHit>();

            if (explodeOnHit != null)
            {
                if (!explodeOnHit.Enabled)
                    return;

                float randValue = Random.value;

                if (randValue <= explodeOnHit.ChanceToExplode)
                {
                    Explosion explosion = Instantiate(explodeOnHit.ExplosionPrefab, hit.point, Quaternion.identity);
                    explosion.Initialize(explodeOnHit.ExplosionDamage, explodeOnHit.ExplosionRange);
                }
            }
        }
        else
            hitscanLine.SetPosition(1, fpsCam.transform.forward * range);
    }

    private IEnumerator FireEffect()
    {
        weaponAudio?.Play();
        hitscanLine.enabled = true;

        yield return effectDuration;

        hitscanLine.enabled = false;
    }
}
