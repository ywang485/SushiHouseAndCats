using UnityEngine;
using System.Collections;

public class IntervalGuest : Guest {

	public int interval2spawn = 20;
	public SushiManager.Sushi[] sushi2order;

	public override bool shouldSpawn(int totalMinute) {
		if(totalMinute % interval2spawn == 0) {
			return true;
		}
		else {
			return false;
		}
	}

	public override SushiManager.Sushi[] getSushiWanted() {
		return sushi2order;
	}
}
