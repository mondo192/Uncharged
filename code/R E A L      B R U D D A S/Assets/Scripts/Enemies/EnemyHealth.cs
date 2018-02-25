using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
	public int currentHealth;
    public int hpIncrement = 25;
    public int score = 10;
	CapsuleCollider capsuleCollider;
	AudioSource enemyAudio;
	public GameObject hpdrop;
	public float sinkSpeed = 2.5f;
	bool isSinking;
	bool isDead;
    bool DestroyDrop = false;
    GameObject hpdrops;

    Animator anim;
	float time = 0;

	// Awake fucntion will call once at the very start of the game, "start" only calls at the instansation
	void Awake () 
	{
        int stop = 1;
        int scoreTracker = 0;
        
        //Every time
        if (ScoreManager.score % 100 == 0 && stop == 1 && ScoreManager.score != 0)
        {
            //Will add hpIncrement which is continually frowing
            EnemyController.EnemyLimit += 5;
            scoreTracker += 100;
            stop = 0;
        }

        //Every time it increments by another 100, we then add another 100 hp to the scoreTracker
        if (ScoreManager.score % (scoreTracker + 100) == 0 && ScoreManager.score != 0)
        {
            stop = 1;
        }

        capsuleCollider = GetComponentInChildren <CapsuleCollider> ();
		currentHealth = startingHealth;
		enemyAudio = GetComponent <AudioSource> ();
		anim = GetComponentInChildren <Animator> ();
    }

    void DeleteHPDrop()
    {
        var hpdropPos = transform.position; //+ (transform.forward * 2);
        //var clone = Instantiate(hpdrop, hpdropPos, Quaternion.identity);
        Destroy(hpdrops, 10.0f);
        DestroyDrop = false;
    }
        

    // Update is called once per frame
    void Update () 
	{
        if (isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        if(DestroyDrop)
        {
            DeleteHPDrop();
        }
    }

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if(isDead)
		{
			return;
		}

		enemyAudio.Play ();

		currentHealth -= amount;

		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		ScoreManager.score += score;
        EnemyController.currEnemies--;


        capsuleCollider.isTrigger = true;

		anim.SetTrigger("Dead");

		//enemyAudio.clip = deathClip;
		enemyAudio.Play ();

		int rand = 0;
		if((rand = Random.Range(0, 1)) == 0)
		{
            hpdrops = Instantiate(hpdrop, transform.position, transform.rotation);
            DestroyDrop = true;
		}
		//Debug.Log("Random: " + rand);

	}

	public void StartSinking ()
	{
		//Debug.Log("Start Sinking", gameObject);
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
		Destroy (gameObject, 2f);
	}
}
