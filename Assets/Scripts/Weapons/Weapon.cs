using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage = 1.0f;
    public float fireRate = .25f;

    public float range = 50f;
    public float hitForce = 100f;

    public bool active;

    private bool canFire = true;

    private void Update()
    {
        Debug.Log("can fire? " + canFire);
        if (active && canFire)
        {
            canFire = false;
            StartCoroutine(NextFire());
            Fire();
        }
    }

    public virtual void Fire()
    {
        
    }

    private IEnumerator NextFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

}
