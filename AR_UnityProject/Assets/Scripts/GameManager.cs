using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject gameOverScreen;
	public int unlockedItems;

	//[HideInInspector]
	public int enemiesAlive = 0;
	public int enemiesKilled = 0;

	private Player player;
	private bool gameOver;
	private int waveNumber;
	private int playerScore;
    
	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
		player = FindObjectOfType<Player>();
	}

	#endregion

	private void Start()
	{
		gameOver = false;
		//unlockedItems = 1;
		playerScore = 0;
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

    public void SetGameOver(bool setBool) { gameOver = setBool; }

    public void SetWaveNumber(int num) { waveNumber = num; }
	public int GetWaveNumber() { return waveNumber; }

    public int GetPlayerScore() { return playerScore; }
	public void SetPlayerScore(int score) { playerScore = score; }
	public Player GetPlayer() { return player; }
}
