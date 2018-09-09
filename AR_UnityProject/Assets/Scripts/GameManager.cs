using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int unlockedItems;
	public int enemiesPerSpawner = 5;
	public float timeBetweenEnemiesSpawn = 1f;

	[HideInInspector]
	public int enemiesAlive = 0;

	private int waveNumber;
	private bool startWave;
	private bool waveComplete;

	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
		unlockedItems = 1;
	}

	#endregion

	private void Start()
	{
		waveNumber = 1;
		startWave = true;
		waveComplete = false;
	}
}
