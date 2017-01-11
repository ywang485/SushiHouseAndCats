using UnityEngine;
using System.Collections;

public class GuestWaitState : GuestState {
	
	private int waitingStartTime;
	private GameManager gameManager;

	public GuestWaitState(Guest subjGuest) : base(subjGuest) {
		gameManager = GameManager.getGameManager ();
	}

	public void resetWaitingStartTime() {
		waitingStartTime = guest.gameManager.getCurrTimeInMinute();
	}

	public override void UpdateState ()
	{

		if (guest.gameManager.getCurrTimeInMinute () - waitingStartTime > guest.getWaitTime()) {
			guest.showMoodIcon (1);
			gameManager.guestManager.humanPopularity -= guest.getPopValueDec () / 2;
			gameManager.playHurtSFX ();
			ToLeaving ();
		}

		guest.target_table = guest.gameManager.mapManager.findAvailableTable();

		if (guest.target_table == null) {
			return;
		} else {
			Sushi target_sushi_on_counter = guest.gameManager.mapManager.findSpecificSushiOnCounter(guest.target_sushi, true);
			if (target_sushi_on_counter != null) {
				target_sushi_on_counter.gameObject.SetActive (false);
				guest.hideBubble ();
				ToGoTowardsTable ();
				guest.animator.SetInteger ("State", 5);
				guest.target_table.availability = false;
			}
		}
	}
}
