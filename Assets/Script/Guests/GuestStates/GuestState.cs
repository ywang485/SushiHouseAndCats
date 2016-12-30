using UnityEngine;
using System.Collections;

public class GuestState {

	protected readonly Guest guest;

	public GuestState (Guest subjGuest) {
		guest = subjGuest;
	}

	public virtual void UpdateState () {
	}

	public virtual void OnTriggerEnter2D(Collider2D other) {

	}

	public virtual void ToGoTowardsCounter() {
		guest.currState = guest.goTowardsCounterState;
	}

	public virtual void ToGoTowardsTable() {
		guest.currState = guest.goTowardsTableState;
	}

	public virtual void ToEating() {
		guest.animator.SetInteger ("State", 0);
		guest.currState = guest.eatingState;
		guest.eatingState.resetEatingStartTime ();
		guest.target_table.sushiPlate.gameObject.SetActive (true);
		HPSubject hp = guest.target_table.sushiPlate.GetComponent<HPSubject> ();
		hp.resetHP ();
		HPBar hpbarObj = guest.target_table.sushiPlate.GetComponentInChildren<HPBar> ();
		if(hpbarObj != null) {
			GameObject hpbar = hpbarObj.gameObject;
			hpbar.SetActive (false);
		}
		guest.target_table.sushiPlate.setSushiType (guest.target_sushi);
	}

	public virtual void ToLeaving() {
		if (guest.target_table != null) {
			guest.target_table.availability = true;
		}
		guest.animator.SetInteger ("State", 5);
		guest.currState = guest.leavingState;
	}

	public virtual void ToWait () {
		guest.currState = guest.waitState;
		guest.waitState.resetWaitingStartTime ();
		guest.animator.SetInteger ("State", 0);
		guest.target_sushi = guest.generateOrder ();
		guest.showSushiIcon (guest.target_sushi);
	}

}
