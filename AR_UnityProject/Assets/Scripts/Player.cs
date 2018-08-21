using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 movement;
    private Vector3 lookAtDir;

    private void Start()
    {
        movement = transform.position;
        lookAtDir = transform.position;
    }

    void Update ()
    {
        Movement();
	}

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        lookAtDir.x = transform.position.x + horizontal;
        lookAtDir.z = transform.position.z + vertical;

        movement.x += horizontal * speed * Time.deltaTime;
        movement.z += vertical * speed * Time.deltaTime;

        transform.LookAt(lookAtDir);
        transform.position = movement;
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.collider.tag == )
    }
}
