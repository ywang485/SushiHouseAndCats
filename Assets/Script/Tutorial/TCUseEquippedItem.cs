﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCUseEquippedItem : TutorialChapter {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(1)) {
			GameObject.Destroy (gameObject);
		}
	}
}