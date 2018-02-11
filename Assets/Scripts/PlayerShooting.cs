using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(GunController))]
public class PlayerShooting : MonoBehaviour 
{
	GunController gunController;

	void Start(){
		gunController = GetComponent<GunController> ();
	}

	void Update(){
		if (Input.GetMouseButton (0)) {
			gunController.Shoot ();
		}
	}
//	public float range = 100f;
//	public float damagePerShot = 20;
//	public float timeBetweenBullets = 0.15f;
//
//
//	float timer;
//	Ray shootRay;
//	RaycastHit shootHit;
//	int shootableMask;
//	LineRenderer gunLine;
//	float effectsDisplayTime = 0.2f;
//
//	void Awake()
//	{
//		shootableMask = LayerMask.GetMask ("Shootable");
//		gunLine = GetComponent<LineRenderer> ();
//	}
//
//	void Update()
//	{
//		timer += Time.deltaTime;
//
//		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets) {
//			Shoot ();
//		}
//
//		if (timer >= effectsDisplayTime) {
//			DisableEffects ();
//		}
//	}
//
//	public void DisableEffects(){
//		gunLine.enabled = false;
//	}
//
//	void Shoot(){
//		timer = 0f;
//
//		gunLine.enabled = true;
//		gunLine.SetPosition (0, transform.position);
//
//		shootRay.origin = transform.position;
//		shootRay.direction = transform.forward;
//
//		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
//			gunLine.SetPosition (1, shootHit.point);
//		} 
//		else {
//			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
//		}
//	}

//	// Drag in the Bullet Emitter from the Component Inspector
//	public GameObject bulletEmitter;
//
//	// Drag in the Bullet Prefab from the Component Inspector
//	public GameObject bullet;
//
//	// Enter the Speed of the Bullet from the Component Inspector
//	public float bulletSpeed;
//
//	// Use this for initialization
//	void Start () 
//	{
//		
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if (Input.GetKeyDown ("mouse 0")) 
//		{
//			// Bullet instantiation
//			GameObject temporaryBulletHandler;
//			temporaryBulletHandler = Instantiate (bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
//
//			// When bullets appear rotated incorrectly because of their pivot, we can rotate them based on our mesh
//			temporaryBulletHandler.transform.Rotate(Vector3.left * 90);
//
//			// Retrieve the Rigidbody component from the instantiated Bullet and control it
//			Rigidbody temporaryRigidbody;
//			temporaryRigidbody = temporaryBulletHandler.GetComponent<Rigidbody> ();
//
//			// Tell the bullet to be "pushed" forward by an amount set by bulletSpeed
//			temporaryRigidbody.AddForce(transform.forward * bulletSpeed);
//
//			// Basic clean up, set the bullets to self destruct after 3seconds
//			Destroy(temporaryBulletHandler, 3.0f);
//		}
//	}
}
