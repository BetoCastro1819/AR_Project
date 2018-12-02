using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
	private ParticleSystem particles;
	private float timeToDisableParticles;

	private float timer;

	void Start ()
	{
		particles = GetComponent<ParticleSystem>();
		timeToDisableParticles = particles.main.startLifetimeMultiplier;
		timer = 0;
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= timeToDisableParticles)
		{
			gameObject.SetActive(false);
			timer = 0;
		}
	}
}
