using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public Transform pauseMenu;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			Pause ();
		}
	}

	public void Pause()
	{
		if (pauseMenu.gameObject.activeInHierarchy == false) {
			pauseMenu.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			pauseMenu.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
}
