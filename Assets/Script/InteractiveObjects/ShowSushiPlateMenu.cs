using UnityEngine;
using System.Collections;

public class ShowSushiPlateMenu : GenericInteractiveObject {

	public GameObject DiscardBtn;

	// Use this for initialization
	void Awake () {
		base.functionText = "Sushi Plate Action";
		base.actAct = showSushiPlateMenu;
		base.deactAct = hideSushiPlateMenu;
	}
	
	void showSushiPlateMenu() {
		base.gameManager.closeAllUI ();
		DiscardBtn.SetActive (true);
	}

	void hideSushiPlateMenu() {
		DiscardBtn.SetActive (false);
	}
}
