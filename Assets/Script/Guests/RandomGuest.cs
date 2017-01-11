using UnityEngine;
using System.Collections;

public class RandomGuest : Guest {

	public int baseSpawningRate = 20;
	public int interval2spawn = 20;
	public SushiManager.Sushi[] sushi2order;

//	public override bool shouldSpawn(int totalMinute) {
//		if (gameManager == null) {
//			gameManager = GameManager.getGameManager ();
//		}
//		if(totalMinute % interval2spawn == 0) {
//			int r = Random.Range (0, 100);
//
//			if (r <= (float)baseSpawningRate + (float)gameManager.guestManager.humanPopularity / 20.0 * 100.0) {
//				return true;
//			} else {
//				return false;
//			}
//		}
//		else {
//			return false;
//		}
//	}

	public override SushiManager.Sushi[] getSushiWanted() {
		return sushi2order;
	}
}
