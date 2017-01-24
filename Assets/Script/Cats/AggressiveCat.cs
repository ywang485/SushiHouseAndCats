using UnityEngine;
using System.Collections;

public class AggressiveCat : MonoBehaviour {

	public GameObject target;
	public float speed = 1f;
	public float minDist = 0.5f;
	public bool attacking = false;
	public GameManager gameManager;
	public float attackingBufferTime = 1f;

	public HPSubject hpsubj;
	private Animator animator;
	private bool facingRight = false;
	public bool towardsFood = false;
	public bool towardsFloor = false;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.getGameManager ();
		hpsubj = GetComponent<HPSubject> ();
		animator = GetComponent<Animator> ();
	}

	void searchForTarget() {
		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		target = player;
		GameObject[] guests = GameObject.FindGameObjectsWithTag ("Guest");
		float minDist = Vector3.Distance (currPos, player.transform.position);
		foreach (GameObject guest in guests) {
			if (Vector3.Distance (currPos, guest.transform.position) < minDist) {
				target = guest;
				minDist = Vector3.Distance (currPos, guest.transform.position);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (attacking) {
			return;
		}

		if (target == null) {
			// Search for a target
			searchForTarget();
		}

		updateAnim ();

		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		if (Vector3.Distance (target.transform.position, currPos) >= minDist) {
			// Go towards target
			transform.position = Vector2.MoveTowards (currPos, target.transform.position, speed * Time.deltaTime);
		} else {
			// Attack
			StartCoroutine (attack ());
			attacking = true;
		}

		// Check Death
		if (hpsubj.HP <= 0) {
			GameObject.Destroy (gameObject);
		}
	}

	IEnumerator attack() {
		animator.SetInteger ("animNo", 0);
		target.GetComponent<HPSubject> ().beAttacked (10);
		yield return new WaitForSeconds (attackingBufferTime);
		attacking = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Weapon") {
			if (hpsubj == null) {
				GameObject.Destroy (gameObject);
			} else {
				hpsubj.beAttacked (GameManager.getGameManager ().calculateDamage (0));
			}
		}
	}

	public void updateAnim() {
		Vector2 drct = Utilities.getDirection (new Vector2 (transform.position.x, transform.position.y), target.transform.position);
		float movex = drct.x;
		float movey = drct.y;
		if (movex == 0 && movey == 0) {
			animator.SetInteger ("animNo", 3);
		} else {
			// Moving right: right, back
			if (movex > 0) {
				if (!facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = true;
				}
				if (towardsFood) {
					animator.SetInteger ("animNo", 5);
				} else if (towardsFloor) {
					animator.SetInteger ("animNo", 5);
				} else {
					animator.SetInteger ("animNo", 2);
				}
				// Moving up: left, back
			} else if (movey > 0) {
				if (facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = false;
				}
				if (towardsFood) {
					animator.SetInteger ("animNo", 5);
				} else if (towardsFloor) {
					animator.SetInteger ("animNo", 5);
				} else {
					animator.SetInteger ("animNo", 2);
				}
				// Moving left: left, front
			} else if (movex < 0) {
				if (facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = false;
				}
				if (towardsFood) {
					animator.SetInteger ("animNo", 4);
				} else if (towardsFloor) {
					animator.SetInteger ("animNo", 4);
				} else {
					animator.SetInteger ("animNo", 1);
				}
				// Moving down: right, front
			} else if (movey < 0) {
				if (!facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = true;
				}
				if (towardsFood) {
					animator.SetInteger ("animNo", 4);
				} else if (towardsFloor) {
					animator.SetInteger ("animNo", 4);
				} else {
					animator.SetInteger ("animNo", 1);
				}
			}
		}

	}


}

