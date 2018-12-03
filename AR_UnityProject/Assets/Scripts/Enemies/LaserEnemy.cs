using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
	public float rotationSpeed = 30f;

	[Header("Warning State")]
	public float timeToStartWarning = 3f;
	public float warningDuration = 2f;
	public float laserWarningWidth = 0.2f;

	[Header("Shooting State")]
	public float laserShootingWidth = 1f;
	public float shootingLaserDuration = 5;
	public int laserDamage = 5;

	private GameObject sparks;
	private LineRenderer laser;
	private Animator animator;

	private float timer = 0;

	private LaserState laserState;
	private enum LaserState
	{
		SPAWN,
		WARNING,
		SHOOTING_LASER,
		LEAVE
	}

	private void OnEnable()
	{
		animator = GetComponent<Animator>();

		laser = GetComponent<LineRenderer>();
		laser.SetPosition(0, transform.position);
		laser.SetPosition(1, transform.position);

		timer = 0;

		laserState = LaserState.SPAWN;

		animator.SetBool("TimeToLeave", false);
	}

	void Update()
	{
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

		UpdateLaserState();
	}


	void UpdateLaserState()
	{
		switch (laserState)
		{
			case LaserState.SPAWN:
				// Play spawning animation on Awake
				SpawnState();
				break;
			case LaserState.WARNING:
				WarningState();
				break;
			case LaserState.SHOOTING_LASER:
				ShootingLaserState();
				break;
			case LaserState.LEAVE:
				LeaveState();
				break;
		}
	}

	void SpawnState()
	{
		laser.SetPosition(1, transform.position);

		timer += Time.deltaTime;
		if (timer >= timeToStartWarning)
		{
			laserState = LaserState.WARNING;
			timer = 0;
		}
	}

	void WarningState()
	{
		// Activate warning laser
		// thin laser that doesn't cause damage to the player

		ShootLaserBeam(false, laserWarningWidth);

		timer += Time.deltaTime;
		if (timer >= warningDuration)
		{
			laserState = LaserState.SHOOTING_LASER;
			timer = 0;
		}
	}

	void ShootingLaserState()
	{
		ShootLaserBeam(true, laserShootingWidth);

		timer += Time.deltaTime;
		if (timer >= shootingLaserDuration)
		{
			laserState = LaserState.LEAVE;

			timer = 0;
		}
	}

	void LeaveState()
	{
		laser.SetPosition(1, transform.position);

		animator.SetBool("TimeToLeave", true);
	}

	void ShootLaserBeam(bool causeDamage, float laserWidth)
    {
		laser.startWidth	= laserWidth;
		laser.endWidth		= laserWidth;

		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                laser.SetPosition(1, hit.point);

				if (causeDamage == true)
				{
					if (sparks == null)
					{
						sparks = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SPARKS_EFFECT);
					}

					if (sparks.activeInHierarchy == false)
					{
						sparks.SetActive(true);
					}

					sparks.transform.position = hit.point;

					Vector3 sparksDir = transform.position - hit.point;
					sparks.transform.forward = sparksDir.normalized;

					if (hit.collider.tag == "Player")
					{
						Spaceship player = hit.collider.GetComponent<Spaceship>();

						player.TakeDamage(laserDamage);
					}
				}
			}
		}
    }

	public void DisableGameObject()
	{
		gameObject.SetActive(false);
	}
}
