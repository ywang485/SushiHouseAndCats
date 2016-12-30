using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SushiManager : MonoBehaviour {

	public enum Sushi{
		TunaNigiri,
		CaliforniaRoll,
		SalmonNigiri,
		SalmonRoll,
		WhiteTunaNigiri,
		TamagoNigiri,
		NOTHING
	};

	// Prices
	static public readonly Dictionary<Sushi, int> sushiPrice = new Dictionary<Sushi, int>() {
		{Sushi.CaliforniaRoll, 10 },
		{Sushi.SalmonNigiri, 30},
		{Sushi.TunaNigiri, 25},
		{Sushi.WhiteTunaNigiri, 25},
		{Sushi.TamagoNigiri, 10},
		{Sushi.SalmonRoll, 30}
	};

	// Sprites
	public Sprite[] sushiSprite;

	public static int sushi2number(SushiManager.Sushi sushi) {
		if (sushi == SushiManager.Sushi.CaliforniaRoll) {
			return 0;
		} else if (sushi == SushiManager.Sushi.SalmonNigiri) {
			return 1;
		} else if (sushi == SushiManager.Sushi.TunaNigiri) {
			return 2;
		} else if (sushi == SushiManager.Sushi.WhiteTunaNigiri) {
			return 3;
		} else if (sushi == SushiManager.Sushi.TamagoNigiri) {
			return 4;
		} else if (sushi == SushiManager.Sushi.SalmonRoll) {
			return 5;
		}
		return -1;
	}

	public bool makeTunaNigiri(IngredientManager ingredientManager) {
		int numRice = 2;
		int numTuna = 1;

		if (ingredientManager.numRice >= numRice && ingredientManager.numTuna >= numTuna) {
			ingredientManager.consumeRice(numRice);
			ingredientManager.consumeTuna(numTuna);
			return true;
		} else {
			return false;
		}
	}

	public bool makeCaliforniaRoll(IngredientManager ingredientManager) {
		int numRice = 2;
		int numAvocado = 1;
		int numCucumber = 2;

		if (ingredientManager.numRice >= numRice && ingredientManager.numAvocado >= numAvocado && ingredientManager.numCucumber >= numCucumber) {
			ingredientManager.consumeAvocado (numAvocado);
			ingredientManager.consumeCucumber (numCucumber);
			ingredientManager.consumeRice (numRice);
			return true;
		} else {
			return false;
		}
	}

	public bool makeSalmonNigiri(IngredientManager ingredientManager) {
		int numRice = 2;
		int numSalmon = 1;

		if (ingredientManager.numRice >= numRice && ingredientManager.numSalmon >= numSalmon) {
			ingredientManager.consumeRice (numRice);
			ingredientManager.consumeSalmon (numSalmon);
			return true;
		} else {
			return false;
		}
	}

	public bool makeSalmonRoll(IngredientManager ingredientManager) {
		int numRice = 2;
		int numSalmon = 1;
		int numAvocado = 1;
		int numCucumber = 1;

		if (ingredientManager.numRice >= numRice && ingredientManager.numSalmon >= numSalmon && ingredientManager.numAvocado >= numAvocado && ingredientManager.numCucumber >= numCucumber) {
			ingredientManager.consumeRice (numRice);
			ingredientManager.consumeSalmon (numSalmon);
			ingredientManager.consumeAvocado (numAvocado);
			ingredientManager.consumeCucumber (numCucumber);
			return true;
		} else {
			return false;
		}
	}

	public bool makeWhiteTunaNigiri(IngredientManager ingredientManager) {
		int numRice = 2;
		int numWhiteTuna = 1;

		if (ingredientManager.numRice >= numRice && ingredientManager.numWhiteFish >= numWhiteTuna) {
			ingredientManager.consumeRice (numRice);
			ingredientManager.consumeWhiteFish (numWhiteTuna);
			return true;
		} else {
			return false;
		}
	}

	public bool makeTamagoNigiri(IngredientManager ingredientManager) {
		int numRice = 2;
		int numEgg = 1;

		if (ingredientManager.numRice >= numRice && ingredientManager.numEgg >= numEgg) {
			ingredientManager.consumeEgg (numEgg);
			ingredientManager.consumeRice (numRice);
			return true;
		} else {
			return false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
