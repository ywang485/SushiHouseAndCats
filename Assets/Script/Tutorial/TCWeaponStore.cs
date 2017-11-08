using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCWeaponStore : TutorialChapter {

	// Use this for initialization
	void Start () {
		base.goToNextTutorialWhenDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("WeaponStoreUI") != null) {
			GameObject.Destroy (gameObject);
		}
	}
}
