using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour 
{
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
	}

	void Update() 
	{
		RocketMovement();
	}

	void RocketMovement()
	{ 
		if (rb != null)
			transform.position += transform.forward * rocketSpeed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision) 
	{
		explosion.enabled = true;

		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
			enemyRb.AddForce(-enemy.gameObject.transform.forward * explosionForce);
		}
		// make rocket explode with walls, and affect enemies/surroundings
	}

	private void OnTriggerEnter(Collider other) 
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if(enemy != null)
			DamageEnemiesWithExplosion(enemy);

		Player player = other.GetComponent<Player>();
		if (player != null)
			player.TakeDamage(rocketDamage);

		RecycleRocket();
	}

	private void DamageEnemiesWithExplosion(Enemy enemy) 
	{
		Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
		enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
		enemy.TakeDamage(rocketDamage);
		Destroy(gameObject);
	}

	private void RecycleRocket()
	{
		explosion.enabled = false;
		gameObject.SetActive(false);
	}
}
