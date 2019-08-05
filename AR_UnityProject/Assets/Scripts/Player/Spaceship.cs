using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	public GameObject explosionEffect;
	public GameObject specialPower;
	public GameObject specialPowerUI;

	public int maxHealth = 100;
	public float rotationSpeed = 20f;

	[Header("Mobile Input")]
	public Joystick joystick;
	public ShootButton shootButton;
	public GameObject pauseMenuButton;
	public GameObject activatePowerButton;

	private bool onMobileDevice;

    [Header("Shooting")]
    public Transform shootingPointLeft;
	public ParticleSystem shootingEffectLeft;
	public Transform shootingPointRight;
	public ParticleSystem shootingEffectRight;
	public float shotsPerSecond = 10;
    public float shootingForce = 50f;

    private float fireRateTimer;


    [Header("Energy")]
    public int maxEnergy = 100;
	
    private float rechargeRateTimer;
    private float startEnergyRechargeTimer;

	private float specialPowerDuration;
	private bool specialPowerIsActive;
	private float specialPowerTimer;
	private bool powerActivated;


	public int Health	{ get; set; }
    public int Energy	{ get; set; }
	public int Score	{ get; set; }

    private Rigidbody rb;

	void Start ()
    {
		onMobileDevice = false;
#if UNITY_ANDROID
		onMobileDevice = true;
		Application.targetFrameRate = 60;
#endif

		if (joystick != null && shootButton != null && pauseMenuButton != null)
		{
			joystick.gameObject.SetActive(onMobileDevice);
			shootButton.gameObject.SetActive(onMobileDevice);
			pauseMenuButton.SetActive(onMobileDevice);
		}

		rb = GetComponent<Rigidbody>();

        startEnergyRechargeTimer = 0;
        fireRateTimer = 0;
        rechargeRateTimer = 0;
		specialPowerTimer = 0;

        Health = maxHealth;
        Energy = maxEnergy;
		Score = 0;

		if (specialPower != null)
		{
			specialPowerDuration = specialPower.GetComponent<ParticleSpread>().explosionDuration;
			specialPowerIsActive = false;
		}

		Energy = 0;
		powerActivated = false;
	}

	void FixedUpdate ()
    {
        Rotation();
		Shoot();

		if (Energy >= maxEnergy && specialPower != null)
        {
			specialPowerUI.SetActive(true);

			if (onMobileDevice)
				activatePowerButton.SetActive(true);

			if (Input.GetKeyDown(KeyCode.LeftShift) || powerActivated)
			{
				activatePowerButton.SetActive(false);

				Energy = 0;

				if (specialPower != null)
				{
					specialPower.transform.position = transform.position;
					specialPower.SetActive(true);

					specialPowerIsActive = true;
					specialPowerUI.SetActive(false);
				}
				rb.velocity = Vector3.zero;
			}
		}


		if (specialPowerIsActive == true)
		{
			specialPowerTimer += Time.deltaTime;
			if (specialPowerTimer >= specialPowerDuration)
			{
				specialPowerIsActive = false;
				specialPowerTimer = 0;
			}
		}

        // Debug some stuff
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RechargeEnergy(20);
            //AddHeath(20);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ConsumeEnergy(20);
            //TakeDamage(20);
        }

		if (Health <= 0)
		{
			KillPlayer();
		}
    }

    void Rotation()
    {
		if (onMobileDevice)
		{
			float x = joystick.Horizontal;
			float y = joystick.Vertical;

			if (x != 0 && y != 0)
			{
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,
													Mathf.Atan2(x, y) * Mathf.Rad2Deg,
													transform.eulerAngles.z);
			}
		}
		else
		{
			if (Input.GetKey(KeyCode.RightArrow))
				transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
			if (Input.GetKey(KeyCode.LeftArrow))
				transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
		}
	}

    void Shoot()
    {
		if (!onMobileDevice)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				ManualFire();
			else if (Input.GetKey(KeyCode.Space))
				AutoFire();
		}
		else
		{
			if (shootButton.Pressed)
				AutoFire();
		}
	}

	void ManualFire()
    {
		startEnergyRechargeTimer = 0;

		rb.AddForce(-transform.forward * shootingForce);

		EnableBullet(shootingPointLeft);
		shootingEffectLeft.Play();

		EnableBullet(shootingPointRight);
		shootingEffectRight.Play();
	}

	void AutoFire()
    {
		startEnergyRechargeTimer = 0;

		if (Time.time >= fireRateTimer)
		{
			fireRateTimer = Time.time + 1 / shotsPerSecond;

			rb.AddForce(-transform.forward * shootingForce);

			EnableBullet(shootingPointLeft);
			shootingEffectLeft.Play();

			EnableBullet(shootingPointRight);
			shootingEffectRight.Play();
		}
	}

    void EnableBullet(Transform _shootingPoint)
    {
		GameObject bullet = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.PLAYER_BULLET);
		if (bullet != null)
		{
			if (bullet.activeInHierarchy == false)
			{
				bullet.GetComponent<BulletBehavior>().SetOriginPos(transform.position);

				bullet.transform.position = _shootingPoint.position;
				bullet.transform.rotation = _shootingPoint.rotation;
				bullet.SetActive(true);
			}
		}
	}

    void KillPlayer()
    {
		joystick.gameObject.SetActive(false);
		shootButton.gameObject.SetActive(false);

		GameObject explosion = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.EXPLOSION);
		if (explosion != null)
		{
			explosion.transform.position = transform.position;
			explosion.SetActive(true);
		}

		GameManager.GetInstance().PlayerFinalScore = Score;
		Destroy(gameObject);
	}

    public void RechargeEnergy(int energyToAdd)
    {
		if (specialPowerIsActive == false)
		{
			if (Energy + energyToAdd <= maxEnergy)
				Energy += energyToAdd;
			else
				Energy = maxEnergy;
		}
	}

    public void ConsumeEnergy(int energyConsumed)
    {
        if (Energy - energyConsumed >= 0)
            Energy -= energyConsumed;
        else
            Energy = 0;
    }

    public float GetEnergyBarValue()
    {
        float currentEnergyPercentage = (Energy * 100) / maxEnergy;
        return currentEnergyPercentage / 100;
    }

	public void AddHeath(int healthToAdd)
    {
        if (Health + healthToAdd <= maxHealth)
            Health += healthToAdd;
        else
            Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Health - damage >= 0)
            Health -= damage;
        else
            Health = 0;

		CameraShake.GetInstance().Shake();
    }

    public float GetHealthBarValue()
    {
        float currentHealthPercentage = (Health * 100) / maxHealth;
        return currentHealthPercentage / 100;
    }

	public void ActivatePower()
	{
		powerActivated = true;
	}
}
