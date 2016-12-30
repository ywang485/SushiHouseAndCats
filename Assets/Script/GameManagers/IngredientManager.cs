using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour {

	public enum Ingredient{
		Rice,
		Tuna,
		Egg,
		Salmon,
		WhiteTuna,
		Cucumber,
		Avocado
	};

	private GameManager gameManager;

	// Number of Ingredients
	public int numRice;
	public int numTuna;
	public int numEgg;
	public int numSalmon;
	public int numWhiteFish;
	public int numCucumber;
	public int numAvocado;

	// Number of Ingredient Indicator
	public Text numRiceIndicator;
	public Text numTunaIndicator;
	public Text numEggIndicator;
	public Text numSalmonIndicator;
	public Text numWhiteFishIndicator;
	public Text numCucumberIndicator;
	public Text numAvocadoIndicator;

	// Ingredient Existence Indicator
	public GameObject tunaIndicator;
	public GameObject salmonIndicator;
	public GameObject whiteFishIndicator;
	public GameObject eggIndicator;
	public GameObject cucumberIndicator;
	public GameObject avocadoIndicator;
	public GameObject riceIndicator;

	// Ingredient Buying Unit
	static public int riceBuyingUnit = 10;
	static public int tunaBuyingUnit = 10;
	static public int eggBuyingUnit = 10;
	static public int salmonBuyingUnit = 10;
	static public int whiteFishBuyingUnit = 10;
	static public int cucumberBuyingUnit = 10;
	static public int avocadoBuyingUnit = 10;

	// Ingredient Price
	static public int ricePrice = 10;
	static public int tunaPrice = 50;
	static public int salmonPrice = 70;
	static public int eggPrice = 30;
	static public int whiteFishPrice = 50;
	static public int cucumberPrice = 5;
	static public int avocadoPrice = 30;

	public bool consumeRice(int howmuch) {
		if (numRice >= howmuch) {
			numRice = numRice - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyRice() {
		if (gameManager.numGold >= ricePrice) {
			gameManager.decreaseNumGold(ricePrice);
			this.numRice += riceBuyingUnit;
			updateIndicators ();
			gameManager.playBlipSFX (true);
			return true;
		}
		else {
			gameManager.playBlipSFX (false);
			return false;
		}
	}

	public bool consumeTuna(int howmuch) {
		if (numTuna >= howmuch) {
			numTuna = numTuna - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyTuna() {
		if (gameManager.numGold >= tunaPrice) {
			gameManager.decreaseNumGold(tunaPrice);
			this.numTuna += tunaBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	public bool consumeEgg(int howmuch) {
		if (numEgg >= howmuch) {
			numEgg = numEgg - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyEgg() {
		if (gameManager.numGold >= eggPrice) {
			gameManager.decreaseNumGold(eggPrice);
			this.numEgg += eggBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	public bool consumeSalmon(int howmuch) {
		if (numSalmon >= howmuch) {
			numSalmon = numSalmon - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buySalmon() {
		if (gameManager.numGold >= salmonPrice) {
			gameManager.decreaseNumGold(salmonPrice);
			this.numSalmon += salmonBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	public bool consumeWhiteFish(int howmuch) {
		if (numWhiteFish >= howmuch) {
			numWhiteFish = numWhiteFish - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyWhiteFish() {
		if (gameManager.numGold >= whiteFishPrice) {
			gameManager.decreaseNumGold(whiteFishPrice);
			this.numWhiteFish += whiteFishBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	public bool consumeCucumber(int howmuch) {
		if (numCucumber >= howmuch) {
			numCucumber = numCucumber - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyCucumber() {
		if (gameManager.numGold >= cucumberPrice) {
			gameManager.decreaseNumGold(cucumberPrice);
			this.numCucumber += cucumberBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	public bool consumeAvocado(int howmuch) {
		if (numAvocado >= howmuch) {
			numAvocado = numAvocado - howmuch;
			updateIndicators ();
			return true;
		}
		return false;
	}

	public bool buyAvocado() {
		if (gameManager.numGold >= avocadoPrice) {
			gameManager.decreaseNumGold(avocadoPrice);
			this.numAvocado += avocadoBuyingUnit;
			updateIndicators ();
			return true;
		}
		else {
			return false;
		}
	}

	void updateNumIngredient() {
		numRiceIndicator.text = numRice.ToString();
		numTunaIndicator.text = numTuna.ToString();
		numEggIndicator.text = numEgg.ToString();
		numSalmonIndicator.text = numSalmon.ToString();
		numWhiteFishIndicator.text = numWhiteFish.ToString();
		numCucumberIndicator.text = numCucumber.ToString();
		numAvocadoIndicator.text = numAvocado.ToString();
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

		if (numRice <= 0) {
			IngredientManager.turnOffIndicator (riceIndicator);
		} else {
			IngredientManager.turnOnIndicator (riceIndicator);
		}

		if (numTuna <= 0) {
			IngredientManager.turnOffIndicator (tunaIndicator);
		} else {
			IngredientManager.turnOnIndicator (tunaIndicator);
		}

		if (numEgg <= 0) {
			IngredientManager.turnOffIndicator (eggIndicator);
		} else {
			IngredientManager.turnOnIndicator (eggIndicator);
		}

		if (numSalmon <= 0) {
			IngredientManager.turnOffIndicator (salmonIndicator);
		} else {
			IngredientManager.turnOnIndicator (salmonIndicator);
		}

		if (numWhiteFish <= 0) {
			IngredientManager.turnOffIndicator (whiteFishIndicator);
		} else {
			IngredientManager.turnOnIndicator (whiteFishIndicator);
		}

		if (numCucumber <= 0) {
			IngredientManager.turnOffIndicator (cucumberIndicator);
		} else {
			IngredientManager.turnOnIndicator (cucumberIndicator);
		}

		if (numAvocado <= 0) {
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
