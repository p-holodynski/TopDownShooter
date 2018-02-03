using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour 
{

	// Drag in the Bullet Emitter from the Component Inspector
	public GameObject bulletEmitter;

	// Drag in the Bullet Prefab from the Component Inspector
	public GameObject bullet;

	// Enter the Speed of the Bullet from the Component Inspector
	public float bulletSpeed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("mouse 0")) 
		{
			// Bullet instantiation
			GameObject temporaryBulletHandler;
			temporaryBulletHandler = Instantiate (bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

			// When bullets appear rotated incorrectly because of their pivot, we can rotate them based on our mesh
			temporaryBulletHandler.transform.Rotate(Vector3.left * 90);

			// Retrieve the Rigidbody component from the instantiated Bullet and control it
			Rigidbody temporaryRigidbody;
			temporaryRigidbody = temporaryBulletHandler.GetComponent<Rigidbody> ();

			// Tell the bullet to be "pushed" forward by an amount set by bulletSpeed
			temporaryRigidbody.AddForce(transform.forward * bulletSpeed);

			// Basic clean up, set the bullets to self destruct after 3seconds
			Destroy(temporaryBulletHandler, 3.0f);
		}
	}
}
