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
        Transform spawnPoint = parent.GetComponent<Player>().transform;

        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            spawnPoint.position + new Vector3(0, 1, 0),
            Quaternion.LookRotation(Vector3.up));

        projectileInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * projectileLaunchForce);
    }
}
