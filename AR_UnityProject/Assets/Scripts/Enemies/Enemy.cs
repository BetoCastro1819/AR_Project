using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health = 100;

	protected Rigidbody rb;
	protected GameManager gm;
	protected Spaceship player;

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		rb = GetComponent<Rigidbody>();
		player = gm.GetPlayer();
	}

	private void Update()
	{
		if (health <= 0)
		{
			// Kill enemy
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}
}
