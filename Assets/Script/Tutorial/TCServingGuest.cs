using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCServingGuest : TutorialChapter {

	int counter = 0;
	int counterMax = 500;

	// Use this for initialization
	void Start () {
		GameManager.getGameManager ().guestManager.spawnOneGuest (0);
	}
	
	// Update is called once per frame
	void Update () {
		counter += 1;
		if (counter >= counterMax) {
			Destroy (gameObject);
		}
	}
}
