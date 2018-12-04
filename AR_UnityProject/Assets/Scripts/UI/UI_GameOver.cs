using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
	public Text finalScore;
	public Text enemiesKilled;

	void Awake()
	{
		finalScore.text = GameManager.GetInstance().PlayerFinalScore.ToString("0");
		enemiesKilled.text = GameManager.GetInstance().enemiesKilled.ToString("0");
	}

	public void GoToScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
