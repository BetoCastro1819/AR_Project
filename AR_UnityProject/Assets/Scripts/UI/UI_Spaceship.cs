using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Spaceship : MonoBehaviour
{
    public Spaceship player;

    public Slider playerHealth;
    public Slider playerEnergy;

    void Start ()
    {
        playerHealth.value = playerHealth.maxValue;
        playerEnergy.value = playerEnergy.maxValue;
	}
	
	void Update ()
    {
        playerHealth.value = player.GetHealthBarValue();
        playerEnergy.value = player.GetEnergyBarValue();
    }
}
