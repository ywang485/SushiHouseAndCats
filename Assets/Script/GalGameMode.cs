using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GalGameMode : MonoBehaviour {

	public SushiManager.Sushi sushiWanted;
	public int statusCode;

	public bool named;

	public Text text;

	public void updateInitialText() {
		if (statusCode == 1) {
			text.text = "Cat looks hungry.";
		} else if (statusCode == 2) {
			text.text = "Cat looks satisfied.";
		} else if (statusCode == 3) {
			text.text = "Cat is busy eating.";
		} else if (statusCode == 4) {
			text.text = "Cat looks scared.";
		} else {
			text.text = "Looks like a badly drawn cat.";
		}
	}

	public void talkToCat() {
		if (named) {
			text.text = "Steve: \"Meow\"";
		} else {
			text.text = "Cat: \"Meow\"";
		}
	}

	public void GiveName() {
		text.text = "I will call you Steve from now on, little cat.";
		named = true;
	}

	public void Pet() {
		if (named) {
			text.text = "Steve looks happy.";
		} else {
			text.text = "Cat looks happy.";
		}

		GameManager.getGameManager ().catManager.increaseCatPopularity (3);
	}

	public void ask() {

		string sushiName = "delicious sushi";

		if (sushiWanted == SushiManager.Sushi.CaliforniaRoll) {
			sushiName = "california roll";
		} else if (sushiWanted == SushiManager.Sushi.SalmonNigiri) {
			sushiName = "salmon nigiri";
		} else if (sushiWanted == SushiManager.Sushi.SalmonRoll) {
			sushiName = "salmon roll";
		} else if (sushiWanted == SushiManager.Sushi.TamagoNigiri) {
			sushiName = "tamago nigiri";
		} else if (sushiWanted == SushiManager.Sushi.TunaNigiri) {
			sushiName = "tuna nigiri";
		} else if (sushiWanted == SushiManager.Sushi.WhiteTunaNigiri) {
			sushiName = "white tuna nigiri";
		}

		if (named) {
			text.text = "Steve wants " + sushiName + ".";
		} else {
			text.text = "Steve wants " + sushiName + ".";
		}
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
