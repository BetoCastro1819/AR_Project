using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
	public void OpenScene(string sceneName)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneName);
	}

	public void Resume()
	{
		Time.timeScale = 1.0f;
		gameObject.SetActive(false);
	}
}
