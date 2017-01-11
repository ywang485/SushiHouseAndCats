using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float speed = 0.1f;

	private Animator animator;

	private Rigidbody2D rb;
	private bool towardsRight = true;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
	
		gameManager = GameManager.getGameManager ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0f;

		float movex = Input.GetAxis ("Horizontal");
		float movey = Input.GetAxis ("Vertical");
		rb.velocity = Utilities.coordinateReverseTransform(new Vector2 (movex * speed * (gameManager.movingSpeed + 1), movey * speed * (gameManager.movingSpeed + 1) ));
		//rb.velocity = new Vector2 (movex * speed, movey * speed);
		transform.localRotation = Quaternion.identity;

		// Test
		// Debug.Log(new Vector2(movex, movey));
		// Debug.Log(rb.velocity);

		if (movex == 0 && movey == 0) {
			animator.SetBool ("still", true);
		} else {
			animator.SetBool ("still", false);
			// Moving right: right, back
			if (movex > 0) {
				if (!towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = true;
				}
				animator.SetInteger ("direction", 1);
			// Moving up: left, back
			} else if (movey > 0) {
				if (towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = false;
				}
				animator.SetInteger ("direction", 1);
			// Moving left: left, front
			} else if (movex < 0) {
				if (towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = false;
				}
				animator.SetInteger ("direction", 0);
			// Moving down: right, front
			} else if (movey < 0) {
				if (!towardsRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					towardsRight = true;
				}
				animator.SetInteger ("direction", 0);
			}
		}

	}
}
