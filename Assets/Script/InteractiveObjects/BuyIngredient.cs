using UnityEngine;
using System.Collections;

public class BuyIngredient : GenericInteractiveObject {

	public GameObject selectIngredientUI;

	// Use this for initialization
	void Start () {
		base.functionText = "";
		base.actAct = buyIngredient;
		base.deactAct = hideIngredientSelectUI;
	}

	void buyIngredient() {
		
		gameManager.closeAllUI ();
		selectIngredientUI.SetActive (true);
	}

	void hideIngredientSelectUI() {
		selectIngredientUI.SetActive (false);
	}
}
