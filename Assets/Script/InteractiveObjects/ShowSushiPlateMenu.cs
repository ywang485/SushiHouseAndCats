using UnityEngine;
using System.Collections;

public class ShowSushiPlateMenu : GenericInteractiveObject {

	public DiscardBtn DiscardBtn;

	// Use this for initialization
	void Awake () {
		base.functionText = "Sushi Plate Action";
		base.actAct = showSushiPlateMenu;
		base.deactAct = hideSushiPlateMenu;
	}
	
	void showSushiPlateMenu() {
		base.gameManager.closeAllUI ();
		DiscardBtn.gameObject.SetActive (true);
	}

	void hideSushiPlateMenu() {
		DiscardBtn.gameObject.SetActive (false);
	}
}
