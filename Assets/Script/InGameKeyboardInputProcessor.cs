using UnityEngine;
using System.Collections;

public class InGameKeyboardInputProcessor : MonoBehaviour {

	public GameObject buyIngredientMenu;
	public GameObject makeSushiMenu;

	[HideInInspector] public GameManager gameManager;

	private KeyCode[] keyCodes = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
	};

	// Use this for initialization
	void Start () {
		gameManager = GameManager.getGameManager ();
	}
	
	// Update is called once per frame
	void Update () {
	
		for (int i = 0; i < keyCodes.Length; i++) {
			if (Input.GetKeyDown (keyCodes [i])) {
				int numberPressed = i + 1;
				if (buyIngredientMenu.activeInHierarchy == true) {
					gameManager.buyIngredient(numberPressed);
				}
				else if(makeSushiMenu.activeInHierarchy == true) {
					gameManager.makeSushi(numberPressed);
				}
			}
		}

	}
}
