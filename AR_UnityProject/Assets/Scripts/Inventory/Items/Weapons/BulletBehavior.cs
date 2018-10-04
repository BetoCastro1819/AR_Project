using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	public GameObject bloodEffect;
	public float bulletSpeed = 10f;
	public float knockbackStrength = 10f;
	public int bulletDamage = 50;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		BulletMovement();
	}

	private void BulletMovement()
	{
		if (rb != null)
			transform.position += transform.forward * bulletSpeed * Time.deltaTime;
	}

	private void DisableBullet()
	{
		gameObject.SetActive(false);
	}

	private void OnCollisionEnter(Collision obj)
	{
		Enemy enemy = obj.gameObject.GetComponent<Enemy>();

		if (enemy != null) 
		{
			enemy.KnockbackEnemy(knockbackStrength);
			enemy.TakeDamage(bulletDamage);

			float bloodYAngle = -transform.rotation.eulerAngles.y;
			Quaternion dir = Quaternion.Euler(transform.rotation.eulerAngles.x, 
											  bloodYAngle, 
											  transform.rotation.eulerAngles.z
											  );

			Instantiate(bloodEffect, transform.position, dir);
		}

		DisableBullet();
	}
}
