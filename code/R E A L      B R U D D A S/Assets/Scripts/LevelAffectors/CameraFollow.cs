using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	public Transform Player = null; //for player transform.
	public float smoothing = 5f; //for amount of smoothing. TEST THIS FOR AGREEABLE VALUE.

	Vector3 offset;

	// Use this for initialization
	void Start () {
		//Cameras position at start of scene is position taken for offset.
		offset = transform.position - Player.position;
		// Vector3s in terms of subtraction returns the distance between the two points. 
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 targetPos = Player.position + offset;

		//Lerp function parameters: Lerp(from position a, to position b, with this much smoothing)
		transform.position = Vector3.Lerp (transform.position, targetPos, smoothing * Time.deltaTime);
	}
}
