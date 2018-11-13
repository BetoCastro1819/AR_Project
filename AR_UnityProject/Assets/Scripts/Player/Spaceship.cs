using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public int maxHealth = 100;
    public float rotationSpeed = 20f;

    [Header("Shooting")]
    public Transform shootingPointLeft;
    public Transform shootingPointRight;
    public float shotsPerSecond = 10;
    public float shootingForce = 50f;

    private float fireRateTimer;
    private bool isShooting;

    [Header("Energy")]
    public int maxEnergy = 100;
    public int energyCostPerShot = 5;
    public int valueForAutoRecharge = 1;
    public float rechargeRate = 0.1f;
    public float timeTostartRecharge = 0.5f;

    private float rechargeRateTimer;
    private float startEnergyRechargeTimer;
    
    public int Health { get; set; }
    public int Energy { get; set; }

    private Rigidbody rb;
    
	void Start ()
    {
        // Get rigidbody for adding force when shooting
        rb = GetComponent<Rigidbody>();

        // Initiaze timer at 0
        startEnergyRechargeTimer = 0;
        fireRateTimer = 0;
        rechargeRateTimer = 0;

        // Initialize bar´s values
        Health = maxHealth;
        Energy = maxEnergy;

        isShooting = false;
    }

    void Update ()
    {
        Rotation();
        Shoot();

        if (!isShooting && Energy < maxEnergy)
        {
            startEnergyRechargeTimer += Time.deltaTime;
            if (startEnergyRechargeTimer > timeTostartRecharge)
            {
                AutomaticEnergyRecharge();
            }
        }

        // Debug some stuff
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddEnergy(20);
            //AddHeath(20);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ConsumeEnergy(20);
            //TakeDamage(20);
        }
    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ManualFire();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            AutoFire();
        }
        else
        {
            isShooting = false;
        }
    }

    void ManualFire()
    {
        if (Energy > 0)
        {
            startEnergyRechargeTimer = 0;

            isShooting = true;

            rb.AddForce(-transform.forward * shootingForce * Time.deltaTime);

            EnableBullet(shootingPointLeft);
            EnableBullet(shootingPointRight);

            ConsumeEnergy(energyCostPerShot);
        }
        else
        {
            // TODO: tell the player he is out of energy
        }
    }

    void AutoFire()
    {
        if (Energy > 0)
        {
            startEnergyRechargeTimer = 0;

            isShooting = true;

            //fireRateTimer += Time.deltaTime;
            if (Time.time >= fireRateTimer)
            {
                fireRateTimer = Time.time + 1 / shotsPerSecond;

                rb.AddForce(-transform.forward * shootingForce * Time.deltaTime);

                EnableBullet(shootingPointLeft);
                EnableBullet(shootingPointRight);

                //fireRateTimer = 0;

                ConsumeEnergy(energyCostPerShot);
            }
        }
        else
        {
            // TODO: tell the player he is out of energy
        }
    }

    void EnableBullet(Transform _shootingPoint)
    {
		GameObject bullet = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.PLAYER_BULLET); 
        if (bullet != null)
        {
            bullet.transform.position = _shootingPoint.position;
            bullet.transform.rotation = _shootingPoint.rotation;
            bullet.SetActive(true);
        }
    }

    void KillPlayer()
    {
    }

    //------------------------------ ENERGY BAR ------------------------------- //
    private void AutomaticEnergyRecharge()
    {
        rechargeRateTimer += Time.deltaTime;
        if (Energy < maxEnergy && 
            rechargeRateTimer > rechargeRate)
        {
            Energy += valueForAutoRecharge;
            rechargeRateTimer = 0;
        }
        else
        {
            startEnergyRechargeTimer = 0;
        }
    }

    public void AddEnergy(int energyToAdd)
    {
        if (Energy + energyToAdd <= maxEnergy)
        {
            Energy += energyToAdd;
        }
        else
        {
            Energy = maxEnergy;
        }
        //Debug.Log("Energy: " + Energy);
        //Debug.Log("Energy Bar: " + GetEnergyBarValue());
    }

    public void ConsumeEnergy(int energyConsumed)
    {
        if (Energy - energyConsumed >= 0)
        {
            Energy -= energyConsumed;
        }
        else
        {
            Energy = 0;
        }
        //Debug.Log("Energy: " + Energy);
        //Debug.Log("Energy Bar: " + GetEnergyBarValue());
    }

    public float GetEnergyBarValue()
    {
        // maxEnergy = 100%
        // currentEnergy = ?

        float currentEnergyPercentage = (Energy * 100) / maxEnergy;

        // Slider values goes from 0.0 to 1.0
        // So we need de decimal version of the % obtained
        return currentEnergyPercentage / 100;
    }
    //---------------------------------------------------------------------------- //



    //------------------------------ PLAYER HEALTH ------------------------------- //
    public void AddHeath(int healthToAdd)
    {
        if (Health + healthToAdd <= maxHealth)
        {
            Health += healthToAdd;
        }
        else
        {
            Health = maxHealth;
        }
        //Debug.Log("Health: " + Health);
        //Debug.Log("Health Bar: " + GetHealthBarValue());
    }

    public void TakeDamage(int damage)
    {
        if (Health - damage >= 0)
        {
            Health -= damage;
        }
        else
        {
            Health = 0;
        }
        //Debug.Log("Health: " + Health);
        //Debug.Log("Health Bar: " + GetHealthBarValue());
    }

    public float GetHealthBarValue()
    {
        // maxHealth = 100%
        // currentHealth = ?

        float currentHealthPercentage = (Health * 100) / maxHealth;

        // Slider values goes from 0.0 to 1.0
        // So we need de decimal version of the % obtained
        return currentHealthPercentage / 100;
    }
	//---------------------------------------------------------------------------- //

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject.name);
	}
}
