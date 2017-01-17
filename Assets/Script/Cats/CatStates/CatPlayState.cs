using UnityEngine;
using System.Collections;

public class CatPlayState : CatState {

	private int playingStartTime;
	private int lastActivationTime;

	public CatPlayState(Cat subjCat) : base(subjCat) {

	}

	public void resetPlayingStartTime() {
		playingStartTime = cat.gameManager.getCurrTimeInMinute();
	}

	void play(int totalMinute) {

		//if (totalMinute == lastActivationTime) {
		//	return;
		//}

		if (totalMinute > 20) {
			cat.gameManager.catManager.increaseCatPopularity (1);
			cat.lookingForFoodState.resetSearchingStartTime ();
			cat.towardsFood = false;
			cat.played = true;
			Debug.Log ("Cat play state exited");
			ToLookingForFood ();
		}

	}

	public override void UpdateState ()
	{
		play (cat.gameManager.getCurrTimeInMinute () - playingStartTime);

	}

}