using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Spaceship player;
	public GameObject gameOverScreen;
	public GameObject pauseMenuScreen;

	//[HideInInspector]
	public int enemiesAlive = 0;
	public int enemiesKilled = 0;

	private int waveNumber;
	private int waveToUnlockItem;

	private bool onMobileDevice;

	public bool GameOver { get; set; }
	public int PlayerFinalScore { get; set; }
	public bool OnMobileDevice
	{
		get { return onMobileDevice; }
	}


	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		onMobileDevice = false;
#if UNITY_ANDROID
		onMobileDevice = true;
		Application.targetFrameRate = 60;
#endif

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

		if (!GameOver && Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 0;
			pauseMenuScreen.SetActive(true);
		}
	}

    public void SetWaveNumber(int num) { waveNumber = num; }
	public int GetWaveNumber() { return waveNumber; }

	public Spaceship GetPlayer() { return player; }
}
