using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SushiManager : MonoBehaviour {

	static public readonly Dictionary<string, SushiType> sushiTypes = new Dictionary<string, SushiType>() {
		{"tuna-nigiri", new SushiType("tuna-nigiri", 25, "Tuna Nigiri", "Sprites/Sushi/tunaNigiri_s", "Sprites/Sushi/tunaNigiri_b", new Dictionary<string, int>(){{"rice", 2}, {"tuna", 1}})},
		{"california-roll", new SushiType("california-roll", 15, "California Roll", "Sprites/Sushi/californiaRoll_s", "Sprites/Sushi/californiaRoll_b", new Dictionary<string, int>(){{"rice", 2}, {"avocado", 1}, {"cucumber", 2}})},
		{"salmon-nigiri", new SushiType("salmon-nigiri", 30, "Salmon Nigiri", "Sprites/Sushi/salmonNigiri_s", "Sprites/Sushi/salmonNigiri_b", new Dictionary<string, int>(){{"rice", 2}, {"salmon", 1}})},
		{"salmon-roll", new SushiType("salmon-roll", 45, "Salmon Roll", "Sprites/Sushi/salmonRoll_s", "Sprites/Sushi/salmonRoll_b", new Dictionary<string, int>(){{"rice", 2}, {"avocado", 1}, {"cucumber", 1}, {"salmon", 1}})},
		{"white-tuna-nigiri", new SushiType("white-tuna-nigiri", 25, "White Tuna Nigiri", "Sprites/Sushi/whiteTunaNigiri_s", "Sprites/Sushi/whiteTunaNigiri_b", new Dictionary<string, int>(){{"rice", 2}, {"white-tuna", 1}})},
		{"tamago-nigiri", new SushiType("tamago-nigiri", 10, "Tamago Nigiri", "Sprites/Sushi/tamagoNigiri_s", "Sprites/Sushi/tamagoNigiri_b", new Dictionary<string, int>(){{"rice", 2}, {"egg", 1}})},
		{"tuna-sashimi", new SushiType("tuna-sashimi", 50, "Tuna Sashimi", "Sprites/Sushi/tunaSashimi_s", "Sprites/Sushi/tunaSashimi_b", new Dictionary<string, int>(){{"tuna", 5}})},
		{"peking-duck-nigiri", new SushiType("peking-duck-nigiri", 70, "Peking Duck Nigiri", "Sprites/Sushi/PekingDuckNigiri_s", "Sprites/Sushi/PekingDuckNigiri_b", new Dictionary<string, int>(){{"rice", 2}, {"duck-meat", 1}})}
	};

	public bool makeSushiCheck(string sushiId) {
		SushiType targetSushi = SushiManager.sushiTypes [sushiId];
		foreach (string ingredient in targetSushi.getIngredients().Keys) {
			if (!PlayerDataManager.getPlayerData().ingredients.ContainsKey(ingredient) || PlayerDataManager.getPlayerData().ingredients [ingredient] < targetSushi.getIngredients () [ingredient]) {
				return false;
			}
			GameManager.getGameManager ().ingredientManager.consumeIngredient (ingredient, targetSushi.getIngredients () [ingredient]);
		}
		return true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
