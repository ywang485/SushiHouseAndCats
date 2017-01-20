using UnityEngine;
using System.Collections;

public class CatPlayState : CatState {

	private int playingStartTime;
	private int lastActivationTime;
	public int totalPlayTime;

	public CatPlayState(Cat subjCat) : base(subjCat) {

	}

	public void resetPlayingStartTime() {
		playingStartTime = cat.gameManager.getCurrTimeInMinute();
	}

	void play(int totalMinute) {

		//if (totalMinute == lastActivationTime) {
		//	return;
		//}

		if (cat.getTargetSushiPlate () == null) {
			cat.lookingForFoodState.resetSearchingStartTime ();
			cat.towardsFood = false;
			ToLookingForFood ();
		}

		if (totalMinute > totalPlayTime) {
			Toy toy = cat.getTargetSushiPlate ().GetComponent<Toy> ();
			cat.gameManager.catManager.increaseCatPopularity (toy.catPopInc);
			cat.lookingForFoodState.resetSearchingStartTime ();
			cat.towardsFood = false;
			cat.played = true;
			Debug.Log ("Cat play state exited");
			ToLookingForFood ();
			HPSubject hpSub = cat.getTargetSushiPlate ().GetComponent<HPSubject> ();
			if (hpSub != null) {
				hpSub.beAttacked (1);
			}
			if (hpSub == null || hpSub.HP <= 0) {
				toy.finished = true;
				Sprite finishedSprite = toy.finishedSprite;
				SpriteRenderer sr = (SpriteRenderer)toy.gameObject.GetComponent<SpriteRenderer> ();
				sr.sprite = finishedSprite;
			}
			cat.targetPosition = cat.lookingForFoodState.wayPoints [cat.lookingForFoodState.nextWayPoint].position;
		}

	}

	public override void UpdateState ()
	{
		play (cat.gameManager.getCurrTimeInMinute () - playingStartTime);

	}

}