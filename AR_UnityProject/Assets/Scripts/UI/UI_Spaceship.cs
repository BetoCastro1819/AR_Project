using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Spaceship : MonoBehaviour
{
    public Spaceship player;

    public Slider playerHealth;
    public Slider playerEnergy;
	public Text playerScore;

	private int previousScore;

    void Start ()
    {
        playerHealth.value = playerHealth.maxValue;
        playerEnergy.value = playerEnergy.maxValue;

		playerScore.text = player.Score.ToString("000000000");

		previousScore = player.Score;
	}

	void Update ()
    {
        playerHealth.value = player.GetHealthBarValue();
        playerEnergy.value = player.GetEnergyBarValue();

		// Only update score on PlayerUI when it has changed
		if (previousScore != player.Score)
		{
			playerScore.text = player.Score.ToString("000000000");
			previousScore = player.Score;
		}
	}
}
