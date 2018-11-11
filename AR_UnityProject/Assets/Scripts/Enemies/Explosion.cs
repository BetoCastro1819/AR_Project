using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public int explosionDamage = 30;
	public float explosionForce = 100f;

	private void OnTriggerEnter(Collider other)
	{
		Enemy enemy = other.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(explosionDamage);
		}

		Spaceship player = other.GetComponent<Spaceship>();
		if (player != null)
		{
			Debug.Log("Player at explosion range");
			player.TakeDamage(explosionDamage);

			Rigidbody rb = player.GetComponent<Rigidbody>();
			Vector3 pushDir = player.transform.position - transform.position;

			rb.AddForce(pushDir.normalized * explosionForce);
		}
	}
}
