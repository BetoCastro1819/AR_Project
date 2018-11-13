using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;
    public float movementSpeed = 0.125f;
    public float rotationSpeed = 0.125f;
    public float rotateAroundSpeed = 1.0f;

    void Update ()
	{
        if (player != null)
        {
            FollowPlayer();
        }
	}

    void FollowPlayer()
    {
        Vector3 camPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);

        transform.position = camPos;

        transform.LookAt(player.transform.position);
    }
}
