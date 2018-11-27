using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	public BulletType bulletType;
	public enum BulletType 
	{
		PLAYER_BULLET,
		ENEMY_BULLET
	};

	public float bulletSpeed = 10f;
	public float knockbackStrength = 10f;
	public int playerBulletDamage = 50;
	public int enemyBulletDamage = 10;

	private Rigidbody rb;
    private Vector3 originPos;

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
		if (bulletType == BulletType.PLAYER_BULLET)
		{
			Enemy enemy = obj.gameObject.GetComponent<Enemy>();

			if (enemy != null)
			{
				enemy.TakeDamage(playerBulletDamage);
			}
		}

		if (bulletType == BulletType.ENEMY_BULLET)
		{
			Spaceship player = obj.gameObject.GetComponent<Spaceship>();

			if (player != null)
			{
				player.TakeDamage(enemyBulletDamage);
			}
		}

		GameObject sparks = ObjectPoolManager.GetInstance().GetObjectFromPool(ObjectPoolManager.ObjectType.SPARKS_EFFECT);
		sparks.transform.position = obj.contacts[0].point;

        Vector3 sparksDir = originPos - obj.contacts[0].point;
		sparks.transform.forward = sparksDir.normalized;

		sparks.SetActive(true);

		DisableBullet();
	}

    public void SetOriginPos(Vector3 _originPos)
    {
        originPos = _originPos;
    }
}
