using UnityEngine;
using System.Collections;

public class SelectIngredient : GenericButton {

	public string ingredient;
	private GameManager gameManager;

	// Use this for initialization
	void Awake () {
		base.hoverBehaviour = 2;
		base.clickAct = buyIngredient;

		gameManager = GameManager.getGameManager();
	}
	
	void buyIngredient() {

		bool success = false;

		success = gameManager.ingredientManager.buyIngredient (ingredient);

		if (!success) {
			gameManager.showBottomText ("Not Enough Gold");
		}

	}
}
