using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Spaceship player;

	public Text unlockedItemText;
	public GameObject newItemScreen;

	public GameObject gameOverScreen;

	//[HideInInspector]
	public int enemiesAlive = 0;
	public int enemiesKilled = 0;

	private int waveNumber;
    private int waveToUnlockItem;

	public bool GameOver { get; set; }
	public int PlayerFinalScore { get; set; }

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
		GameOver = false;

		PlayerFinalScore = 0;
	}

	private void Update()
	{
        if (player == null)
        {
            if (gameOverScreen.activeSelf == false)
            {
                Debug.Log("GAME OVER");
                GameOver = true;
                gameOverScreen.SetActive(true);
            }
        }

		/*
        if (waveNumber >= waveToUnlockItem)
        {
            waveToUnlockItem = waveNumber + wavesToUnlockNewItem;
			if (unlockedItems < inventory.playerItems.Count)
			{
				unlockedItemText.text = inventory.playerItems[unlockedItems].name;
				newItemScreen.SetActive(true);
				unlockedItems++;
				Debug.Log("UNLOCKED: " + inventory.playerItems[unlockedItems - 1].name);
			}
		}

		if (newItemScreen.activeSelf == true)
		{
			// Pause game until player presses ENTER
			Time.timeScale = 0; 
			if (Input.GetKeyDown(KeyCode.Space))
			{
				newItemScreen.SetActive(false);
				Time.timeScale = 1;
			}
		}
		*/
    }

    public void SetWaveNumber(int num) { waveNumber = num; }
	public int GetWaveNumber() { return waveNumber; }

	public Spaceship GetPlayer() { return player; }
}
