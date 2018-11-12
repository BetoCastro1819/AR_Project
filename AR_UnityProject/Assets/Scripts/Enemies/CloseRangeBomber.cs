using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeBomber : Enemy
{
	[Header("Movement")]
	public float movementSpeed = 20f;
	public float stopAtDistance = 2f;

	[Header("Explosion")]
	public GameObject explosionPrefab;
	public int explosionDamage = 30;
	public float timeToExplode = 2f;
	public float explosionRadius = 2f;
	public float explosionForce = 2000f;

	private float timer;
	private bool aboutToExplode;

	public override void Start ()
	{
		base.Start();

		timer = 0;
		aboutToExplode = false;
	}
	
	void Update ()
	{
		if (player != null)
		{
			if (!aboutToExplode)
			{
				FollowPlayer();
			}
			else
			{
				PrepareForExplosion();
			}
		}

		if (health <= 0)
		{
			Explosion();
		}
	}

	void FollowPlayer()
	{
		Vector3 dir = player.transform.position - transform.position;
		rb.velocity = dir.normalized * movementSpeed;

		if (Vector3.Distance(transform.position, player.transform.position) <= stopAtDistance)
		{
			aboutToExplode = true;
		}
	}

	void PrepareForExplosion()
	{
		rb.velocity = Vector3.zero;

		timer += Time.deltaTime;
		if (timer >= timeToExplode)
		{
			Explosion();
		}
	}

	void Explosion()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
