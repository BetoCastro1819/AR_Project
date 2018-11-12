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
	public ObjectsPool sparksEffectPool;

	public enum ObjectType
	{
		PLAYER_BULLET,
		SPARKS_EFFECT
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
				pooledObject =  sparksEffectPool.GetObjectPooled();
				break;
		}
		return pooledObject;
	}
}
