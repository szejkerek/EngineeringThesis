using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject explosionEffect;
    [Header("Explosion")]
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;

    private void Update()
    {
        fireEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, transform.up);
    }

    private void OnDestroy()
    {
        Explode();
    }

    private void Explode()
    {
        if (!gameObject.scene.isLoaded) return;

        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach (var obj in projectiles)
        {
            if (Vector3.Distance(obj.transform.position, transform.position) > explosionRadius)
                continue;

            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
                continue;
            
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
}