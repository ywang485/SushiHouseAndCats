using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSushiUnlockedBox : SCUI {

	private Text sushiNameAndIngredientsText;
	private Image sushiSprite;

	// Use this for initialization
	void Awake () {
		sushiNameAndIngredientsText = transform.Find("SushiNameAndIngredientsText").GetComponent<Text>();
		sushiSprite = transform.Find("Image").GetComponent<Image>();
		GameManager.getGameManager ().pauseClock ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		GameManager.getGameManager ().unpauseClock ();
	}

	public void setContent(string sushiTypeId) {
		if (SushiManager.sushiTypes.ContainsKey (sushiTypeId)) {
			sushiSprite.sprite = Resources.Load (SushiManager.sushiTypes[sushiTypeId].getSpritePath_b(), typeof(Sprite)) as Sprite;
			string recipe = "";
			Dictionary<string, int> ingredients = SushiManager.sushiTypes [sushiTypeId].getIngredients ();
			foreach(string ingredient in ingredients.Keys) {
				recipe += ingredient + " x " + ingredients[ingredient].ToString() + ",";
			}
			sushiNameAndIngredientsText.text = SushiManager.sushiTypes [sushiTypeId].getId () + "\n" + recipe + "\n" + "Sell Price: " + SushiManager.sushiTypes [sushiTypeId].getPrice(); 
		}
	}
}
