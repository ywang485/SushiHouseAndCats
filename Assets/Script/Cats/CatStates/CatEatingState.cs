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

		if (totalMinute % cat.getEatingSpeed() == 0) {

			GameObject targetSushi = cat.getTargetSushiPlate ();
			if (targetSushi == null) {
				Debug.Log ("target Sushi is Null. ");
			}
			GameObject obj = targetSushi.gameObject;
			HPSubject sub = obj.GetComponent<HPSubject> ();
			Debug.Log ("sub: " + sub);
			if (sub == null) {
				GameManager.getGameManager ().catManager.increaseCatPopularity (2);
				if (PlayerDataManager.playerData.catMoodIconEnabled) {
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
				ToLeaving ();
				return;
			}
			sub.beAttacked (cat.getEatingPower ());

			if (sub.HP <= 0) {
				GameManager.getGameManager ().catManager.increaseCatPopularity (2);
				if (PlayerDataManager.playerData.catMoodIconEnabled) {
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
