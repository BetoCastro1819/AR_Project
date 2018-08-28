using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 10f;
	public int health = 100;

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public void KillEnemy(GameObject enemy) 
	{
		Destroy(enemy);
	}
}
