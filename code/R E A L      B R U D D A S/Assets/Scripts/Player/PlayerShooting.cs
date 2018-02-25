using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 1000f;
	public Slider healthSlider;
	GameObject player;
	PlayerHP playerHP;


	float timer;
	Ray shootRay = new Ray();
	RaycastHit shootHit;
	int shootableMask;
	//ParticleSystem gunParticles;
	LineRenderer gunLine;
	public AudioClip gunAudio;
	public AudioClip glassSmash;
	AudioSource audio;
	Light gunLight;
	Light overheadLight;

	float effectsDisplayTime = 0.2f;

	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		//gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		audio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHP = player.GetComponent <PlayerHP> ();

		overheadLight = (GameObject.Find("OverheadLight")).GetComponent<Light>();
	}


	void Update ()
	{
		timer += Time.deltaTime;

        //Fire1 - project settings > input > Fire1 left mouse button (mouse 0)
        if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot ();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}


	public void DisableEffects ()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}


	void Shoot ()
	{

		timer = 0f;

		// this code randomly selects a pitch for each shot
		//gunAudio.pitch = Random.Range (0.94f, 1.1f);
		//gunAudio.Play ();
		audio.PlayOneShot (gunAudio, 0.7f);

		//Debug.Log("Light spot angle: " + overheadLight.spotAngle);
		// This makes the light radius smaller when you shoot the gun
		if(overheadLight.spotAngle > 0 && playerHP.currentHealth > 1)
		{
			overheadLight.spotAngle--;
			playerHP.currentHealth--;
			healthSlider.value = playerHP.currentHealth;
		}
		

		gunLight.enabled = true;

        //Start and stop it to separate the bullets and give a bullet feel
        //gunParticles.Stop ();
        //gunParticles.Play ();

        gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

        //If it hits something, its returned back to us and we say the other end of the line is there. If hits nothing, then continue on
        //out shootHit stands for output of what we hit
        //shootablemash ensures we can only shoot things that can be shot
        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
            //Gets enemyHealth script. 
            //ShootHit gets what we hit, collider sees the contact, then gets the enemyhealth component
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			Debug.Log(enemyHealth);

			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				Debug.Log("damage was given");
				audio.PlayOneShot (glassSmash);
			}
            //Sets from barrel to the object
            gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

}
