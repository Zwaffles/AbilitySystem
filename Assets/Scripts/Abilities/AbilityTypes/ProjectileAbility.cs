using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectile")]
public class ProjectileAbility : Ability
{
    private Transform projectileSpawnTransform;

    public GameObject projectilePrefab;
    public float projectileLaunchForce = 700f;

    public override void Activate(GameObject parent)
    {
        Transform spawnPoint = parent.GetComponent<PlayerController>().currentWeapon.transform;

        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            Quaternion.LookRotation(Vector3.up));

        projectileInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * projectileLaunchForce);
    }
}
