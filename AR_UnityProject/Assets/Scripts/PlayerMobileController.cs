using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
	public Joystick joystick;
	public float speed;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();	
	}
	
	void Update ()
	{
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        Vector3 movement = new Vector3(x, 0, y);

        rb.velocity = movement * speed;

        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                Mathf.Atan2(x, y) * Mathf.Rad2Deg,
                                                transform.eulerAngles.z);
        }
	}
}
