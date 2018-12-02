using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    public float timeToShootLaser = 3f;
    public float laserDuration = 5;
	public int laserDamage = 5;

    private LineRenderer laser;

    private float shootLaserTimer;
    private float shootingDurationTimer;

    GameObject sparks;

	void Start ()
    {
        laser = GetComponent<LineRenderer>();

        shootLaserTimer = 0;
        shootingDurationTimer = 0;
    }

    void Update ()
    {
        shootLaserTimer += Time.deltaTime;
        if (shootLaserTimer >= timeToShootLaser)
        {
            HandleLaserDuration();
        }

        transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
    }

    void HandleLaserDuration()
    {
        ShootLaserBeam();

        shootingDurationTimer += Time.deltaTime;
        if (shootingDurationTimer >= laserDuration)
        {
            shootLaserTimer = 0;
            shootingDurationTimer = 0;
            laser.SetPosition(1, transform.position);
        }

    }

    void ShootLaserBeam()
    {
        laser.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                laser.SetPosition(1, hit.point);

                if (sparks == null)
                {
                    sparks = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SPARKS_EFFECT);
                }

                if (sparks.activeInHierarchy == false)
                {
                    sparks.SetActive(true);
                }

                sparks.transform.position = hit.point;

                Vector3 sparksDir = transform.position - hit.point;
                sparks.transform.forward = sparksDir.normalized;

                if (hit.collider.tag == "Player")
                {
                    Spaceship player = hit.collider.GetComponent<Spaceship>();

                    player.TakeDamage(laserDamage);
                }
            }
        }
    }
}
