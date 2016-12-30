using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {

	public bool availability;
	public Sushi sushiPlate;

	void Awake() {
		//GameObject sushiObj = transform.GetComponentInChildren<Sushi> ().gameObject;.FindChild ("SushiPlate").gameObject;
		//sushiPlate = sushiObj.GetComponent<Sushi> ();
		availability = true;
	}

}
