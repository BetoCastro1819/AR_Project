using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
	public GameObject pooledObject;
	public Transform poolParent;
	public int poolSize = 20;

	private List<GameObject> pool;

	#region Singleton
	private static ObjectsPool instance;
	public  static ObjectsPool Get()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
	}
	#endregion

	private void Start()
	{
		pool = new List<GameObject>();
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			obj.transform.parent = poolParent;
			pool.Add(obj);
		}
	}

	public GameObject GetObjectPooled()
	{
		for (int i = 0; i < pool.Count; i++)
		{
			if (!pool[i].activeInHierarchy)
				return pool[i];
		}
		return null;
	}
}
