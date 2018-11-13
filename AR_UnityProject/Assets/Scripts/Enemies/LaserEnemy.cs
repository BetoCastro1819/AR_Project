using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    private LineRenderer laser;

    GameObject sparks;

	public override void Start ()
    {
        base.Start();

        laser = GetComponent<LineRenderer>();
    }

    void Update ()
    {
        ShootLaserBeam();
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

                    player.TakeDamage(1);
                }
            }
        }
    }
}
