using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
	#region Singleton
	private static ObjectPoolManager instance;
	public static ObjectPoolManager GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public ObjectsPool playerBulletsPool;
	public ObjectsPool shootingEnemyBulletsPool;
	public ObjectsPool sparksEffectPool;
	public ObjectsPool explosionsPool;
	public ObjectsPool plasmaExplosionsPool;
	public ObjectsPool rechargeHealthPool;
	public ObjectsPool rechargeEnergyPool;

	public enum ObjectType
	{
		PLAYER_BULLET,
		SHOOTING_ENEMY_BULLET,
		SPARKS_EFFECT,
		EXPLOSION,
		PLASMA_EXPLOSION,
		ENERGY_PARTICLES,
		HEALTH_PARTICLES,
	}

	public GameObject GetObjectFromPool(ObjectType objectType)
	{
		GameObject pooledObject = null;

		switch (objectType)
		{
			case ObjectType.PLAYER_BULLET:
				pooledObject = playerBulletsPool.GetObjectPooled();
				break;
			case ObjectType.SPARKS_EFFECT:
				pooledObject = sparksEffectPool.GetObjectPooled();
				break;
			case ObjectType.SHOOTING_ENEMY_BULLET:
				pooledObject = shootingEnemyBulletsPool.GetObjectPooled();
				break;
			case ObjectType.EXPLOSION:
				pooledObject = explosionsPool.GetObjectPooled();
				break;
			case ObjectType.PLASMA_EXPLOSION:
				pooledObject = plasmaExplosionsPool.GetObjectPooled();
				break;
			case ObjectType.HEALTH_PARTICLES:
				pooledObject = rechargeHealthPool.GetObjectPooled();
				break;
			case ObjectType.ENERGY_PARTICLES:
				pooledObject = rechargeEnergyPool.GetObjectPooled();
				break;
		}
		return pooledObject;
	}
}
