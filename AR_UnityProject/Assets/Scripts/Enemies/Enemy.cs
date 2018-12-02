using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public List<GameObject> rechargeParticleList;
	public int health = 100;
    public int onKillParticlesRecharge = 10;

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
		int randomParticles = Random.Range(0, rechargeParticleList.Count);

		energyParticles = Instantiate(rechargeParticleList[randomParticles], transform.position, Quaternion.identity);

        // Particles from Enemy parent class
        RechargeParticles rechargeParticles = energyParticles.GetComponent<RechargeParticles>();

        if (rechargeParticles != null)
        {
            rechargeParticles.RechargeValue = onKillParticlesRecharge;
            Debug.Log("Recharge value: " + rechargeParticles.RechargeValue);
        }

        Destroy(gameObject);
    }
}
