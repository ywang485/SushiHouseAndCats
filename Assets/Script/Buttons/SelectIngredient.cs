using UnityEngine;
using System.Collections;

public class SelectIngredient : GenericButton {

	public IngredientManager.Ingredient ingredient;
	private GameManager gameManager;

	// Use this for initialization
	void Awake () {
		base.hoverBehaviour = 2;
		base.clickAct = buyIngredient;

		gameManager = GameManager.getGameManager();
	}
	
	void buyIngredient() {

		bool success = false;

		if (ingredient == IngredientManager.Ingredient.Tuna) {
			success = gameManager.ingredientManager.buyTuna ();
		} else if (ingredient == IngredientManager.Ingredient.Salmon) {
			success = gameManager.ingredientManager.buySalmon ();
		} else if (ingredient == IngredientManager.Ingredient.WhiteTuna) {
			success = gameManager.ingredientManager.buyWhiteFish ();
		} else if (ingredient == IngredientManager.Ingredient.Egg) {
			success = gameManager.ingredientManager.buyEgg ();
		} else if (ingredient == IngredientManager.Ingredient.Cucumber) {
			success = gameManager.ingredientManager.buyCucumber ();
		} else if (ingredient == IngredientManager.Ingredient.Avocado) {
			success = gameManager.ingredientManager.buyAvocado ();
		}

		if (!success) {
			gameManager.showBottomText ("Not Enough Gold");
		}

	}
}
