using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	
	Transform player;
	//PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	// using the nav agent thingy
	UnityEngine.AI.NavMeshAgent nav;

	// Awake fucntion will call once at the very start of the game, "start" only calls at the instansation
	void Awake () 
	{
		// find player position
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		// find path to player
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}
	

	void Update () 
	{
		if(enemyHealth.currentHealth > 0 /*&& playerHealth.currentHealth > 0*/)
		{
			// find new path to player
			nav.SetDestination (player.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
}
