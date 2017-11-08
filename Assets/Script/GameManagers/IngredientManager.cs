using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour {

	static public readonly int ingredientBuyingUnit = 10;
	static public readonly Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>() {
		{"rice", new Ingredient("rice", 10, "Rice", "Sprites/Ingredients/rice_s", "Sprites/Ingredients/rice_b")},
		{"tuna", new Ingredient("tuna", 50, "Tuna", "Sprites/Ingredients/tuna_s","Sprites/Ingredients/tuna_b")},
		{"egg", new Ingredient("egg", 30, "Egg", "Sprites/Ingredients/egg_s", "Sprites/Ingredients/egg_b")},
		{"salmon", new Ingredient("salmon", 70, "Salmon", "Sprites/Ingredients/salmon_s", "Sprites/Ingredients/salmon_b")},
		{"white-tuna", new Ingredient("white-tuna", 50, "White Tuna", "Sprites/Ingredients/whiteTuna_s", "Sprites/Ingredients/whiteTuna_b")},
		{"cucumber", new Ingredient("cucumber", 5, "Cucumber", "Sprites/Ingredients/cucumber_s", "Sprites/Ingredients/cucumber_b")},
		{"avocado", new Ingredient("avocado", 30, "Avocado", "Sprites/Ingredients/avocado_s", "Sprites/Ingredients/avocado_b")},
		{"duck-meat", new Ingredient("duck-meat", 40, "Duck Meat", "Sprites/Ingredients/avocado_s", "Sprites/Ingredients/duckMeat_b")}
		};

	private GameManager gameManager;

	// Number of Ingredient Indicator
	public Text ingredientAmountIndicator;

	// Ingredient Existence Indicator
	public GameObject tunaIndicator;
	public GameObject salmonIndicator;
	public GameObject whiteFishIndicator;
	public GameObject eggIndicator;
	public GameObject cucumberIndicator;
	public GameObject avocadoIndicator;
	public GameObject riceIndicator;

	public void consumeIngredient(string ingredient, int howmuch) {
		PlayerDataManager.getPlayerData().ingredients [ingredient] -= howmuch;
		updateIndicators ();
	}

	public bool buyIngredient(string ingredient) {
		Ingredient target = IngredientManager.ingredients [ingredient];
		if (PlayerDataManager.getPlayerData().numGold >= target.getPrice()) {
			gameManager.decreaseNumGold(target.getPrice());
			PlayerDataManager.getPlayerData().ingredients[ingredient] += ingredientBuyingUnit;
			updateIndicators ();
			gameManager.playSFX (GameManager.posBlipSFX);
			return true;
		}
		else {
			gameManager.playSFX (GameManager.negBlipSFX);
			return false;
		}
	}
		
	void updateNumIngredient() {
		string content = "";
		foreach (string iid in PlayerDataManager.getPlayerData().ingredients.Keys){
			content  +=  (iid + ": " + PlayerDataManager.getPlayerData().ingredients[iid] + "\n");
			
		}
		// For testing
		content += ("Sushi Sold: " +  PlayerDataManager.getPlayerData().sushiSold + "\n");
		// end testing

		ingredientAmountIndicator.text = content;
	}

	static void turnOnIndicator(GameObject indicator) {

		SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer> ();
		Color changeTo = indicatorSprite.color;
		changeTo.a = 1f;
		indicatorSprite.color = changeTo;

	}

	static void turnOffIndicator(GameObject indicator) {
		SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer> ();
		Color changeTo = indicatorSprite.color;
		changeTo.a = 0f;
		indicatorSprite.color = changeTo;
	}	

	void updateIndicators() {
		
		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("rice") || PlayerDataManager.getPlayerData().ingredients["rice"] <= 0) {
			IngredientManager.turnOffIndicator (riceIndicator);
		} else {
			IngredientManager.turnOnIndicator (riceIndicator);
		}
			
		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("tuna") ||PlayerDataManager.getPlayerData().ingredients["tuna"] <= 0) {
			IngredientManager.turnOffIndicator (tunaIndicator);
		} else {
			IngredientManager.turnOnIndicator (tunaIndicator);
		}

		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("egg") || PlayerDataManager.getPlayerData().ingredients["egg"] <= 0) {
			IngredientManager.turnOffIndicator (eggIndicator);
		} else {
			IngredientManager.turnOnIndicator (eggIndicator);
		}

		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("salmon") || PlayerDataManager.getPlayerData().ingredients["salmon"] <= 0) {
			IngredientManager.turnOffIndicator (salmonIndicator);
		} else {
			IngredientManager.turnOnIndicator (salmonIndicator);
		}

		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("white-tuna") || PlayerDataManager.getPlayerData().ingredients["white-tuna"] <= 0) {
			IngredientManager.turnOffIndicator (whiteFishIndicator);
		} else {
			IngredientManager.turnOnIndicator (whiteFishIndicator);
		}

		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("cucumber") || PlayerDataManager.getPlayerData().ingredients["cucumber"] <= 0) {
			IngredientManager.turnOffIndicator (cucumberIndicator);
		} else {
			IngredientManager.turnOnIndicator (cucumberIndicator);
		}

		if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey("avocado") || PlayerDataManager.getPlayerData().ingredients["avocado"] <= 0) {
			IngredientManager.turnOffIndicator (avocadoIndicator);
		} else {
			IngredientManager.turnOnIndicator (avocadoIndicator);
		}


	}

	// Use this for initialization
	void Start () {

		gameManager = gameObject.GetComponentInParent<GameManager> ();
		updateIndicators ();
	
	}
	
	// Update is called once per frame
	void Update () {
		updateNumIngredient ();
	}
}
