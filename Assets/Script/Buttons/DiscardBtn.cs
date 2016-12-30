using UnityEngine;
using System.Collections;

public class DiscardBtn : GenericButton {

	// Use this for initialization
	void Awake () {
		base.hoverBehaviour = 2;
		base.clickAct = discardSushiPlate;
	}
	
	// Update is called once per frame
	void discardSushiPlate () {
		//gameObject.SetActive (false);
		Sushi sushiPlate = GetComponentInParent<Sushi> ();
		sushiPlate.gameObject.SetActive (false);
	}
}
