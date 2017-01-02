using UnityEngine;
using System.Collections;

public class CatState {

	protected readonly Cat cat;

	public CatState (Cat subjCat) {
		cat = subjCat;
	}

	public virtual void UpdateState() {
	}

	public virtual void OnLeftClick() {
		if (cat.hpsubj == null) {
			if (PlayerDataManager.playerData.catMoodIconEnabled) {
				cat.showMoodIcon (3);
			}
			ToEscaping ();
		} else {
			cat.hpsubj.beAttacked (GameManager.getGameManager().calculateDamage(0));
		}
	}

	public virtual void OnRightClick() {
		if (PlayerDataManager.playerData.galGameModeEnabled) {
			cat.gameManager.enterGalGameMode (cat.getStatusCode(), cat.getSushiWanted()[0]);
		}
		else if (PlayerDataManager.playerData.catPettingEnabled) {
			cat.pet ();
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D other) {
		
	}

	public virtual void ToLookingForFood() {
		cat.currState = cat.lookingForFoodState;
		cat.lookingForFoodState.resetSearchingStartTime ();
	}

	public virtual void ToGoTowardsFood() {
		cat.hideBubble ();
		cat.currState = cat.goTowardsFoodState;
		CatGoTowardsFoodState gtfs = (CatGoTowardsFoodState)cat.currState;
		gtfs.updateAnim ();
	}

	public virtual void ToEating() {
		cat.currState = cat.eatingState;
		cat.eatingState.resetEatingStartTime ();
	}

	public virtual void ToLeaving() {
		GameManager.getGameManager ().catManager.increaseCatPopularity (1);
		cat.currState = cat.leavingState;
		cat.animator.SetInteger ("animNo", 2);
		cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
		CatLeavingState ls = (CatLeavingState)cat.currState;
		ls.updateAnim ();
	}

	public virtual void ToEscaping() {
		if (cat.currState != cat.escapingState) {
			GameManager.getGameManager ().catManager.decreaseCatPopularity (2);
			cat.hideBubble ();
			cat.currState = cat.escapingState;
			CatEscapingState es = (CatEscapingState)cat.currState;
			es.updateAnim ();
			cat.gameManager.playMeowSFX (false);
			cat.animator.SetInteger ("animNo", 2);
			//cat.transform.localScale = new Vector3 (cat.transform.localScale.x * (-1), cat.transform.localScale.y, cat.transform.localScale.z);
		}

	}

}
