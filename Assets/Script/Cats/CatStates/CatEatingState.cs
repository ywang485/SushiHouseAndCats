using UnityEngine;
using System.Collections;

public class CatEatingState : CatState {

	private int eatingStartTime;
	private int lastActivationTime;

	public CatEatingState(Cat subjCat) : base(subjCat) {

	}

	public void resetEatingStartTime() {
		eatingStartTime = cat.gameManager.getCurrTimeInMinute();
	}

	void eat(int totalMinute) {

		if (totalMinute == lastActivationTime) {
			return;
		}
		GameObject targetSushi = cat.getTargetSushiPlate ();
		int catPopInc;
		Food food = targetSushi.GetComponent<Food> ();
		if (food == null) {
			catPopInc = 2;
		} else {
			catPopInc = food.catPopInc;
		}

		if (totalMinute % cat.getEatingSpeed() == 0) {


			if (targetSushi == null) {
				Debug.Log ("target Sushi is Null. ");
			}
			GameObject obj = targetSushi.gameObject;
			HPSubject sub = obj.GetComponent<HPSubject> ();
			Debug.Log ("sub: " + sub);
			if (sub == null) {
				GameManager.getGameManager ().catManager.increaseCatPopularity (catPopInc, cat.catTypeId, 1);
				if (PlayerDataManager.getPlayerData().catMoodIconEnabled) {
					cat.showMoodIcon (2);
				}
				cat.towardsFood = false;
				Food foodObj = targetSushi.GetComponent<Food> ();
				Sprite foodSprite = foodObj.finishedSprite;
				SpriteRenderer sr = (SpriteRenderer)targetSushi.GetComponent<SpriteRenderer> ();
				sr.sprite = foodSprite;
				foodObj.finished = true;
				ToLeaving ();
				//GameObject.Destroy (targetSushi.gameObject);
				return;
			}
			sub.beAttacked (cat.getEatingPower ());

			if (sub.HP <= 0) {
				GameManager.getGameManager ().messagingCenter.eventHappened (new CatFedEvent (cat.catTypeId, catPopInc));
				if (PlayerDataManager.getPlayerData().catMoodIconEnabled) {
					cat.showMoodIcon (2);
				}
				cat.towardsFood = false;
				ToLeaving ();
			}

			lastActivationTime = totalMinute;

		}

	}

	public override void UpdateState ()
	{
		eat (cat.gameManager.getCurrTimeInMinute () - eatingStartTime);

	}

}
