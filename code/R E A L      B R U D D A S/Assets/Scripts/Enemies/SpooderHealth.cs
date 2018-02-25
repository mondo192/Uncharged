using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpooderHealth : MonoBehaviour {

	public static int startingHealth = 100;
	public int currentHealth;
	public int score = 100;
	bool isDead;

	// Use this for initialization
	void Awake () 
	{
		currentHealth = startingHealth;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if(isDead)
		{
			return;
		}

		currentHealth -= amount;

		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		ScoreManager.score += score;

		StartSinking ();
		//EnemyController.currEnemies--;


		//capsuleCollider.isTrigger = true;

		//anim.SetTrigger("Dead");

		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		/*
		int rand = 0;
		if((rand = Random.Range(0, 1)) == 0)
		{
			hpdrops = Instantiate(hpdrop, transform.position, transform.rotation);
			DestroyDrop = true;
		}
		//Debug.Log("Random: " + rand);
		*/
	}

	public void StartSinking ()
	{
		//Debug.Log("Start Sinking", gameObject);
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		//isSinking = true;
		Destroy (gameObject, 2f);
	}
}
