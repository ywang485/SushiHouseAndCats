using UnityEngine;
using System.Collections;

public class GuestGoTowardsTableState : GuestState {
	public GuestGoTowardsTableState(Guest subjGuest) : base(subjGuest) {
	}

	public override void UpdateState ()
	{
		Vector2 targetLocation = guest.target_table.transform.position;
		Vector2 currPos = guest.transform.position;
		guest.transform.position = Vector2.MoveTowards(currPos, targetLocation, guest.getSpeed() * Time.deltaTime);

	}

	public override void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag ("Table")) {
			ToEating ();
		}
	}
}
