using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyTunaFish :  GenericInteractiveObject {

	void Start () {
		base.functionText = "Buy Tuna Fish";
		base.actAct = buyTunaFish;
	}

	void buyTunaFish () {
		if (Input.GetButtonDown ("Fire1")) {
			if (base.gameManager.ingredientManager.buyIngredient("tuna")) {
				base.gameManager.showBottomText("Tuna Bought");
			} else {
				base.gameManager.showBottomText  ("Not Enough Gold.");
			}
		}
	}

}
