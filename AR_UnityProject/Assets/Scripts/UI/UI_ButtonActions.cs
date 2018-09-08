using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ButtonActions : MonoBehaviour
{
	public GameObject controlsScreen;
	public GameObject mainMenuScreen;

	public void Play()
	{
		SceneManager.LoadScene("MainScene");
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
}
