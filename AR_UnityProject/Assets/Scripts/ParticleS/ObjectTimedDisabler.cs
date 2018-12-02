using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimedDisabler: MonoBehaviour
{
	public float timeToDisableObject = 2;

	private float timer;

	void Start ()
	{
		timer = 0;
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= timeToDisableObject)
		{
			gameObject.SetActive(false);
			timer = 0;
		}
	}
}
