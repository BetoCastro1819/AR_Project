using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
	private ParticleSystem particles;
	private float timeToDestroy;

	void Start ()
	{
		particles = GetComponent<ParticleSystem>();
		timeToDestroy = particles.main.duration;
		Destroy(gameObject, timeToDestroy);
	}
}
