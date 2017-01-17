using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cat : MonoBehaviour {

	private static int defActPopThUp = 999;
	private static int defActPopThDown = -999;
	private static float speed = 1f;
	private static int catPopValDecr = 2;
	private static int eatingPower = 20;
	private static int eatingSpeed = 1;
	private static int searchingTime = 20;

	public HPSubject hpsubj;

	[HideInInspector] public Bubble bubble;
	[HideInInspector] public Icon icon;
	[HideInInspector] public GameManager gameManager;
	[HideInInspector] public Animator animator;

	[HideInInspector] public CatLookingForFoodState lookingForFoodState;
	[HideInInspector] public CatGoTowardsFoodState goTowardsFoodState;
	[HideInInspector] public CatEatingState eatingState;
	[HideInInspector] public CatLeavingState leavingState;
	[HideInInspector] public CatEscapingState escapingState;
	[HideInInspector] public CatPlayState playState;

	public bool facingRight = false;

	private GameObject targetSushiPlate;
	private bool isPet;
	public Vector2 targetPosition;
	public bool towardsFloor = false;
	public bool towardsFood = false;
	public bool played = false;

	public CatState currState;

	public virtual int getActPopThUp() {
		return defActPopThUp;
	}

	public virtual int getActPopThDown() {
		return defActPopThDown;
	}

	public float speedScale(float scale) {
		return speed * scale;
	}

	public virtual int getSearchingTime() {
		return Cat.searchingTime;
	}
		

	public virtual float getSpeed() {
		return Cat.speed;
	}

	public virtual int getCatPopValDecr() {
		return Cat.catPopValDecr;
	}

	public virtual int getEatingPower() {
		return Cat.eatingPower;
	}

	public virtual int getEatingSpeed() {
		return Cat.eatingSpeed;
	}

	public void showMoodIcon(int mood) {
		icon.setIcon (gameManager.moodIcons[mood]);
		bubble.show ();
	}
		
	public void showSushiIcon () {
		Debug.Log ("Show Sushi Icon Executed!");
		SushiManager.Sushi sushi = getSushiWanted () [0];
		icon.setIcon (gameManager.sushiManager.sushiSprite[SushiManager.sushi2number(sushi)]);
		bubble.show ();
	}

	public void hideBubble() {
		if (icon != null) {
			icon.setIcon (null);
			bubble.hide ();
		}
	}

	public virtual List<SushiManager.Sushi> getSushiWanted() {
		List<SushiManager.Sushi> sushiWanted = new List<SushiManager.Sushi>();

		sushiWanted.Add (SushiManager.Sushi.CaliforniaRoll);
		sushiWanted.Add (SushiManager.Sushi.SalmonNigiri);
		sushiWanted.Add (SushiManager.Sushi.SalmonRoll);
		sushiWanted.Add (SushiManager.Sushi.TamagoNigiri);
		sushiWanted.Add (SushiManager.Sushi.TunaNigiri);
		sushiWanted.Add (SushiManager.Sushi.WhiteTunaNigiri);

		return sushiWanted;
	}

	public bool isWantedSushi(SushiManager.Sushi sushi) {
		return getSushiWanted ().Contains (sushi);
	}

	public void setTargetSushiPlate(GameObject target) {
		targetSushiPlate = target;
	}

	public GameObject getTargetSushiPlate() {
		return targetSushiPlate;
	}

	// Initialization
	public void Awake() {
		gameManager = GameManager.getGameManager ();

		lookingForFoodState = new CatLookingForFoodState (this);
		goTowardsFoodState = new CatGoTowardsFoodState (this);
		eatingState = new CatEatingState (this);
		leavingState = new CatLeavingState (this);
		escapingState = new CatEscapingState (this);
		playState = new CatPlayState (this);

		bubble = gameObject.GetComponentInChildren<Bubble> ();
		icon = gameObject.GetComponentInChildren<Icon> ();

		animator = GetComponent<Animator> ();

		init ();

	}

	public int getStatusCode() {
		if (currState == lookingForFoodState) {
			return 1;
		} else if (currState == goTowardsFoodState) {
			return 1;
		} else if (currState == leavingState) {
			return 2;
		} else if (currState == eatingState) {
			return 3;
		} else if (currState == escapingState) {
			return 4;
		} else {
			return 0;
		}
	}

	public void init() {
		currState = lookingForFoodState;
		lookingForFoodState.resetSearchingStartTime ();
		if (PlayerDataManager.playerData.catOrderEnabled) {
			showSushiIcon ();
		}
		hpsubj = GetComponent<HPSubject> ();
		if (hpsubj != null) {
			hpsubj.HP = hpsubj.maxHP;
		}
		hideBubble ();
		animator.SetInteger("animNo", 4);
		lookingForFoodState.actualSpeed = speedScale (2.0f);

	}

	public void updateAnim() {
		Vector2 drct = Utilities.getDirection (new Vector2 (transform.position.x, transform.position.y), targetPosition);
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


	void Update() {
		currState.UpdateState ();
		if (hpsubj != null) {
			if (hpsubj.HP <= 0) {
				gameObject.SetActive(false);
			}
		}
		updateAnim();
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			currState.OnLeftClick ();
		}
			
		if (Input.GetMouseButtonDown (1)) {
			currState.OnRightClick ();
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Weapon") {
			GameObject.Destroy(other.gameObject);
			if (hpsubj == null) {
				if (PlayerDataManager.playerData.catMoodIconEnabled) {
					showMoodIcon (3);
				}
				currState.ToEscaping ();
			} else {
				hpsubj.beAttacked (GameManager.getGameManager ().calculateDamage (0));
			}
		}
		currState.OnTriggerEnter2D (other);
	}

	void OnEnable(){
		isPet = false;
	}

	public void pet() {
		if (!isPet) {
			if (PlayerDataManager.playerData.catMoodIconEnabled) {
				showMoodIcon (2);
			}
			Debug.Log ("Cat being pet!");
			lookingForFoodState.resetSearchingStartTime ();
			gameManager.catManager.increaseCatPopularity (gameManager.calculatePettingPower());
			isPet = true;
		}
	}

}