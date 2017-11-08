using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	public enum LocType{
		Floor,
		Table,
		Counter,
		Door,
		Window
	};

	// Locations
	public GameObject doorLoc;
	public GameObject counterLoc;
	public Table[] tables;

	public GameObject[] cat_enterLocs;
	public GameObject[] sushiOnCounterIndicators;

	// Use this for initialization
	protected virtual void init () {
	}

	void Awake() {
		init ();
	}

	public GameObject getSushiIndicator(int spaceNo) {
		return sushiOnCounterIndicators [spaceNo];
	}

	public Table findAvailableTable() {

		ArrayList available_tables = new ArrayList ();

		for (int i = 0; i < tables.Length; i++) {
			if (tables [i].availability == true) {
				available_tables.Add(tables [i]);
			}
		}

		if (available_tables.Count <= 0) {
			return null;
		}

		Table target_table = (Table) available_tables [Random.Range (0, available_tables.Count)];

		return target_table;
	}

	public int findAvailableSushiLocOnCounter() {
		int no = -1; 

		for (int i = 0; i < sushiOnCounterIndicators.Length; i++) {
			if (sushiOnCounterIndicators [i].activeInHierarchy == false) {
				no = i;
				break;
			}
		}

		return no;
	}
		
	public void setSushiOnCounter(int loc, string sushi){
		if (sushi == "NOTHING") {
			sushiOnCounterIndicators [loc].SetActive (false);
			return;
		}
		sushiOnCounterIndicators [loc].SetActive (true);
		Sushi sushiObj = sushiOnCounterIndicators[loc].GetComponent<Sushi>();
		sushiObj.setSushiType(sushi);
	}

	public int findSushiOnCounter() {
		int no = -1; 

		for (int i = 0; i < sushiOnCounterIndicators.Length; i++) {
			if (sushiOnCounterIndicators [i].activeInHierarchy == false) {
				no = i;
				break;
			}
		}

		return no;
	}

	public Sushi findSpecificSushiOnCounter(string sushiType, bool untouched = false) {

		Sushi sushiPlateFound = null;
		for (int i = 0; i < sushiOnCounterIndicators.Length; i++) {
			if (!sushiOnCounterIndicators [i].activeInHierarchy) {
				continue;
			}
			Sushi sushiObj = sushiOnCounterIndicators [i].GetComponent<Sushi> ();
			if (sushiObj.getSushiType() == sushiType) {
				if (untouched) {
					HPSubject hpSub = sushiOnCounterIndicators [i].GetComponent<HPSubject> ();
					if (hpSub.HP < hpSub.maxHP) {
						continue;
					}
				}
				sushiPlateFound = sushiObj;
				break;
			}
		}

		return sushiPlateFound;
	}

	public Location findCatSpawningLoc() {
		int spawn_loc = Random.Range(0, cat_enterLocs.Length);
		return cat_enterLocs [spawn_loc].GetComponent<Location> ();
	}

	public Vector2 getDoorLocation() {
		return doorLoc.transform.position;
	}

	public Vector2 getCounterLocation() {
		return counterLoc.transform.position;
	}
}
