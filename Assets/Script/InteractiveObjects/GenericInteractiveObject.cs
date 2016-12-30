using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenericInteractiveObject : MonoBehaviour {

	[HideInInspector] public GameManager gameManager;
	protected bool isActive;

	protected string functionText;

	protected delegate void activationAction ();
	protected activationAction actAct;

	protected delegate void deactiveAction();
	protected deactiveAction deactAct;

	void Start() {
		gameManager = GameManager.getGameManager ();
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			gameManager.showBottomText (functionText);
			isActive = true;
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		
		if (other.gameObject.tag == "Player") {
			gameManager.hideBottomText ();
			isActive = false;
			if (deactAct != null) {
				deactAct ();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			actAct();
		}
	}
}
