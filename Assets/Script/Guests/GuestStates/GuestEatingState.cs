using UnityEngine;
using System.Collections;

public class GuestEatingState : GuestState {

	private int eatingStartTime;
	private GameManager gameManager;

	public GuestEatingState(Guest subjGuest) : base(subjGuest) {
		gameManager = GameManager.getGameManager ();
	}

	public void resetEatingStartTime() {
		eatingStartTime = gameManager.getCurrTimeInMinute();
	}

	public override void UpdateState () {
		HPSubject hpSub = guest.target_table.sushiPlate.GetComponent<HPSubject> ();
		if (hpSub.HP < hpSub.maxHP) {
			guest.showMoodIcon (1);
			PlayerDataManager.getPlayerData().humanPopularity -= guest.getPopValueDec ();
			ToLeaving ();
			gameManager.playSFX (GameManager.hurtSFX);
		}
		if (!guest.target_table.sushiPlate.gameObject.activeInHierarchy) {
			guest.showMoodIcon (1);
			PlayerDataManager.getPlayerData().humanPopularity -= guest.getPopValueDec ();
			gameManager.playSFX (GameManager.hurtSFX);
			ToLeaving ();
		}
		int eatingTotalTime = gameManager.getCurrTimeInMinute() - eatingStartTime;
		if (eatingTotalTime > guest.getEatingTime()) {
			gameManager.guestManager.increaseHumanPopularity (guest.getPopValue ());
			gameManager.increaseNumGold (SushiManager.sushiTypes[guest.target_sushi].getPrice());			guest.target_table.sushiPlate.gameObject.SetActive (false);
			guest.showMoodIcon (0);
			gameManager.playSFX (GameManager.coinSFX);
			PlayerDataManager.getPlayerData().sushiSold += 1;
			ToLeaving ();
		}
	}
}
