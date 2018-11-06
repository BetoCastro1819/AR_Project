using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public GameObject bulletPrefab;
    public ObjectsPool objectsPool;
    public Transform shootingPointLeft;
    public Transform shootingPointRight;

    public float fireRate = 0.2f;
    public float shootingForce = 50f;
    public float rotationSpeed = 20f;
    public int health = 100;

    private Rigidbody rb;
    private float timer;
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;
	}
	
	void Update ()
    {
        Rotation();

        Shoot();

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
    }

    void ManualFire()
    {
        rb.AddForce(-transform.forward * shootingForce * Time.deltaTime);

        EnableBullet(shootingPointLeft);
        EnableBullet(shootingPointRight);

        //Instantiate(bulletPrefab, shootingPointLeft.position, shootingPointLeft.rotation);
        //Instantiate(bulletPrefab, shootingPointRight.position, shootingPointRight.rotation);
    }

    void AutoFire()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            rb.AddForce(-transform.forward * shootingForce * Time.deltaTime);

            EnableBullet(shootingPointLeft);
            EnableBullet(shootingPointRight);

            //Instantiate(bulletPrefab, shootingPointLeft.position, shootingPointLeft.rotation);
            //Instantiate(bulletPrefab, shootingPointRight.position, shootingPointRight.rotation);

            timer = 0;
        }
    }

    void EnableBullet(Transform _shootingPoint)
    {
        GameObject bullet = objectsPool.GetObjectPooled();
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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
