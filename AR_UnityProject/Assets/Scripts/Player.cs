using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 10f;
	public int health = 100;

	public float pushbackForce = 100f;
	public float pushbackTimer = 0.2f;

    [HideInInspector]
    public bool canPlaceItem = true;

	private Rigidbody rb;
	private bool canMove = false;

	private bool beingAttacked = false;
	private float timer;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		canMove = true;
		timer = 0;
	}

	void Update()
	{
		if (health <= 0)
			Destroy(gameObject);
	}

	private void FixedUpdate()
	{
        // Player movement
        if (!beingAttacked)
            Movement();
        else
            BeingAttacked();


        // Check if can place object in front
        RaycastHit hit;
        if (Physics.Raycast(transform.position, this.transform.forward, out hit, 1.0f))
        {
            canPlaceItem = false;
        }
        else
        {
           canPlaceItem = true;
        }
    }

	void Movement()
	{
		float x = InputManager.GetInstance().HorizontalAxis();       
		float y = InputManager.GetInstance().VerticalAxis();         

        Vector3 movement = new Vector3(x, 0, y);

        rb.velocity = movement * speed;

        if (x != 0 || y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                Mathf.Atan2(x, y) * Mathf.Rad2Deg,
                                                transform.eulerAngles.z);
        }
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	void BeingAttacked()
	{
        rb.AddForce(-transform.forward * pushbackForce);

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
