using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 10f;
	public int health = 100;

	public float pushbackSpeed = 100f;
	public float pushbackTimer = 0.2f;

	private Rigidbody rb;
	private Vector3 movement;
	private Vector3 lookAtDir;
	private bool canMove = false;

	private bool beingAttacked = false;
	private float timer;

	private void Start()
	{
		movement = transform.position;
		lookAtDir = transform.position;
		canMove = true;
		rb = GetComponent<Rigidbody>();
		timer = 0;
	}

	void Update()
	{
		if (!beingAttacked)
			Movement();
		else
			BeingAttacked();

		if (health < 0)
			Destroy(gameObject);
	}

	private void FixedUpdate()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, this.transform.forward, out hit, 0.5f))
		{
			if (hit.collider.tag == "Wall")
				canMove = false;
		}
		else
			canMove = true;
	}

	void Movement()
	{
		float horizontal = InputManager.GetInstance().HorizontalAxis();     // Input.GetAxis("Horizontal");
		float vertical = InputManager.GetInstance().VerticalAxis();         // Input.GetAxis("Vertical");

		// Rotation
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

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	void BeingAttacked()
	{

		movement += -transform.forward * pushbackSpeed * Time.deltaTime;
		transform.position = movement;

		timer += Time.deltaTime;
		if (timer > pushbackTimer)
		{
			beingAttacked = false;
			timer = 0;
		}
	}

    private void OnCollisionEnter(Collision obj)
    {
		Enemy enemy = obj.gameObject.GetComponent<Enemy>();
		if (enemy != null)
			beingAttacked = true;
    }
}
