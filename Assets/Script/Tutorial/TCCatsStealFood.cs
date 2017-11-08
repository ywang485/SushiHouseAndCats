using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCCatsStealFood : TutorialChapter {

	int counter = 0;
	int counterMax = 100;

	// Use this for initialization
	void Start () {
		GameManager.getGameManager ().catManager.spawnOneCat ();
	}
	
	// Update is called once per frame
	void Update () {

		if (counter >= counterMax) {
			if (GameObject.FindGameObjectWithTag ("Cat") == null) {
				Destroy (gameObject);
			}
		} else {
			counter += 1;
		}
		
	}
}
