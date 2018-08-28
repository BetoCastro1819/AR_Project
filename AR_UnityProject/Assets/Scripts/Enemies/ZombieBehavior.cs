using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : Enemy
{
	private Player player;
	private Rigidbody rb;
	private ZombieState state;
	private bool playerFound = false;

	enum ZombieState 
	{
		IDLE,
		MOVING,
		ATTACKING
	}

	void Awake () 
	{
		// TODO: Make EnemyManager class
		player = FindObjectOfType<Player>();
		if (player != null) 
			playerFound = true;

		state = ZombieState.IDLE;

		rb = GetComponent<Rigidbody>();
		SetRigidbody(rb);

	}

	void Update () 
	{
		if (health <= 0)
			KillEnemy(this.gameObject);

		ZombieFSM(state);
	}

	void ZombieFSM(ZombieState zombieState) 
	{
		switch (zombieState) {
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
		}
	}

	void Movement() 
	{
		Vector3 movement = this.transform.forward * speed * Time.deltaTime;
		transform.LookAt(player.transform.position);

		transform.position += movement;

		// Make transition to ATTACKING state, when pleayer is near
	}

	void Attack() 
	{
		// Melee zombie attack
		// Attack when player is near attackRadius
	}
}
