using UnityEngine;
using System.Collections;

public class EnterCatStore : GenericButton {

	// Use this for initialization
	void Awake () {
		base.hoverBehaviour = 2;
		base.clickAct = enterCatStore;
	}

	// Update is called once per frame
	void enterCatStore () {
	}
}
