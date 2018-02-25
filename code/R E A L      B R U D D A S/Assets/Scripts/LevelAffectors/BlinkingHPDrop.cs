using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingHPDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("Blink", 0, 5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Blink()
    {
        gameObject.active = true;
    }
}
