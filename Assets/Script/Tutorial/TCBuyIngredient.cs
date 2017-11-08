using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCBuyIngredient : TutorialChapter {

	// Use this for initialization
	void Start () {
		Debug.Log ("Tutorial Chapter: Buy Ingredient");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("BuyIngredientMenu")) {
			transform.Find ("Arrow-1").gameObject.SetActive (false);
			transform.Find ("Arrow-2").gameObject.SetActive (true);
			transform.Find ("Tutorial-BuyIngredient-1").gameObject.SetActive (false);
			transform.Find ("Tutorial-BuyIngredient-2").gameObject.SetActive (true);
		}
		if (PlayerDataManager.getPlayerData ().ingredients ["rice"] > 0 && PlayerDataManager.getPlayerData ().ingredients ["tuna"] > 0) {
			Destroy (gameObject);
		}
		
	}
}
