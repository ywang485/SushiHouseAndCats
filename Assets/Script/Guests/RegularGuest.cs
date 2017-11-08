using UnityEngine;
using System.Collections;

// Guests who come at fixed times
public class RegularGuest : Guest {

	public int time2spawn;
	public string[] sushi2order;

//	public override bool shouldSpawn(int totalMinute) {
//		if (gameManager == null) {
//			gameManager = GameManager.getGameManager ();
//		}
//		if (gameManager.getCurrTimeInMinute () == time2spawn) {
//			return true;
//		} else {
//			return false;
//		}
//	}

	public override string[] getSushiWanted() {
		return sushi2order;
	}

}
