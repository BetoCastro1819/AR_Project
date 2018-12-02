using UnityEngine;

public class ShootingEnemy : Enemy
{
	public GameObject explosionEffect;
	public ParticleSystem muzzleFlashEffect;

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
			KillEnemy();
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

			muzzleFlashEffect.Play();

			GameObject bullet = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SHOOTING_ENEMY_BULLET);
			bullet.transform.position = shootingPoint.transform.position;
			bullet.transform.rotation = shootingPoint.transform.rotation;
			bullet.SetActive(true);
		}
	}

	public override void KillEnemy()
	{
		base.KillEnemy();

		// Ship explosion
		//Instantiate(explosionEffect, transform.position, Quaternion.identity);

		GameObject explosion = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.PLASMA_EXPLOSION);

		if (explosion != null &&
			explosion.activeInHierarchy == false)
		{
			explosion.transform.position = transform.position;
			explosion.SetActive(true);
		}

		Destroy(gameObject);
	}
}
