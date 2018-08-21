using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 movement;
    private Vector3 lookAtDir;
    private Rigidbody rb;
    private bool canMove = false;

    private void Start()
    {
        movement = transform.position;
        lookAtDir = transform.position;
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    void Update ()
    {
        Movement();
	}

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, this.transform.forward, out hit, 1f))
        {
            if (hit.collider.tag == "Wall")
                canMove = false;
        }
        else
            canMove = true;
    }

    void Movement()
    {
        //canMove = true;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        lookAtDir.x = transform.position.x + horizontal;
        lookAtDir.z = transform.position.z + vertical;

        if (canMove)
        {
            movement.x += horizontal * speed * Time.deltaTime;
            movement.z += vertical * speed * Time.deltaTime;
        }

        transform.LookAt(lookAtDir);

        transform.position = movement;
    }

    private void OnCollisionEnter(Collision obj)
    {
        /*
        if (obj.collider.tag == "Wall")
            canMove = false;
        */
    }
}
