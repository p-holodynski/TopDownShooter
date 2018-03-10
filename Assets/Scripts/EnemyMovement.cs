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
	public NavMeshAgent nav; // reference to navMeshAgent
	Material skinMaterial;
	Color originalColor;
	LivingEntity targetEntinty;
	public ParticleSystem deathEffect;

	//float attackDistanceTreshhold = .5f;
	//float timeBetweenAttacks = 1;
	//float nextAttackTime;
	//float myCollisionRadius;
	//float playerCollisionRadius;

	bool hasTarget;

	public enum State {Idle, Chasing, Attacking};
	State currentState;

	void Awake(){
		nav = GetComponent<NavMeshAgent>();

		if (GameObject.FindGameObjectWithTag ("Player") != null) {

			hasTarget = true;
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntinty = player.GetComponent<LivingEntity> ();
		
		}
	}

	protected override void Start()
	{
		base.Start ();

		skinMaterial = GetComponent<Renderer> ().material;
		originalColor = skinMaterial.color;

		if (hasTarget) {
			currentState = State.Chasing;

			targetEntinty.OnDeath += OnPlayerDeath;
			//playerHealth = player.GetComponent<PlayerHealth>();
			//enemyHealth = GetComponent<EnemyHealth>();
			//myCollisionRadius = GetComponent<CapsuleCollider>().radius;
			//playerCollisionRadius = GetComponent<SphereCollider> ().radius;

			StartCoroutine (UpdatePath ());
		}
	}

	public void SetCharacteristics(float moveSpeed, int hitsToKillPlayer, float enemyHealth, Color skinColor){
		nav.speed = moveSpeed;

		if (hasTarget) {
			
		}
		startingHealth = enemyHealth;

		skinMaterial = GetComponent<Renderer> ().material;
		skinMaterial.color = skinColor;
		originalColor = skinMaterial.color;
	}

	public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
	{

		if (damage >= health) {
			Destroy(Instantiate (deathEffect.gameObject, hitPoint, Quaternion.FromToRotation (Vector3.forward, hitDirection)) as GameObject, deathEffect.main.startLifetimeMultiplier);
		}
		base.TakeHit (damage, hitPoint, hitDirection);
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
