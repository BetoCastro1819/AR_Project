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

	private void Awake()
	{
		GameManager.GetInstance().enemiesAlive++;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public void KillEnemy(GameObject enemy) 
	{
		Destroy(enemy);
		GameManager.GetInstance().enemiesAlive--;
		GameManager.GetInstance().playerScore += pointsPerKill;
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
