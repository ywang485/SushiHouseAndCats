﻿using UnityEngine;
using System.Collections;

public class FishTreats : BasicWeapon {

	// Use this for initialization
	void Start () {
		Destroy(gameObject,2.0f);
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
