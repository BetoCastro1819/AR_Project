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

    private Camera cam;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		canMove = true;
		timer = 0;
        cam = Camera.main;
	}

	void Update()
	{
		if (health <= 0)
			Destroy(gameObject);

        Movement();
        Aiming();
    }

	private void FixedUpdate()
	{
        // Player movement
        /*
        if (!beingAttacked)
            Movement();
        else
            BeingAttacked();
        */
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

        Vector3 movement = Vector3.forward * y + Vector3.right * x;

        rb.velocity = movement * speed;

        /*
        if (x != 0 || y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                Mathf.Atan2(x, y) * Mathf.Rad2Deg,
                                                transform.eulerAngles.z);
        }
        */
	}

    void Aiming()
    {
        // Get cursor´s position in the world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // will be used to store cursor´s position in the world space
        Vector3 worldMousePosition = new Vector3();

        // Store cursor´s position in the world
        // if it is proyecting a ray over an object in the world
        if (Physics.Raycast(ray, out hit))
        {
            worldMousePosition = hit.point;
            //Debug.Log(hit.collider.name);
        }

        Vector3 aimPos = worldMousePosition;
        aimPos.y = transform.position.y;

        transform.LookAt(aimPos);
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
