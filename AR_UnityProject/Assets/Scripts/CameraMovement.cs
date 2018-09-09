using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform playerPos;
	public Vector3 offset;
	public float lerpSpeed = 0.125f;

	void Update ()
	{
		Vector3 camPos = new Vector3(playerPos.transform.position.x + offset.x, playerPos.transform.position.y + offset.y, playerPos.transform.position.z + offset.z);
		transform.position = Vector3.Lerp(transform.position, camPos, lerpSpeed);
	}
}
