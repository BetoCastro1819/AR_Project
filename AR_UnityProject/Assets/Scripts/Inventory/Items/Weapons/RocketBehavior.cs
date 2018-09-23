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

	private void Start() 
	{
		rb = GetComponent<Rigidbody>();
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
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		if(enemy != null)
		{
			Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
			enemyRb.AddForce(-enemy.gameObject.transform.forward * explosionForce);
			SphereCollider explosion = GetComponent<SphereCollider>();
			explosion.enabled = true;
		}

		// make rocket explode with walls, and affect enemies/surroundings
	}

	private void OnTriggerEnter(Collider other) 
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if(enemy != null)
			CreateExplosion(enemy);		
	}

	private void CreateExplosion(Enemy enemy) 
	{
		Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
		enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
		enemy.TakeDamage(rocketDamage);
		Destroy(gameObject);
	}
}
