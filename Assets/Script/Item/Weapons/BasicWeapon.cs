using UnityEngine;
using System.Collections;

public class BasicWeapon : MonoBehaviour {

	public Vector2 targetLoc;
	public GameManager gameManager;
	public float speed;


	void Awake() {
		gameManager = GameManager.getGameManager ();
		speed = gameManager.weaponSpeed;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	protected void Update () {
		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		transform.position = Vector2.MoveTowards(currPos, targetLoc, speed * Time.deltaTime);
	}
}
