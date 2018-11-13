using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject rechargeEnergyParticles;
	public int health = 100;

	protected Rigidbody rb;
	protected GameManager gm;
	protected Spaceship player;
	protected GameObject energyParticles;

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		rb = GetComponent<Rigidbody>();
		player = gm.GetPlayer();
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public virtual void KillEnemy()
	{
		// Call from pool later on
		energyParticles = Instantiate(rechargeEnergyParticles, transform.position, Quaternion.identity);
	}
}
