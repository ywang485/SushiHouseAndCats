﻿using UnityEngine;
using System.Collections;

public class Tomato : BasicWeapon {

	// Use this for initialization
	void Start () {
		Destroy(gameObject,1.0f);
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
