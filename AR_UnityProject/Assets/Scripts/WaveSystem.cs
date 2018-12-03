using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
	public GameObject laserWarningUI;

	public List<GameObject> listOfEnemies;
	public List<GameObject> listOfLaserRobots;
	public List<Transform> spawnPoints;

	public int wavesForLaserEvent = 2;
	public int laserRobotsToActivate = 1;

	//public GameObject waveCountdown;
	//public Text waveCountdownText;

	public float waveRate = 3f;
	public float spawnRate = 1f;
	public int numberOfEnemies = 5;
	public int addEnemiesPerWave = 3;

	private bool coroutineStarted;
	private int waveNumber;
	private int laserEventAtWave;

	private float laserRobotsDuration;
	private float timer;

	public WaveState waveState;
	public enum WaveState
	{
		COUNTDOWN,
		SPAWNING,
		LASER_ROBOTS,
		WAITING_FOR_PLAYER
	}

	void Start()
	{
		waveNumber = 1;
		laserEventAtWave = wavesForLaserEvent + waveNumber;
		timer = 0;
		waveState = WaveState.COUNTDOWN;
		coroutineStarted = false;

		LaserEnemy laserRobot = listOfLaserRobots[0].GetComponent<LaserEnemy>();
		float animationOffset = 1f;
		laserRobotsDuration = laserRobot.warningDuration + laserRobot.shootingLaserDuration + animationOffset;

		//waveCountdown.SetActive(false);
    }

    void Update()
	{
		if (GameManager.GetInstance().GameOver == false)
		{
			WaveFSM();
		}
	}

	void WaveFSM()
	{
		switch (waveState)
		{
			case WaveState.COUNTDOWN:
				WaveCountdown();
				break;
			case WaveState.SPAWNING:
				if (!coroutineStarted)
				{
					StartCoroutine(WaveSpawning());
				}
				break;
			case WaveState.LASER_ROBOTS:
				LaserRobotsEvent();
				break;
			case WaveState.WAITING_FOR_PLAYER:
				CheckForAliveEnemies();
				break;
		}
	}

	void WaveCountdown()
	{
		timer += Time.deltaTime;
		if (timer >= 1)
		{
			if (waveNumber >= laserEventAtWave)
			{
				waveState = WaveState.LASER_ROBOTS;
				laserEventAtWave = waveNumber + wavesForLaserEvent;
			}
			else
			{
				waveState = WaveState.SPAWNING;
			}

			timer = 0;
		}
        //waveCountdown.SetActive(true);
        //waveCountdownText.text = timer.ToString("0");
	}

	IEnumerator WaveSpawning()
	{
		coroutineStarted = true;
        //waveCountdown.SetActive(false);

        int spawnPointIndex = 0;
		for (int i = 0; i < numberOfEnemies; i++)
		{
			int randomEnemyIndex = Random.Range(0, listOfEnemies.Count);
			Instantiate(listOfEnemies[randomEnemyIndex], spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);

			spawnPointIndex++;
			if (spawnPointIndex > spawnPoints.Count - 1)
				spawnPointIndex = 0;

			yield return new WaitForSeconds(spawnRate);
		}
		coroutineStarted = false;
		waveState = WaveState.WAITING_FOR_PLAYER;
	}

	void LaserRobotsEvent()
	{
		laserWarningUI.SetActive(true);

		for (int i = 0; i < laserRobotsToActivate; i++)
		{
			if (listOfLaserRobots[i].activeInHierarchy == false)
			{
				listOfLaserRobots[i].SetActive(true);
			}
		}

		timer += Time.deltaTime;
		if (timer >= laserRobotsDuration)
		{
			waveState = WaveState.COUNTDOWN;

			laserWarningUI.SetActive(false);
			timer = 0;

			laserRobotsToActivate++;

			if (laserRobotsToActivate > listOfLaserRobots.Count)
			{
				laserRobotsToActivate = listOfLaserRobots.Count;
			}
		}
	}

	void CheckForAliveEnemies()
	{
		if (GameManager.GetInstance().enemiesAlive <= 0)
		{
			waveState = WaveState.COUNTDOWN;
            timer = waveRate;
			waveNumber++;
			numberOfEnemies += addEnemiesPerWave;
			GameManager.GetInstance().SetWaveNumber(waveNumber);
		}
	}
}
