using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : Weapon
{
    [SerializeField] private Transform weaponEnd;

    private Camera fpsCam;

    private WaitForSeconds effectDuration = new WaitForSeconds(.07f);

    private AudioSource weaponAudio;

    private LineRenderer hitscanLine;

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
                health.Damage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * hitForce);
        }
        else
            hitscanLine.SetPosition(1, fpsCam.transform.forward * range);
    }

    private IEnumerator FireEffect()
    {
        weaponAudio.Play();
        hitscanLine.enabled = true;

        yield return effectDuration;

        hitscanLine.enabled = false;
    }
}
