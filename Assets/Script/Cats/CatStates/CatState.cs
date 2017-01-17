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
//		if (cat.hpsubj == null) {
//			if (PlayerDataManager.playerData.catMoodIconEnabled) {
//				cat.showMoodIcon (3);
//			}
//			ToEscaping ();
//		} else {
//			cat.hpsubj.beAttacked (GameManager.getGameManager().calculateDamage(0));
//		}
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
		cat.towardsFood = true;
		cat.hideBubble ();
		cat.currState = cat.goTowardsFoodState;
		cat.targetPosition = cat.getTargetSushiPlate ().gameObject.transform.position;
	}

	public virtual void ToEating() {
		cat.currState = cat.eatingState;
		cat.eatingState.resetEatingStartTime ();
	}

	public virtual void ToLeaving() {
			GameManager.getGameManager ().catManager.increaseCatPopularity (1);
			cat.targetPosition = GameManager.getGameManager ().mapManager.getDoorLocation ();
			cat.currState = cat.leavingState;
	}

	public virtual void ToPlay() {
		cat.playState.resetPlayingStartTime();
		cat.currState = cat.playState;
	}

	public virtual void ToEscaping() {
		if (cat.currState != cat.escapingState) {
			cat.towardsFood = false;
			cat.targetPosition = GameManager.getGameManager ().mapManager.getDoorLocation ();
			GameManager.getGameManager ().catManager.decreaseCatPopularity (2);
			cat.hideBubble ();
			cat.currState = cat.escapingState;
			cat.gameManager.playMeowSFX (false);
		}

	}

}
