using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : Enemy
{
	public float pushPlayerForce = 100f;

	private Player player;
	private Rigidbody zombieRb;
	private ZombieState state;
	private bool playerFound = false;

	enum ZombieState 
	{
		IDLE,
		MOVING,
		ATTACKING,
		DEAD
	}

	void Start() 
	{
		// TODO: Make EnemyManager class to just look for player ONCE, and not per enemy
		player = FindObjectOfType<Player>();
		if (player != null) 
			playerFound = true;

		state = ZombieState.IDLE;

		zombieRb = GetComponent<Rigidbody>();
		SetRigidbody(zombieRb);

	}

	void Update () 
	{
		if (transform.position.y < -3)
			KillEnemy(gameObject);

		if (player != null)
			ZombieFSM(state);
	}

	void ZombieFSM(ZombieState zombieState) 
	{
		switch (zombieState) 
        {
			case ZombieState.IDLE:
				if (playerFound) 
					state = ZombieState.MOVING;
				
				break;
			case ZombieState.MOVING:
				Movement();
				break;
			case ZombieState.ATTACKING:
				Attack();
				break;
			case ZombieState.DEAD:
				KillZombie();
				break;
		}
	}

	void Movement() 
	{
		Vector3 movement = this.transform.forward * speed * Time.deltaTime;
		transform.LookAt(player.transform.position);

		transform.position += movement;

		if (health <= 0)
			state = ZombieState.DEAD;		

		// Make transition to ATTACKING state, when pleayer is near
	}

	void KillZombie()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.None;
		Destroy(gameObject, 2f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Player player = collision.gameObject.GetComponent<Player>();
		if (player != null)
			player.TakeDamage(damage);

		RocketBehavior rocket = collision.gameObject.GetComponent<RocketBehavior>();
		if(rocket != null) 
			TakeDamage(rocket.rocketDamage);
	}

	void Attack() 
	{
		// Melee zombie attack
		// Attack when player is near attackRadius
	}
}
