using UnityEngine;
using System.Collections;

public class QuitGame : GenericButton {
	
	void Start() {
		base.clickAct = quitGame;
	}

	void quitGame() {
		Application.Quit ();
	}
}
