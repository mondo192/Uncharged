using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public int hpAdd;
	public Slider healthSlider;
	public Transform DeathMenu;

	PlayerController playerController;
	PlayerShooting playerShooting;
	Light overheadLight;

	bool damaged;
	bool isDead;

	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    Animator anim;
    AudioSource playerDeath;
    public AudioClip playerHit;
    AudioSource audio;


    // Use this for initialization
    void Awake () 
	{
		playerController = GetComponent <PlayerController> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
		overheadLight = (GameObject.Find("OverheadLight")).GetComponent<Light>();
        anim = GetComponentInChildren<Animator>();
        playerDeath = GetComponent<AudioSource>();
        //playerHit = GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	// code to run when a player collides with a drop
	void OnTriggerEnter (Collider other)
	{
		// debug message
		//Debug.Log("On Trigger Enter");

		// checks if the thing is a hpdrop
		if(other.gameObject.tag == "HPdrop")
		{
			// if you have full health and light then don't pick it up
			if( (currentHealth < startingHealth) || (overheadLight.spotAngle < 85))
			{
				// if the hp is not full, but not less 80, just make it full, otherwise add 20
				if(currentHealth > (startingHealth - (startingHealth / 5)))
				{
					currentHealth = startingHealth;
				}
				else
				{
					currentHealth += hpAdd;
				}

				// if the light is not full, but not less 65 (dergess), just make it full (back to 85), otherwise add 20 (degress)
				if(overheadLight.spotAngle > 65)
				{
					overheadLight.spotAngle = 85;
				}
				else
				{
					overheadLight.spotAngle += hpAdd / 10;
				}
				// then destroy the pick up after gaining health and light
				Destroy(other.gameObject);
				healthSlider.value = currentHealth;
			}
		}
	}

	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

        audio.PlayOneShot(playerHit, 1f);

        if (currentHealth <= 0 && !isDead || overheadLight.spotAngle <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		playerShooting.DisableEffects ();

		anim.SetTrigger ("Death");
        playerDeath.Play();
        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        playerController.enabled = false;
		playerShooting.enabled = false;
		RestartLevel();
	}

	public void RestartLevel ()
	{
		//SceneManager.LoadScene (0); --Old way of dieing
		DeathMenu.gameObject.SetActive (true);
	}

}
