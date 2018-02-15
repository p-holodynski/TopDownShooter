using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : LivingEntity 
{

	Transform player; // reference to playerObject
	//PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	NavMeshAgent nav; // reference to navMeshAgent


	protected override void Start()
	{
		base.Start ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent<PlayerHealth>();
		//enemyHealth = GetComponent<EnemyHealth>();
		nav = GetComponent<NavMeshAgent>();

		StartCoroutine (UpdatePath ());
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
			
	}

	IEnumerator UpdatePath(){
		float refreshRate = .25f;

		while (player != null) {
			//Vector3 playerPosition = new Vector3 (player.position.x, 0, player.position.z);
			if (!dead) {
				nav.SetDestination (player.position);
			}
			yield return new WaitForSeconds (refreshRate);
		}
	}
}
