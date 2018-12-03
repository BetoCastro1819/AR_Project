using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuScreen : MonoBehaviour
{
	public GameObject resetButton;
	public GameObject player;
	public Transform shipMenuPosition;

	public float timeToEnableResetButton = 5f;

	public GameObject controlsScreen;
	public GameObject mainMenuScreen;

	private float timer;
	private bool playerHasMoved;

	private void Start()
	{
		timer = 0;
		playerHasMoved = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerHasMoved = true;
		}

		if (playerHasMoved)
		{
			timer += Time.deltaTime;
			if (timer >= timeToEnableResetButton)
			{
				resetButton.SetActive(true);
				timer = 0;
			}
		}
	}

	public void Play(string sceneToLoad)
	{
		SceneManager.LoadScene(sceneToLoad);
	}

	public void Controls()
	{
		controlsScreen.SetActive(true);
		mainMenuScreen.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Back()
	{
		controlsScreen.SetActive(false);
		mainMenuScreen.SetActive(true);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}

	public void Replay()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void ResetShipPosition()
	{
		player.transform.position = shipMenuPosition.transform.position;
		Rigidbody playerRb = player.GetComponent<Rigidbody>();
		playerRb.velocity = Vector3.zero;

		playerHasMoved = false;
		resetButton.SetActive(false);
	}
}
