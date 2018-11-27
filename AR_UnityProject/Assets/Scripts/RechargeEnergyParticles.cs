using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeEnergyParticles : MonoBehaviour
{
	public float particleSpeed = 1f;
	public float timeToGoToPosition = 2f;

	public int EnergyRechargeValue { get; set; }

	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;

	private Spaceship player;

	private float timer;

	private void Start()
	{
		player = GameManager.GetInstance().player;
		system = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[system.main.maxParticles];

		timer = 0;
	}

	private void Update()
	{
		timer += Time.unscaledDeltaTime;
		if (timer > timeToGoToPosition)
		{
			if (player != null)
			{
				ParticlesGoToPosition();
			}
		}
	}

	private void ParticlesGoToPosition()
	{
		Vector3 playerPos = player.transform.position;

		// GetParticles is allocation free because we reuse the m_Particles buffer between updates
		int numParticlesAlive = system.GetParticles(particles);

		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++)
		{
			particles[i].velocity = Vector3.Normalize(playerPos - particles[i].position) * particleSpeed * Time.unscaledDeltaTime;


			if (Mathf.RoundToInt(particles[i].position.x) == Mathf.RoundToInt(playerPos.x) &&
				Mathf.RoundToInt(particles[i].position.y) == Mathf.RoundToInt(playerPos.y))
			{
				player.RechargeEnergy(EnergyRechargeValue);
				particles[i].remainingLifetime = 0;

				//Debug.Log(player.GetEnergyBarValue());
			}
		}

		// Apply the particle changes to the particle system
		system.SetParticles(particles, numParticlesAlive);
	}
}
