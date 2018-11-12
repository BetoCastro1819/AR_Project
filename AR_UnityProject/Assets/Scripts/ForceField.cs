using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
	public int damagePlayer = 5;
	public float damageRate = 0.5f;
	public float pushPlayerForce = 1000f;

	private Spaceship player;
	private float damagePlayerTimer;

	void Start ()
	{
		player = FindObjectOfType<Spaceship>();
		damagePlayerTimer = damageRate;
	}
	
	void Update ()
	{
		// Also check if GAME OVER
		if (player == null)
		{
			player = FindObjectOfType<Spaceship>();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Vector3 zeroXZ = new Vector3(0, transform.position.y, 0);


			Vector3 pointOfPlayerCollision = collision.contacts[0].point;


			Vector3 dirToPushPlayer =  zeroXZ - pointOfPlayerCollision;


			//GameObject sparks = Instantiate(sparksEffect, pointOfPlayerCollision, Quaternion.identity);

			GameObject sparksEffect = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SPARKS_EFFECT);
			sparksEffect.transform.position = pointOfPlayerCollision;
			sparksEffect.transform.forward = dirToPushPlayer;
			sparksEffect.SetActive(true);

			Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

			playerRigidbody.AddForce(dirToPushPlayer.normalized * pushPlayerForce);

			player.TakeDamage(damagePlayer);

			//Debug.Log("PLAYER IS TOUCHING ME!!!");
			/*
			damagePlayerTimer += Time.deltaTime;
			if (damagePlayerTimer >= damageRate)
			{
				damagePlayerTimer = 0;
			}
			*/
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			damagePlayerTimer = damageRate;
		}
	}
}
