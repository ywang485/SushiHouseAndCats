using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour, SCObserver {

	private GameManager gameManager;

	// Weapon Prefab
	public int currWeaponIdx = 0;

	public List<string> weaponList;
	//public int[] weaponCost;

	public Aim aim;

	// UI Element
	public Image weaponIndicator;

	private float cooldownUntil = 0f;

	void Awake() {
		gameManager = GameManager.getGameManager ();
		createdWeaponList ();
	}

	void Start() {
		gameManager.messagingCenter.addObserver (this, GameMessagingCenter.evt_newItemObtainedStr);
	}

	public void OnNotify(SCEvent evt) {
		if (evt.GetType () == typeof(NewItemObtainedEvent)) {
			createdWeaponList ();
		}
	}

	public int getWeaponDamage() {
		return ItemDatabase.getItem (weaponList [currWeaponIdx]).getAttack ();
	}

	void playWeaponAnimation(Vector2 targetLoc, GameObject weaponPrefab) {
		Transform playerTransform = GameObject.Find ("player").transform;
		GameObject weaponObj = (GameObject)GameObject.Instantiate (weaponPrefab, playerTransform.position, Quaternion.identity);
		Item bw = weaponObj.GetComponent<Item> ();
		bw.targetLoc = targetLoc;
	}

	void createdWeaponList() {
		weaponList = new List<string> ();
		List<string> itemIds = new List<string>(PlayerDataManager.getPlayerData().inventory.Keys);
		for (int i = 0; i < itemIds.Count; i++) {
			if (ItemDatabase.getItem (itemIds [i]).getAttack () > 0) {
				weaponList.Add (itemIds [i]);
				Debug.Log (itemIds [i]);
			}
		}
		//Debug.Log (weaponList.Count);
	}

	void Update() {
		if (Input.GetMouseButton(1)) {
			if (!GameManager.clockPaused && Time.time >= cooldownUntil) {
				GameObject weaponPrefab = Resources.Load(ItemDatabase.getItem (weaponList[currWeaponIdx]).getPrefabPath(), typeof(GameObject)) as GameObject;
				if (weaponPrefab.GetComponent<MeleeWeapon> () != null) {
					if (!MeleeWeapon.hasInstance) {
						MeleeWeapon.hasInstance = true;
						playWeaponAnimation (Camera.main.ScreenToWorldPoint (Input.mousePosition), weaponPrefab);
					}
				} else {
					playWeaponAnimation (Camera.main.ScreenToWorldPoint (Input.mousePosition), weaponPrefab);
				}
				cooldownUntil = Time.time + PlayerDataManager.getPlayerData().cooldown;
				//gameManager.decreaseNumGold (weaponCost [currWeaponIdx]);
			}
		}
		string spritePath = ItemDatabase.getItem(weaponList [currWeaponIdx]).getSpritePath();
		weaponIndicator.sprite = Resources.Load(spritePath, typeof(Sprite)) as Sprite;
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			currWeaponIdx = (currWeaponIdx + 1) % weaponList.Count;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			currWeaponIdx = currWeaponIdx - 1;
			if (currWeaponIdx < 0) {
				currWeaponIdx = weaponList.Count - 1;
			}
		}
	}

}
