using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;
    public float movementSpeed = 0.125f;
    public float rotationSpeed = 0.125f;

    private Vector3 smoothLookAtPoint;

    private void Start()
    {
        smoothLookAtPoint = player.position;
    }

    void Update ()
	{
		if (player != null)
		{
            Vector3 camPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);

            /*
            // Creates a position for the camera based on player´s facing
            Vector3 camPos =    player.transform.right * offset.x +         // X axis
                                player.transform.forward * offset.z +       // Y axis
                                player.transform.up * offset.y;             // Z axis
            */


            transform.position = Vector3.Lerp(transform.position, camPos, movementSpeed);

            smoothLookAtPoint = Vector3.Lerp(smoothLookAtPoint, player.position, rotationSpeed);
            transform.LookAt(smoothLookAtPoint);
		}
	}
}
