using UnityEngine;
using System.Collections;

// Guests who come at fixed times
public class RegularGuest : Guest {

	public int time2spawn;
	public SushiManager.Sushi[] sushi2order;

	public override bool shouldSpawn(int totalMinute) {
		if (gameManager == null) {
			gameManager = GameManager.getGameManager ();
		}
		if (gameManager.getCurrTimeInMinute () == time2spawn) {
			return true;
		} else {
			return false;
		}
	}

	public override SushiManager.Sushi[] getSushiWanted() {
		return sushi2order;
	}

}
