using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 10f;
	public int damage = 10;
	public int health = 100;
	public int pointsPerKill = 150;

	private Rigidbody rb;
	private GameManager gm;

	private void Awake()
	{
		gm = GameManager.GetInstance();
		gm.enemiesAlive++;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public void KillEnemy(GameObject enemy) 
	{
		Destroy(enemy);
		gm.enemiesAlive--;
		gm.enemiesKilled++;
		gm.SetPlayerScore(gm.GetPlayerScore() + pointsPerKill);
	}

	public void KnockbackEnemy(float strength) 
	{
		if (rb != null)
			rb.AddForce(-this.transform.forward * strength);
	}

	public void SetRigidbody(Rigidbody rigidbody) 
	{
		rb = rigidbody;
	}
}
