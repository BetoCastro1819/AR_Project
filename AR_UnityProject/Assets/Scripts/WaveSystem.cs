using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
	public GameObject enemyPrefab;
	public List<Transform> spawnPoints;
	public float waveRate = 3f;
	public float spawnRate = 1f;
	public int numberOfEnemies = 5;

	private WaveState waveState;
	private bool courutineStated;
	private float timer;
	private int waveNumber;

	private enum WaveState
	{
		COUNTDOWN,
		SPAWNING,
		WAITING_FOR_PLAYER
	}

	void Start()
	{
		waveNumber = 1;
		timer = waveRate;
		waveState = WaveState.COUNTDOWN;
		courutineStated = false;
	}

	void Update()
	{
		WaveFSM();
	}

	void WaveFSM()
	{
		switch (waveState)
		{
			case WaveState.COUNTDOWN:
				WaveCountdown();
				break;
			case WaveState.SPAWNING:
				if (!courutineStated)
					StartCoroutine(WaveSpawning());
				break;
			case WaveState.WAITING_FOR_PLAYER:
				CheckForAliveEnemies();
				break;
		}
	}

	void WaveCountdown()
	{
		timer += Time.deltaTime;
		if (timer > waveRate)
		{
			// Start spawning enemies
			waveState = WaveState.SPAWNING;
			timer = 0;
		}
	}

	IEnumerator WaveSpawning()
	{
		courutineStated = true;

		int spawnPointIndex = 0;
		for (int i = 0; i < numberOfEnemies; i++)
		{
			Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
			spawnPointIndex++;

			if (spawnPointIndex > spawnPoints.Count - 1)
				spawnPointIndex = 0;

			yield return new WaitForSeconds(spawnRate);
		}

		courutineStated = false;
		waveState = WaveState.WAITING_FOR_PLAYER;
	}

	void CheckForAliveEnemies()
	{
		if (GameManager.GetInstance().enemiesAlive <= 0)
		{
			timer += Time.deltaTime;
			if (timer > waveRate)
			{
				waveState = WaveState.SPAWNING;
				timer = 0;
				waveNumber++;
				GameManager.GetInstance().SetWaveNumber(waveNumber);
			}
		}
	}
}
