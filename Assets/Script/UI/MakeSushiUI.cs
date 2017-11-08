using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakeSushiUI : MonoBehaviour, SCObserver {

	static private readonly string makeSushiBtnPrefab = "Prefabs/MakeSushiButton";
	private GameManager gameManager;
	int posXoffset = 130;
	int posYoffset = -100;
	int numBtn;

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(NewSushiTypeUnlockedEvent)) {
			NewSushiTypeUnlockedEvent nstu = (NewSushiTypeUnlockedEvent)evt;
			addSushiBtn (nstu.getSushiTypeId());
		}

	}

	void addSushiBtn(string sid) {
		GameObject makeSushiBtn = GameObject.Instantiate (Resources.Load (makeSushiBtnPrefab, typeof(GameObject)) as GameObject, transform);
		makeSushiBtn.transform.Find ("SushiId").GetComponent<Text> ().text = SushiManager.sushiTypes[sid].getDescription();
		makeSushiBtn.GetComponentInChildren<Button> ().GetComponent<Image>().sprite = Resources.Load (SushiManager.sushiTypes[sid].getSpritePath_b(), typeof(Sprite)) as Sprite;
		makeSushiBtn.transform.Find ("Price").GetComponent<Text> ().text = SushiManager.sushiTypes[sid].getPrice().ToString() + "G";

		makeSushiBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (posXoffset * numBtn, posYoffset);

		makeSushiBtn.GetComponentInChildren<Button> ().onClick.AddListener (() => gameManager.makeSushi(sid));

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener((data) => { showRecipe(sid); });
		makeSushiBtn.GetComponentInChildren<EventTrigger> ().triggers.Add(entry);

		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener ((data) => {hideRecipe();});
		makeSushiBtn.GetComponentInChildren<EventTrigger>().triggers.Add(entry2);
		numBtn += 1;
	}

	// Use this for initialization
	void Start () {
		int count = 0;
		gameManager = GameManager.getGameManager ();
		foreach (string sid in PlayerDataManager.unlockedSushiType) {
			GameObject makeSushiBtn = GameObject.Instantiate (Resources.Load (makeSushiBtnPrefab, typeof(GameObject)) as GameObject, transform);
			makeSushiBtn.transform.Find ("SushiId").GetComponent<Text> ().text = SushiManager.sushiTypes[sid].getDescription();
			makeSushiBtn.GetComponentInChildren<Button> ().GetComponent<Image>().sprite = Resources.Load (SushiManager.sushiTypes[sid].getSpritePath_b(), typeof(Sprite)) as Sprite;
			makeSushiBtn.transform.Find ("Price").GetComponent<Text> ().text = SushiManager.sushiTypes[sid].getPrice().ToString() + "G";

			makeSushiBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (posXoffset * count, posYoffset);

			makeSushiBtn.GetComponentInChildren<Button> ().onClick.AddListener (() => gameManager.makeSushi(sid));

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener((data) => { showRecipe(sid); });
			makeSushiBtn.GetComponentInChildren<EventTrigger> ().triggers.Add(entry);

			EventTrigger.Entry entry2 = new EventTrigger.Entry ();
			entry2.eventID = EventTriggerType.PointerExit;
			entry2.callback.AddListener ((data) => {hideRecipe();});
			makeSushiBtn.GetComponentInChildren<EventTrigger>().triggers.Add(entry2);
			count += 1;
		}
		numBtn = count;
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_newSushiTypeUnlockedStr);
	}

	void showRecipe(string sushiId) {
		string recipe = sushiId + ": ";
		Dictionary<string, int> ingredients = SushiManager.sushiTypes [sushiId].getIngredients ();
		foreach(string ingredient in ingredients.Keys) {
			recipe += ingredient + " x " + ingredients[ingredient].ToString() + ",";
		}
		GetComponentInChildren<Text> ().text = recipe;
	}

	void hideRecipe() {
		GetComponentInChildren<Text> ().text = "";
	}
}
