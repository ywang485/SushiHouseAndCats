using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

	private GameManager gameManager;

	// Weapon Prefab
	public int currWeaponIdx = 0;

	public GameObject[] weaponList;
	public bool[] weaponStatus;
	public int[] weaponCost;
	public int[] weaponAttack;

	// UI Element
	public Image weaponIndicator;

	void Awake() {
		gameManager = GameManager.getGameManager ();
	}

	void playWeaponAnimation(Vector2 targetLoc) {
		Transform playerTransform = GameObject.Find ("player").transform;
		GameObject weaponObj = (GameObject)GameObject.Instantiate (weaponList[currWeaponIdx], playerTransform.position, Quaternion.identity);
		BasicWeapon bw = weaponObj.GetComponent<BasicWeapon> ();
		bw.targetLoc = targetLoc;
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			if (!GameManager.clockPaused) {
				playWeaponAnimation (Camera.main.ScreenToWorldPoint (Input.mousePosition));
				gameManager.decreaseNumGold (weaponCost [currWeaponIdx]);
			}
		}
		SpriteRenderer r = weaponList [currWeaponIdx].GetComponent<SpriteRenderer> ();
		weaponIndicator.sprite = r.sprite;
		if (Input.GetKeyDown (KeyCode.Z)) {
			currWeaponIdx = (currWeaponIdx + 1) % weaponList.Length;
			while (!weaponStatus [currWeaponIdx]) {
				currWeaponIdx = (currWeaponIdx + 1) % weaponList.Length;
			}
		}
	}

}
