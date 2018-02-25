using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	GameObject player;
	PlayerHP playerHP;
	EnemyHealth enemyHealth;

	bool playerInRange;
	float timer;

	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHP = player.GetComponent <PlayerHP> ();
		enemyHealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
		{
			//Debug.Log("ATTACK");
			Attack ();
		}	
	}

	void OnTriggerEnter (Collider other)
	{
		//Debug.Log("On Trigger Enter");
		if(other.gameObject == player)
		{
			playerInRange = true;
			//Debug.Log("in range");
		}
	}

	void OnTriggerExit (Collider other)
	{
		//Debug.Log("On Trigger Exit");
		if(other.gameObject == player)
		{
			playerInRange = false;
			//Debug.Log("not in range");
		}
	}

	void Attack ()
	{
		//Debug.Log("Attacking");
		timer = 0f;

		if(playerHP.currentHealth > 0)
		{
			playerHP.TakeDamage (attackDamage);
		}
	}
}
