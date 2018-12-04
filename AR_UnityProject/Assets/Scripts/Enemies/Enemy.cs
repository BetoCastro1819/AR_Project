using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public List<GameObject> rechargeParticleList;
	public int health = 100;
    public int onKillParticlesRecharge = 10;
	public int onKillScore = 150;

	protected Rigidbody rb;
	protected GameManager gm;
	protected Spaceship player;
	protected GameObject particlesFromPool;

	public virtual void Start()
	{
		gm = GameManager.GetInstance();
		rb = GetComponent<Rigidbody>();
		player = gm.GetPlayer();
		gm.enemiesAlive++;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public virtual void KillEnemy()
	{
		//energyParticles = Instantiate(rechargeParticleList[randomParticles], transform.position, Quaternion.identity);

		CameraShake.GetInstance().Shake();

		int randomParticles = Random.Range(0, 2);
		if (randomParticles == 0)
		{
			particlesFromPool = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.ENERGY_PARTICLES);
		}
		else if (randomParticles == 1)
		{
			particlesFromPool = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.HEALTH_PARTICLES);
		}

		if (particlesFromPool != null &&
			particlesFromPool.activeInHierarchy == false)
		{
			particlesFromPool.SetActive(true);
			particlesFromPool.transform.position = transform.position; 
		}

		// Particles from Enemy parent class
		RechargeParticles rechargeParticles = particlesFromPool.GetComponent<RechargeParticles>();

        if (rechargeParticles != null)
        {
            rechargeParticles.RechargeValue = onKillParticlesRecharge;
            Debug.Log("Recharge value: " + rechargeParticles.RechargeValue);
        }

        //Destroy(gameObject);
    }

	private void OnDestroy()
	{
		player.Score += onKillScore;
		gm.enemiesAlive--;
		gm.enemiesKilled++;
	}
}
