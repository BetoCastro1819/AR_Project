using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour 
{
	public GameObject trail;
	public GameObject explosionEffect;
	public int rocketDamage = 120;
	public float rocketSpeed = 100f;
	public float explosionForce = 100f;
	public float explosionRadius = 3f;
	public float upwardsModifier = 10f;

	private Rigidbody rb;
	private SphereCollider explosion;

	private void Start() 
	{
		rb = GetComponent<Rigidbody>();
		explosion = GetComponent<SphereCollider>();
		explosion.radius = explosionRadius;
		GameObject rocketTrail = Instantiate(trail, transform.position, transform.rotation);
		rocketTrail.transform.parent = transform;
	}

	void Update() 
	{
		RocketMovement();
	}

	void RocketMovement()
	{
		if (rb != null)
		{
			transform.position += transform.forward * rocketSpeed * Time.deltaTime;
		}
	}

	private void OnCollisionEnter(Collision collision) 
	{
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
			enemyRb.constraints = RigidbodyConstraints.None;
			enemy.TakeDamage(rocketDamage);
			enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
			//enemyRb.AddForce(-enemy.gameObject.transform.forward * explosionForce);
		}

		Instantiate(explosionEffect, transform.position, transform.rotation);
		explosion.enabled = true;
    }

	private void OnTriggerEnter(Collider other) 
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
			enemyRb.constraints = RigidbodyConstraints.None;
			enemy.TakeDamage(rocketDamage);
			enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
			//enemyRb.AddForce(-enemy.gameObject.transform.forward * explosionForce);
        }

        Player player = other.GetComponent<Player>();
		if (player != null)
			player.TakeDamage(rocketDamage);

		RecycleRocket();
	}

	private void RecycleRocket()
	{
		explosion.enabled = false;
		gameObject.SetActive(false);
	}
}
