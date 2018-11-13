﻿using UnityEngine;

public class ShootingEnemy : Enemy
{
	public GameObject explosionEffect;

	[Header("Movement")]
	public float lerpSpeed = 5f;
	public float distToKeepFromPlayer = 3f;

	[Header("Shooting")]
	public GameObject shootingPoint;
	public float shotsPerSecond = 0f;

	private float fireRateTimer;

	public override void Start()
	{
		base.Start();

		fireRateTimer = 0;
	}

	private void Update()
	{
		if (health <= 0)
		{
			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		if (player != null)
		{
			transform.LookAt(player.transform.position);

			Movement();

			Shoot();
		}
	}

	void Movement()
	{
		Vector3 targetPos = player.transform.position - player.transform.position.normalized * distToKeepFromPlayer;
		targetPos.y = transform.position.y;

		transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
	}


	void Shoot()
	{
		if (Time.time >= fireRateTimer)
		{
			fireRateTimer = Time.time + 1 / shotsPerSecond;

			GameObject bullet = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SHOOTING_ENEMY_BULLET);
			bullet.transform.position = shootingPoint.transform.position;
			bullet.transform.rotation = shootingPoint.transform.rotation;
			bullet.SetActive(true);
		}
	}
}
