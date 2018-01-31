using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	Transform player; // reference to playerObject
	//PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	NavMeshAgent nav; // reference to navMeshAgent


	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent<PlayerHealth>();
		//enemyHealth = GetComponent<EnemyHealth>();
		nav = GetComponent<NavMeshAgent>();
	}


	// update not fixedupdate because navmesh
	void Update()
	{
//		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
//		{
			nav.SetDestination (player.position);
//		}
//		else
//		{
//			nav.enabled = false;
//		}
			
	}
}
