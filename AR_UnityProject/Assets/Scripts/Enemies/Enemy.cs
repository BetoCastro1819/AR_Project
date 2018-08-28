using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 10f;
	public int health = 100;

	private Rigidbody rb;

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public void KillEnemy(GameObject enemy) 
	{
		Destroy(enemy);
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
