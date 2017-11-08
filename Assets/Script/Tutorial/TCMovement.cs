using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCMovement : TutorialChapter {

	// Use this for initialization
	void Start () {
		Debug.Log ("Tutorial Chapter: Movement");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
			GameObject.Destroy (gameObject);
		}
	}
}
