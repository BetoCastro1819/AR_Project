using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
	public GameObject explosionEffect;
	public int damage = 50;
	public float timeToExplode = 2f;
	public float explosionRadius = 3;
	public float explosionForce = 100f;
	public float upwardsModifier = 100f;

	private SphereCollider explosion;
	private float timer;

	private void Start()
	{
		explosion = GetComponent<SphereCollider>();
		explosion.radius = explosionRadius;
		explosion.enabled = false;
		timer = 0;
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > timeToExplode)
		{
			timer = 0;
			Explode();
		}
	}

	void Explode()
	{
		explosion.enabled = true;
		Instantiate(explosionEffect, transform.position, transform.rotation);
		Destroy(gameObject, 1f);
	}

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
			enemyRb.constraints = RigidbodyConstraints.None;
			enemy.TakeDamage(damage);
			enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
			//enemyRb.AddForce(-enemy.gameObject.transform.forward * explosionForce);
		}
	}
}
