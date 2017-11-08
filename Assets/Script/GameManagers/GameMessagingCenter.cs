using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SCObserver {
	void OnNotify (SCEvent message);
}

public abstract class SCEvent {
	public abstract string getEventName();
}

public class GameStartFor5Min : SCEvent{
	public override string getEventName() {
		return GameMessagingCenter.evt_gameStartFor5MinStr;
	}
}

public class CatDefeatedComboStopEvent : SCEvent {
	public override string getEventName() {
		return GameMessagingCenter.evt_catDefeatedComboStopStr;
	}
}

public class CatFedComboStartEvent : SCEvent {
	public override string getEventName() {
		return GameMessagingCenter.evt_catFedComboStartStr;
	}
}

public class CatDefeatedComboStartEvent : SCEvent {
	public override string getEventName() {
		return GameMessagingCenter.evt_catDefeatedComboStartStr;
	}
}

public class CatFedComboStopEvent : SCEvent {
	public override string getEventName() {
		return GameMessagingCenter.evt_catFedComboStopStr;
	}
}

public class CatDefeatedEvent : SCEvent{
	private string catTypeId;

	public CatDefeatedEvent(string catTypeId) {
		this.catTypeId = catTypeId;
	}

	public override string getEventName() {
		return GameMessagingCenter.evt_catDefeatedStr;
	}

	public string getCatTypeId() {
		return this.catTypeId;
	}
}

public class CatFedEvent : SCEvent {
	private string catTypeId;
	private int catPopInc;

	public CatFedEvent(string catTypeId, int catPopInc) {
		this.catTypeId = catTypeId;
		this.catPopInc = catPopInc;
	}

	public override string getEventName() {
		return GameMessagingCenter.evt_catFedStr;
	}

	public string getCatTypeId() {
		return this.catTypeId;
	}

	public int getCatPopInc() {
		return catPopInc;
	}
}

public class CatEnemyLevelUpEvent : SCEvent {
	private int levelUpTo;
	public CatEnemyLevelUpEvent(int levelUpTo) {
		this.levelUpTo = levelUpTo;
	}
	public int getCurrEnemyLevel() {
		return this.levelUpTo;
	}
	public override string getEventName() {
		return GameMessagingCenter.evt_catEnemyLevelUpStr;
	}
}

public class CatFriendLevelUpEvent : SCEvent {
	private int levelUpTo;
	public CatFriendLevelUpEvent(int levelUpTo) {
		this.levelUpTo = levelUpTo;
	}
	public int getCurrFriendLevel() {
		return this.levelUpTo;
	}
	public override string getEventName() {
		return GameMessagingCenter.evt_catFriendLevelUpStr;
	}
}

public class NewSushiTypeUnlockedEvent : SCEvent {

	private string sushiTypeId;

	public NewSushiTypeUnlockedEvent(string sid) {
		this.sushiTypeId = sid;
	}
	public string getSushiTypeId() {
		return sushiTypeId;
	}
	public override string getEventName() {
		return GameMessagingCenter.evt_newSushiTypeUnlockedStr;
	}
}

public class NewIngredientUnlockedEvent : SCEvent {

	private string ingredientId;

	public NewIngredientUnlockedEvent(string iid) {
		this.ingredientId = iid;
	}
	public string getIngredientId() {
		return ingredientId;
	}
	public override string getEventName() {
		return GameMessagingCenter.evt_newIngredientUnlockedStr;
	}
}

public class NewItemObtainedEvent : SCEvent {
	private string itemId;

	public NewItemObtainedEvent(string iid) {
		this.itemId = iid;
	}

	public string getItemId() {
		return this.itemId;
	}

	public override string getEventName () {
		return GameMessagingCenter.evt_newItemObtainedStr;
	}
}

public class NewUIElementUnlockedEvent : SCEvent {
	private string uid;

	public NewUIElementUnlockedEvent(string uid) {
		this.uid = uid;
	}

	public string getUID() {
		return this.uid;
	}

	public override string getEventName () {
		return GameMessagingCenter.evt_newUIElementUnlockedStr;
	}
}

public class GameMessagingCenter : MonoBehaviour {

	static public string evt_catDefeatedStr = "CatDefeated";
	static public string evt_catFedStr = "CatFed";
	static public string evt_gameStartFor5MinStr = "GameStartFor5Min";
	static public string evt_catDefeatedComboStartStr = "CatDefeatedComboStart";
	static public string evt_catFedComboStopStr = "CatFedComboStop";
	static public string evt_catFedComboStartStr = "CatFedComboStart";
	static public string evt_catDefeatedComboStopStr = "CatDefeatedComboStop";
	static public string evt_catEnemyLevelUpStr = "CatEnemyLevelUp";
	static public string evt_catFriendLevelUpStr = "CatFriendLevelUp";
	static public string evt_newSushiTypeUnlockedStr = "NewSushiTypeUnlocked";
	static public string evt_newIngredientUnlockedStr = "NewIngredientUnlocked";
	static public string evt_newItemObtainedStr = "NewItemObtained";
	static public string evt_newUIElementUnlockedStr = "NewUIElementUnlocked";

	private Dictionary<string, List<SCObserver>> observerList;

	// Use this for initialization
	void Awake () {
		observerList = new Dictionary<string, List<SCObserver>> ();
		observerList.Add(evt_catDefeatedStr, new List<SCObserver>());
		observerList.Add(evt_catFedStr, new List<SCObserver>());
		observerList.Add(evt_gameStartFor5MinStr, new List<SCObserver>());
		observerList.Add (evt_catDefeatedComboStartStr, new List<SCObserver>());
		observerList.Add (evt_catFedComboStopStr, new List<SCObserver>());
		observerList.Add (evt_catFedComboStartStr, new List<SCObserver>());
		observerList.Add (evt_catDefeatedComboStopStr, new List<SCObserver>());
		observerList.Add (evt_catEnemyLevelUpStr, new List<SCObserver>());
		observerList.Add (evt_catFriendLevelUpStr, new List<SCObserver>());
		observerList.Add (evt_newSushiTypeUnlockedStr, new List<SCObserver> ());
		observerList.Add (evt_newIngredientUnlockedStr, new List<SCObserver> ());
		observerList.Add (evt_newItemObtainedStr, new List<SCObserver>());
		observerList.Add (evt_newUIElementUnlockedStr, new List<SCObserver>());

	}

	public void addObserver(SCObserver observer, string eventId) {
		observerList [eventId].Add (observer);
	}

	public void deleteObserver(SCObserver observer, string eventId) {
		observerList [eventId].Remove (observer);
	}

	private void notifyObserver(SCEvent evt) {
		foreach (SCObserver observer in observerList[evt.getEventName()]) {
			observer.OnNotify (evt);
			Debug.Log ("Event " + evt.getEventName() + " occurred.");
		}
	}

	public void eventHappened(SCEvent evt) {
		notifyObserver (evt);
		Debug.Log ("Event " + evt.getEventName() + " occurred.");
		Debug.Log ("Observer Count: " + observerList[evt.getEventName()].Count.ToString());
	}


}
