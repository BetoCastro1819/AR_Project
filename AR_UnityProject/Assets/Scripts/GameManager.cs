using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int unlockedItems;
	public int playerScore = 0;

	[HideInInspector]
	public int enemiesAlive = 0;

	private int waveNumber;
	private bool gameOver;

	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
	}

	#endregion

	private void Start()
	{
		gameOver = false;
		unlockedItems = 1;

	}

	public void SetWaveNumber(int num)
	{
		waveNumber = num;
	}

	public void SetGameOver(bool setBool)
	{
		gameOver = setBool;
	}
}
