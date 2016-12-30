using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MakeSushi : GenericInteractiveObject {

	public GameObject selectSushiUI;

	void Start () {
		base.functionText = "Make Sushi";
		base.actAct = makeSushi;
		base.deactAct = hideSushiSelectUI;
	}

	void hideSushiSelectUI() {
		selectSushiUI.SetActive (false);
	}

	void makeSushi() {
		gameManager.closeAllUI ();
		selectSushiUI.SetActive (true);
		//if (Input.GetButtonDown ("Fire1")) {
		//	base.gameManager.closeAllUI ();
		//	selectSushiUI.SetActive (true);
		//}

	}
}
