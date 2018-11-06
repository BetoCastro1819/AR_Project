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

    private Vector3 smoothLookAtPoint;

    private void Start()
    {
        smoothLookAtPoint = player.position;
    }

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

        /*
        if (Input.GetKey(KeyCode.Q))
        {
            camPos += transform.right;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(transform.right * -rotateAroundSpeed * Time.deltaTime);
        }


        // Creates a position for the camera based on player´s facing
        Vector3 camPos =    player.transform.right * offset.x +         // X axis
                            player.transform.forward * offset.z +       // Y axis
                            player.transform.up * offset.y;             // Z axis
        */

        transform.position = camPos;

        //smoothLookAtPoint = Vector3.Lerp(smoothLookAtPoint, player.position, rotationSpeed);
        transform.LookAt(player.transform.position);
    }
}
