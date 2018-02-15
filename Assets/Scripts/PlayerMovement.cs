using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LivingEntity 
{

	public float speed = 6f;
	public float dashDistance = 10f;
	Vector3 movement;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;
	public float timer = 0;

	// called automatically
	// called whenever the script is enabled or not, good for setting up references
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody = GetComponent<Rigidbody> ();
	}

	// called automatically
	// unity calls it on its scripts that fires every physics update (normal update runs with rendering, this one with physics)
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal"); // has only values of -1,0,1
		float v = Input.GetAxisRaw("Vertical");

		Move (h, v);
		Turning ();

		timer += Time.deltaTime;
		if (Input.GetButtonDown ("Jump") && timer >= 3) 
		{
			Dash ();
			timer = 0;
		}
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody.MoveRotation (newRotation);
		}
	}

	void Dash()
	{
		playerRigidBody.AddForce(transform.forward * dashDistance, ForceMode.Impulse);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall") {
			Debug.Log ("touch");
			foreach (ContactPoint contact in collision.contacts) {
				Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false); // TODO contact point is not in the center of the playerbody
			}
		}
	}


}
