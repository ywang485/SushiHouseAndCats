using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, SCObserver {

	static readonly public string catEnemyLevelIndicatorPath = "Prefabs/UI/CatEnemyLevelIndicator";
	static readonly public string catFriendLevelIndicatorPath = "Prefabs/UI/CatFriendLevelIndicator";

	private GameObject catEnemyLevelIndicator;
	private GameObject catFriendLevelIndicator;
	private GameObject canvas;

	public GameObject weaponStoreBtn;

	// Use this for initialization
	void Start () {
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_catDefeatedComboStartStr);
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_catDefeatedComboStopStr);
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_catFedComboStartStr);
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_catFedComboStopStr);
		GameManager.getGameManager ().messagingCenter.addObserver (this, GameMessagingCenter.evt_catEnemyLevelUpStr);
		canvas = GameObject.Find ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(CatDefeatedComboStartEvent)) {
			catEnemyLevelIndicator = GameObject.Instantiate (Resources.Load (catEnemyLevelIndicatorPath, typeof(GameObject)) as GameObject, canvas.transform);
		} else if (evt.GetType () == typeof(CatDefeatedComboStopEvent)) {
			GameObject.Destroy (catEnemyLevelIndicator);
		} else if (evt.GetType () == typeof(CatFedComboStartEvent)) {
			catFriendLevelIndicator = GameObject.Instantiate (Resources.Load (catFriendLevelIndicatorPath, typeof(GameObject)) as GameObject, canvas.transform);
		} else if (evt.GetType () == typeof(CatFedComboStopEvent)) {
			GameObject.Destroy(catFriendLevelIndicator);
		} else if(evt.GetType() == typeof(CatEnemyLevelUpEvent)) {
			CatEnemyLevelUpEvent celu = (CatEnemyLevelUpEvent)evt;
			if (celu.getCurrEnemyLevel () == 1) {
				showWeaponStoreBtn ();
			}
		}
	
	}

	public void showWeaponStoreBtn() {
		weaponStoreBtn.SetActive (true);
		GameManager.getGameManager ().messagingCenter.eventHappened (new NewUIElementUnlockedEvent("weapon-store"));
	}
}
