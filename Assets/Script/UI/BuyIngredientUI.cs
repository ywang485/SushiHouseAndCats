using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuyIngredientUI : MonoBehaviour, SCObserver {

	static private readonly string buyIngredientBtnPrefab = "Prefabs/BuyIngredientButton";
	private GameManager gameManager;
	int posXoffset = 110;
	int posYoffset = -100;
	int numBtn;

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(NewIngredientUnlockedEvent)) {
			NewIngredientUnlockedEvent niu = (NewIngredientUnlockedEvent)evt;
			addIngredientBtn (niu.getIngredientId());
		}

	}

	// Use this for initialization
	void Start () {
		int count = 0;
		gameManager = GameManager.getGameManager ();
		foreach (string iid in PlayerDataManager.getPlayerData().ingredients.Keys) {
			GameObject ingredientBtn = GameObject.Instantiate (Resources.Load (buyIngredientBtnPrefab, typeof(GameObject)) as GameObject, transform);
			ingredientBtn.GetComponentInChildren<Button> ().onClick.AddListener (() => gameManager.buyIngredient(iid));
			ingredientBtn.transform.Find ("IngredientId").GetComponent<Text> ().text = IngredientManager.ingredients[iid].getDescription();
			ingredientBtn.GetComponentInChildren<Button> ().GetComponent<Image>().sprite = Resources.Load (IngredientManager.ingredients[iid].getSpritePathB(), typeof(Sprite)) as Sprite;
			ingredientBtn.transform.Find ("Price").GetComponent<Text> ().text = IngredientManager.ingredients[iid].getPrice().ToString() + "G/10";
			ingredientBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (posXoffset * count, posYoffset);

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener((data) => { showIngredientId(iid); });
			ingredientBtn.GetComponentInChildren<EventTrigger> ().triggers.Add(entry);

			EventTrigger.Entry entry2 = new EventTrigger.Entry ();
			entry2.eventID = EventTriggerType.PointerExit;
			entry2.callback.AddListener ((data) => {hideRecipe();});
			ingredientBtn.GetComponentInChildren<EventTrigger>().triggers.Add(entry2);

			count += 1;
		}
		numBtn = count;
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_newIngredientUnlockedStr);
	}

	void addIngredientBtn(string iid) {
		GameObject ingredientBtn = GameObject.Instantiate (Resources.Load (buyIngredientBtnPrefab, typeof(GameObject)) as GameObject, transform);
		ingredientBtn.GetComponentInChildren<Button> ().onClick.AddListener (() => gameManager.buyIngredient(iid));
		ingredientBtn.transform.Find ("IngredientId").GetComponent<Text> ().text = IngredientManager.ingredients[iid].getDescription();
		ingredientBtn.GetComponentInChildren<Button> ().GetComponent<Image>().sprite = Resources.Load (IngredientManager.ingredients[iid].getSpritePathB(), typeof(Sprite)) as Sprite;
		ingredientBtn.transform.Find ("Price").GetComponent<Text> ().text = IngredientManager.ingredients[iid].getPrice().ToString() + "G/10";
		ingredientBtn.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (posXoffset * numBtn, posYoffset);

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener((data) => { showIngredientId(iid); });
		ingredientBtn.GetComponentInChildren<EventTrigger> ().triggers.Add(entry);

		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener ((data) => {hideRecipe();});
		ingredientBtn.GetComponentInChildren<EventTrigger>().triggers.Add(entry2);
		numBtn += 1;
	}

	void showIngredientId(string ingredientId) {
		GetComponentInChildren<Text> ().text = ingredientId;
	}

	void hideRecipe() {
		GetComponentInChildren<Text> ().text = "";
	}
}
