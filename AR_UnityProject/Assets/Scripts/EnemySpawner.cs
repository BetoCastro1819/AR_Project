using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;

	private float timer;
	private int spawnedEnemies;
	private float timeBetweenSpawns;
	private GameManager gm;

	private void Start()
	{
		timer = 0;
		spawnedEnemies = 0;
		gm = GameManager.GetInstance();
		timeBetweenSpawns = gm.timeBetweenEnemiesSpawn;
	}

	void Update ()
	{
		SpawnEnemies();
	}

	// TODO: Turn this method into a Coroutine
	void SpawnEnemies()
	{
		if (spawnedEnemies < gm.enemiesPerSpawner)
		{
			timer += Time.deltaTime;
			if (timer > timeBetweenSpawns)
			{
				Instantiate(enemyPrefab, transform.position, transform.rotation);
				spawnedEnemies++;
				timer = 0;
			}
		}
	}
}
