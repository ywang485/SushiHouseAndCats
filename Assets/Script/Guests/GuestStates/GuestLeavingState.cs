using UnityEngine;
using System.Collections;

public class GuestLeavingState : GuestState {
	
	public GuestLeavingState(Guest subjGuest) : base(subjGuest) {
	}

	public override void UpdateState ()
	{
		Vector2 targetLocation = guest.gameManager.mapManager.getDoorLocation();
		Vector2 currPos = guest.transform.position;
		guest.transform.position = Vector2.MoveTowards(currPos, targetLocation, guest.getSpeed() * Time.deltaTime);

	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("DoorLoc")) {
			guest.gameObject.SetActive (false);
		}
	}

}
