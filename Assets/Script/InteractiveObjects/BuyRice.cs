﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyRice :  GenericInteractiveObject{

	void Start () {
		base.functionText = "Buy Rice (10G/10)";
		base.actAct = buyRice;
	}

	void buyRice () {
		if (Input.GetButtonDown ("Fire1")) {
			if (base.gameManager.ingredientManager.buyIngredient("rice")) {
				base.gameManager.showBottomText ("Rice Bought");
			} else {
				base.gameManager.showBottomText ("Not Enough Gold.");
			}
		}
	}
}
