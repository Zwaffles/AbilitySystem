using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Explosion : MonoBehaviour
{
    [Header("AnimationCurve")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float timeInSeconds = 1f;

    private Vector3 initialScale;
    private Vector3 finalScale;
    private float graphValue;

    private float damage;

    private float storedTime;

    private bool active;

    public void Initialize(float _damage, float radius)
    {
        damage = _damage;
        initialScale = transform.localScale;
        finalScale = new Vector3(radius, radius, radius);
        animationCurve.postWrapMode = WrapMode.Once;
        storedTime = Time.time;
        active = true;
        StartCoroutine(Destroy(timeInSeconds));
    }

    private void Update()
    {
        if (!active)
            return;

        graphValue = animationCurve.Evaluate((Time.time- storedTime) / timeInSeconds);
        transform.localScale = finalScale * graphValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealthComponent health = other.GetComponent<EnemyHealthComponent>();

        if (health != null)
            health.Damage(damage);

        Debug.Log(other.gameObject);
    }

    IEnumerator Destroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
