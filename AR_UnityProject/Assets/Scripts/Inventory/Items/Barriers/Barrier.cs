using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int health = 30;
    public int damageFromExplosions = 10;

	void Update ()
    {
        if (health <= 0)
            DestroyBarrier();
	}

    void DestroyBarrier()
    {
        // Add destuction effect
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletBehavior bullet = collision.gameObject.GetComponent<BulletBehavior>();
        if (bullet)
            TakeDamage(bullet.bulletDamage);

        RocketBehavior rocket = collision.gameObject.GetComponent<RocketBehavior>();
        if (rocket)
            TakeDamage(rocket.rocketDamage);
    }

    // Handles explosions
    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(damageFromExplosions);
    }
}
