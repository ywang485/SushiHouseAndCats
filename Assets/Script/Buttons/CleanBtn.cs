using UnityEngine;
using System.Collections;

public class CleanBtn : GenericButton {

	// Use this for initialization
	void Awake () {
		base.hoverBehaviour = 2;
		base.clickAct = cleanObject;
	}

	// Update is called once per frame
	void cleanObject () {
		//gameObject.SetActive (false);
		GameObject.Destroy(transform.parent.gameObject);
	}
}
