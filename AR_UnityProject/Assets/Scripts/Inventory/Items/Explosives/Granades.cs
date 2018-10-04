using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granades : Item
{
	public GameObject granadePrefab;
	public float fowardForce = 20f;
	public float upwardsForce = 4f;

	public override void UseItem()
	{
		base.UseItem();

		if (Input.GetKeyDown(KeyCode.Space))
			ThrowGranade();
	}

	void ThrowGranade()
	{
		if (currentAmmo > 0)
		{
			GameObject granade = Instantiate(granadePrefab, transform.position, transform.rotation);
			Rigidbody rb = granade.GetComponent<Rigidbody>();
			rb.AddForce((transform.forward * fowardForce) + (transform.up * upwardsForce));

			currentAmmo--;
		}
	}
}
