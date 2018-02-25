using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStart : MonoBehaviour {

	public void SwapScene(string sceneName)
	{
		Application.LoadLevel (sceneName);
	}
}
