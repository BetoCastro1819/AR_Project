using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : Item
{
	public GameObject bulletPrefab;
    public Type_Of_Weapon weaponType;
    public Transform shootingPoint;
    public bool autoFire = true;

    public override void UseItem()
    {
        base.UseItem();

        if (Input.GetKey(KeyCode.Space))
            Shoot(weaponType);
    }

    void Shoot(Type_Of_Weapon type)
    {
        switch (type)
        {
            case Type_Of_Weapon.GUN:
				Gun();
                break;
            case Type_Of_Weapon.UZI:
                Debug.Log("Im an UZI");
                break;
            case Type_Of_Weapon.ASSAULT_RIFLE:
                Debug.Log("Im an ASSAULT RIFLE");
                break;
            case Type_Of_Weapon.ROCKET_LAUNCHER:
                Debug.Log("Im a ROCKET LAUNCHER");
                break;
        }
    }

    public enum Type_Of_Weapon
    {
        GUN,
        UZI,
        ASSAULT_RIFLE,
        ROCKET_LAUNCHER
    }

    void Gun()
    {
		if (autoFire)
		{
			Debug.Log("Im an AUTOMATIC-GUN");
			RaycastShooting();

		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
				//RaycastShooting();
			}
		}
	}

	void RaycastShooting()
	{
		//Debug.Log("Im a MANUAL-GUN");

		RaycastHit hit;

		if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, 50f))
		{
			if (hit.collider.tag == "Object")
			{
				Destroy(hit.collider.gameObject);
				Debug.Log("I shot a " + hit.collider.name);
			}
		}
	}
}
