using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed = 12f;

	private Vector3 movement;
	Animator anim;
	private Rigidbody rb;
	int floorMask;
	float camRayLength = 100f;
	public AudioClip footsteps;
	AudioSource audio;
    Light overheadLight;
    Light LightbulbLight;

    //Gets called regardless if screipt is enabled
    void Awake()
	{
		rb = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask("Ground");
		anim = GetComponent <Animator>();
		audio = GetComponent <AudioSource>();
        overheadLight = (GameObject.Find("OverheadLight")).GetComponent<Light>();
        LightbulbLight = (GameObject.Find("LightbulbLight")).GetComponent<Light>();
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        overheadLight.enabled = false;
        LightbulbLight.enabled = false;
        yield return new WaitForSeconds(1);
        overheadLight.enabled = true;
        LightbulbLight.enabled = true;
    }
    
    //Runs with physics 
    void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning();
		Animating(h, v);
	}

	void Move(float h, float v)
	{
        //x, y, z. Where z is forwards and backwards and x is left and right
        movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);
	}

	// this function is called from the walking animation, at the point where he steps
	public void PlayFootstep()
	{
		audio.PlayOneShot (footsteps, 0.05f);
	}

    //Create a ray that goes from camera to scene
    void Turning ()
	{
        //Take the mouse position on the screen and cast that ray to the point underneath that mouse
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Information gotten back from the ray (The point underneath the mouse its pointing to)
        RaycastHit floorHit;

        //Raycast fxn return true if hit something - parm 1 = ray itself, parm 2 = returns info parm 3 = length of ray cast, 4 = only trying to hit things on floor layer.
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
            //Point its hit floor - position of player
            Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

            //makes player to mouse a forward vector
            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rb.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = (h != 0f || v != 0f);
		anim.SetBool ("IsWalking", walking);
	}
}
