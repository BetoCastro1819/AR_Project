using UnityEngine;

public class ShootingEnemy : Enemy
{
	[Header("Movement")]
	public float accelerationSpeed = 5f;
	public float maxVelocity = 5f;
	public float accelerateForSeconds = 3f;
	public float idleForSeconds = 3f;

	[Header("Shooting")]
	public float fireRate = 0f;
	public int bulletDamage = 5;

	private float accelerationTimer;
	private float idleTimer;

	private bool timeToAccelerate;

	public override void Start ()
	{
		base.Start();

		accelerationTimer = 0;
		idleTimer = 0;
		timeToAccelerate = true;
	}

	void Update()
	{
		if (player != null)
		{
			transform.LookAt(player.transform.position);

			if (timeToAccelerate)
			{
				Accelerate();
			}
			else
			{
				Idle();
			}
		}
	}

	void Accelerate()
	{
		Vector3 dir = player.transform.position - transform.position;

		rb.AddForce(transform.forward * accelerationSpeed * Time.fixedDeltaTime);

		accelerationTimer += Time.deltaTime;
		if (accelerationTimer >= accelerateForSeconds)
		{
			timeToAccelerate = false;
			accelerationTimer = 0;
		}
	}

	void Idle()
	{
		idleTimer += Time.deltaTime;
		if (idleTimer >= idleForSeconds)
		{
			timeToAccelerate = true;
			idleTimer = 0;
		}
	}
}
