using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorial : MonoBehaviour, SCObserver {

	private string[] tutorialList = {
		"Prefabs/Tutorials/Movement",
		"Prefabs/Tutorials/BuyIngredient",
		"Prefabs/Tutorials/MakeSushi",
		"Prefabs/Tutorials/CounterSpace",
		"Prefabs/Tutorials/ServingGuest",
		"Prefabs/Tutorials/UseEquippedItem",
		"Prefabs/Tutorials/CatsStealingFood"
	};

	private string weaponStoreTutorial = "Prefabs/Tutorials/WeaponStore";

	private int currTutorial = 0;

	public void nextTutorial() {
		if (currTutorial < tutorialList.Length) {
			GameObject tutorialObj = GameObject.Instantiate (Resources.Load (tutorialList [currTutorial], typeof(GameObject)) as GameObject, this.transform);
			currTutorial += 1;
		} else {
			PlayerDataManager.getPlayerData ().catSpawning = true;
			PlayerDataManager.getPlayerData ().guestSpawning = true;
		}
	}
	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(NewUIElementUnlockedEvent)) {
			NewUIElementUnlockedEvent nuieu = (NewUIElementUnlockedEvent) evt;
			if (nuieu.getUID () == "weapon-store") {
				GameObject tutorialObj = GameObject.Instantiate (Resources.Load(weaponStoreTutorial, typeof(GameObject)) as GameObject, this.transform);
			}
		}
	}

	void Start() {
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_newUIElementUnlockedStr);
		nextTutorial();
	}
}
