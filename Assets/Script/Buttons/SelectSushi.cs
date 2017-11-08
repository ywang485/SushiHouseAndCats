using UnityEngine;
using System.Collections;

public class SelectSushi : GenericButton {

	public string sushi;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		base.hoverBehaviour = 2;
		base.clickAct = makeSushi;

		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	void makeSushi() {

		//gameManager.makeSushi (sushi);

	}
}
