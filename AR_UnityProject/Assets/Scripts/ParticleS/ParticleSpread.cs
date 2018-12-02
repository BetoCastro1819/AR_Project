using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpread : MonoBehaviour
{
	public int explosionDamage = 100;
	public float explosionDuration = 2f;
	public float expansionFactor = 0.01f;
	public float initialRadius = 0.5f;

	private ParticleSystem.ShapeModule emissionShape;
	private ParticleSystem particlesToSpread;

	private SphereCollider sphereCollider;

	private float durationTimer;

	void Start ()
	{
		sphereCollider = GetComponent<SphereCollider>();
		particlesToSpread = GetComponent<ParticleSystem>();

		sphereCollider.radius = initialRadius;

		emissionShape = particlesToSpread.shape;
		emissionShape.radius = sphereCollider.radius;

		durationTimer = 0;
	}
	
	void Update ()
	{
		durationTimer += Time.deltaTime;
		if (durationTimer <= explosionDuration)
		{
			CameraShake.GetInstance().Shake();
			sphereCollider.radius += expansionFactor;

			emissionShape.radius = sphereCollider.radius;
		}
		else
		{
			ResetExplosionValues();
		}
	}

	void ResetExplosionValues()
	{
		durationTimer = 0;

		sphereCollider.radius = initialRadius;
		emissionShape.radius = sphereCollider.radius;

		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(explosionDamage);
		}
	}
}
