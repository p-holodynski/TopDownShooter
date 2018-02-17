using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyMovement : LivingEntity 
{

	Transform player; // reference to playerObject
	//PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	NavMeshAgent nav; // reference to navMeshAgent
	Material skinMaterial;
	Color originalColor;
	LivingEntity targetEntinty;

	//float attackDistanceTreshhold = .5f;
	//float timeBetweenAttacks = 1;
	//float nextAttackTime;
	//float myCollisionRadius;
	//float playerCollisionRadius;

	bool hasTarget;

	public enum State {Idle, Chasing, Attacking};
	State currentState;

	protected override void Start()
	{
		base.Start ();
		nav = GetComponent<NavMeshAgent>();
		skinMaterial = GetComponent<Renderer> ().material;
		originalColor = skinMaterial.color;

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			currentState = State.Chasing;
			hasTarget = true;
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntinty = player.GetComponent<LivingEntity> ();
			targetEntinty.OnDeath += OnPlayerDeath;
			//playerHealth = player.GetComponent<PlayerHealth>();
			//enemyHealth = GetComponent<EnemyHealth>();
			//myCollisionRadius = GetComponent<CapsuleCollider>().radius;
			//playerCollisionRadius = GetComponent<SphereCollider> ().radius;

			StartCoroutine (UpdatePath ());
		}
	}

	void OnPlayerDeath(){
		hasTarget = false;
		currentState = State.Idle;
	}


	// update not fixedupdate because navmesh
	void Update()
	{
//		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
//		{
			
//		}
//		else
//		{
//			nav.enabled = false;
//		}
//		if (Time.time > nextAttackTime) {
////			float squareDistanceToTarget = (player.position - transform.position).sqrMagnitude;
////			if (squareDistanceToTarget < Mathf.Pow (attackDistanceTreshhold + myCollisionRadius + playerCollisionRadius, 2)) {
////				
////			}
//			nextAttackTime = Time.time + timeBetweenAttacks;
//			StartCoroutine (Attack ());
//		}
			
	}

//	IEnumerator Attack(){
//
//		currentState = State.Attacking;
//		nav.enabled = false;
//		Vector3 originalPosition = transform.position;
//		Vector3 dirToTarget = (player.position - transform.position).normalized;
//		Vector3 attackPosition = player.position - dirToTarget * (myCollisionRadius + playerCollisionRadius + attackDistanceTreshhold/2);
//
//		float attackSpeed = 3;
//		float percent = 0;
//
//		while (percent <= 1) {
//
//			percent += Time.deltaTime * attackSpeed;
//			float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
//			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
//
//			yield return null;
//		}
//
//		currentState = State.Chasing;
//		nav.enabled = true;
//	}

	IEnumerator UpdatePath(){
		float refreshRate = .25f;

		while (hasTarget) {
			if (currentState == State.Chasing) {
				//Vector3 dirToTarget = (player.position - transform.position).normalized;
				//Vector3 playerPosition = player.position - dirToTarget * (myCollisionRadius + playerCollisionRadius + attackDistanceTreshhold/2);
				if (!dead) {
					nav.SetDestination (player.position);
				}
				yield return new WaitForSeconds (refreshRate);
			}
		}
	}
}
