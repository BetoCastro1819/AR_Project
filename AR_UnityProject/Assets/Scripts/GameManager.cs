using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject gameOverScreen;
	public int unlockedItems;
	public int playerScore = 0;

	//[HideInInspector]
	public int enemiesAlive = 0;

	private int waveNumber;
	private bool gameOver;
	private Player player;

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
		player = FindObjectOfType<Player>();
	}

	private void Update()
	{
		if (player == null)
			if (gameOverScreen.activeSelf == false)
			{
				Debug.Log("GAME OVER");
				gameOver = true;
				gameOverScreen.SetActive(true);
			}
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
