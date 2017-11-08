using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCCounterSpace : TutorialChapter {

	int counter = 0;
	int counterMax = 500;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag ("Sushi").Length >= 3) {
			Destroy (gameObject);
		}
	}
}
