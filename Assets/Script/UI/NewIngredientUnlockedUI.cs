using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewIngredientUnlockedUI : SCUI {

	private Text ingredientNameAndPriceText;
	private Image ingredientSprite;

	// Use this for initialization
	void Awake () {
		ingredientNameAndPriceText = transform.Find("IngredientNameAndPriceText").GetComponent<Text>();
		ingredientSprite = transform.Find("Image").GetComponent<Image>();
		//GameManager.getGameManager ().pauseClock ();
		setContent ("tuna");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnDestroy() {
		//GameManager.getGameManager ().unpauseClock ();
	}

	public void setContent(string ingredientId) {
		if (PlayerDataManager.getPlayerData().ingredients.ContainsKey (ingredientId)) {
			ingredientSprite.sprite = Resources.Load (IngredientManager.ingredients[ingredientId].getSpritePathB(), typeof(Sprite)) as Sprite;
			ingredientNameAndPriceText.text = IngredientManager.ingredients[ingredientId].getId() + "\n" + "Buy Price: " + IngredientManager.ingredients[ingredientId].getPrice() + "G/" + IngredientManager.ingredientBuyingUnit; 
		}
	}
}
