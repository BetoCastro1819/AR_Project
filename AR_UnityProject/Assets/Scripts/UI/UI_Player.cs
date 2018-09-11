using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Player : MonoBehaviour
{
	public Text playerHealth;
	public Text score;
	public Text waveNumber;
	public Text enemiesKilled;

	private GameManager gm;
	private int currentScore;
	private int currentHealth;
	private int currentWave;
	private int currentEnemiesKilled;

	private void Start()
	{
		gm = GameManager.GetInstance();

		if (score != null)
		{
			currentScore = gm.GetPlayerScore();
			score.text = "Score: " + currentScore.ToString("00000000");
		}

		if (waveNumber != null)
		{
			currentWave = gm.GetWaveNumber();
			waveNumber.text = "Wave Nro: " + currentWave.ToString("0");
		}

		if (playerHealth != null)
		{
			currentHealth = gm.GetPlayer().health;
			playerHealth.text = "Health: " + currentHealth.ToString("0");
		}

		if (enemiesKilled != null)
		{
			currentEnemiesKilled = gm.enemiesKilled;
			enemiesKilled.text = "Enemies Killed: " + currentEnemiesKilled.ToString("0");
		}
	}


	void Update ()
	{
		if (playerHealth != null)
		{
			if (currentHealth != gm.GetPlayer().health)
			{
				currentHealth = gm.GetPlayer().health;
				playerHealth.text = "Health: " + currentHealth.ToString("0");
			}
		}

		if (score != null)
		{
			if (gm.GetPlayerScore() != currentScore)
			{
				currentScore = gm.GetPlayerScore();
				score.text = "Score: " + currentScore.ToString("00000000");
			}
		}

		if (waveNumber != null)
		{
			if (currentWave != gm.GetWaveNumber())
			{
				currentWave = gm.GetWaveNumber();
				waveNumber.text = "Wave Nro: " + currentWave.ToString("0");
			}
		}

		if (enemiesKilled != null)
		{
			if (currentEnemiesKilled != gm.enemiesKilled)
			{
				currentEnemiesKilled = gm.enemiesKilled;
				enemiesKilled.text = "Enemies Killed: " + currentEnemiesKilled.ToString("0");
			}
		}
	}
}
