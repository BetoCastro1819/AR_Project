using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuScreen : MonoBehaviour
{
	[Header("On PC")]
	public GameObject spacebarUI;
	public GameObject leftArrowKeyUI;
	public GameObject rightArrowKeyUI;

	[Header("On Mobile")]
	public GameObject joystickUI;
	public GameObject shootButtonUI;

	public GameObject resetButton;
	public GameObject player;
	public Transform shipMenuPosition;

	public float timeToEnableResetButton = 5f;

	private float timer;
	private bool playerHasMoved;

	private bool onMobileDevice;
	private ShootButton shootButton;

	private void Start()
	{
		timer = 0;
		playerHasMoved = false;

		onMobileDevice = false;

#if UNITY_ANDROID
		onMobileDevice = true;
#endif

		if (onMobileDevice)
		{
			EnableMobileInput(onMobileDevice);
			shootButton = shootButtonUI.GetComponent<ShootButton>();
		}
		else
		{
			EnableMobileInput(onMobileDevice);
		}
	}

	private void Update()
	{
		if (PlayerHasMoved())
		{
			timer += Time.deltaTime;
			if (timer >= timeToEnableResetButton)
			{
				resetButton.SetActive(true);
				timer = 0;
			}
		}
	}

	bool PlayerHasMoved()
	{
		if (!onMobileDevice)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				playerHasMoved = true;
			}
		}
		else
		{
			if (shootButton.Pressed)
			{
				playerHasMoved = true;
			}
		}

		return playerHasMoved;
	}

	public void EnableMobileInput(bool onMobile)
	{
		spacebarUI.SetActive(!onMobile);
		leftArrowKeyUI.SetActive(!onMobile);
		rightArrowKeyUI.SetActive(!onMobile);

		joystickUI.SetActive(onMobile);
		shootButtonUI.SetActive(onMobile);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void Quit()
	{
		Application.Quit();
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
