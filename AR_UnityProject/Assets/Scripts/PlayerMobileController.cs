using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
	public Joystick joystick;
	public float speed;
	public float joystickDeadzone = 0.2f;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();	
	}
	
	void Update ()
	{
		if (joystick.Horizontal > joystickDeadzone ||
			joystick.Horizontal < -joystickDeadzone ||
			joystick.Vertical > joystickDeadzone ||
			joystick.Vertical < -joystickDeadzone)
		{
			rb.velocity = new Vector3(joystick.Horizontal * speed,
										rb.velocity.y,
										joystick.Vertical * speed);
		}
		else
			rb.velocity = Vector3.zero;
	}
}
