using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	static public readonly Dictionary<string, CatType> catTypes = new Dictionary<string, CatType>() {
		{"generic-cat", new CatType("generic-cat", "It's just a cat.", "Animator/GenericCat")},
		{"monster-cat-1", new CatType("monster-cat-1", "It's a monster", "Animator/MonsterCat1", 30, 1f)},
		{"monster-cat-2", new CatType("monster-cat-2", "It's a monster", "Animator/MonsterCat2")},
		{"canvas-cat", new CatType("canvas-cat", "It's a monster", "Animator/CanvasCat")},
		{"book-cat", new CatType("book-cat", "It's a monster", "Animator/BookCat")},
		{"adv-book-cat", new CatType("adv-book-cat", "It's a monster", "Animator/AdvBookCat")}
	};
		
	private static int catPopValDecr = 2;

	public string catTypeId;
	private float speed = 1f;
	private int eatingPower = 20;
	private int eatingSpeed = 1;
	private int searchingTime = 20;
	public bool attacking = false;

	private HPSubject hpsubj;
	private Animator animator;

	[HideInInspector] public Bubble bubble;
	[HideInInspector] public Icon icon;
	[HideInInspector] public GameManager gameManager;

	[HideInInspector] public CatLookingForFoodState lookingForFoodState;
	[HideInInspector] public CatGoTowardsFoodState goTowardsFoodState;
	[HideInInspector] public CatEatingState eatingState;
	[HideInInspector] public CatLeavingState leavingState;
	[HideInInspector] public CatEscapingState escapingState;
	[HideInInspector] public CatPlayState playState;
	[HideInInspector] public CatAvoidingState avoidingState;
	[HideInInspector] public CatAttackingState attackingState;


	[HideInInspector] public Location currLoc;

	public bool facingRight = false;

	private GameObject targetSushiPlate;
	private bool isPet = false;
	public Vector2 targetPosition;
	public bool jumping = false;
	public bool towardsFood = false;
	public bool played = false;
	private bool hasDeath = false;

	public CatState currState;

	public float getSpeed() {
		return speed;
	}

	public float speedScale(float scale) {
		return speed * scale;
	}

	public int getEatingPower() {
		return eatingPower;
	}

	public int getEatingSpeed() {
		return eatingSpeed;
	}

	public int getSearchingTime() {
		return searchingTime;
	}

	public void showMoodIcon(int mood) {
		icon.setIcon (gameManager.moodIcons[mood]);
		bubble.show ();
	}

	public void showSushiIcon () {
		Debug.Log ("Show Sushi Icon Executed!");
		string sushi = getSushiWanted () [0];
		icon.setIcon (Resources.Load(SushiManager.sushiTypes[sushi].getSpritePath_s(), typeof(Sprite)) as Sprite);
		bubble.show ();
	}

	public void hideBubble() {
		if (icon != null) {
			icon.setIcon (null);
			bubble.hide ();
		}
	}

	public virtual List<string> getSushiWanted() {
		List<string> sushiWanted = new List<string> () {
			"california-roll",
			"salmon-nigiri",
			"salmon-roll",
			"tamago-nigiri",
			"tuna-nigiri",
			"white-tuna-nigiri"
		};

		return sushiWanted;
	}

	public bool isWantedSushi(string sushi) {
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
		avoidingState = new CatAvoidingState (this);
		attackingState = new CatAttackingState (this);

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
		
	private void generateCat() {
		if (PlayerDataManager.getPlayerData() != null) {
			string[] catList = new string[PlayerDataManager.getPlayerData().catTypes.Keys.Count];
			PlayerDataManager.getPlayerData().catTypes.Keys.CopyTo (catList, 0);
			this.catTypeId = catList[Random.Range (0, catList.Length)];
			this.speed = catTypes [catTypeId].getSpeed ();
			if (catTypes [catTypeId].getMaxHp() > 0) {
				hasDeath = true;
				hpsubj.maxHP = catTypes [catTypeId].getMaxHp ();
				hpsubj.HP = hpsubj.maxHP;
			}
			Debug.Log (catTypeId + " spawned");
		} else {
			string[] demoCats = new string[catTypes.Keys.Count];
			catTypes.Keys.CopyTo (demoCats,0);
			this.catTypeId = demoCats [Random.Range (0, demoCats.Length)];
			Debug.Log (catTypeId + " spawned!");
		}
	}

	public void init() {

		hpsubj = GetComponent<HPSubject> ();
		if (hpsubj != null) {
			hpsubj.HP = hpsubj.maxHP;
		}

		generateCat ();
		animator.runtimeAnimatorController = Resources.Load (Cat.catTypes [catTypeId].getAnimator(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

		currState = lookingForFoodState;
		lookingForFoodState.resetSearchingStartTime ();
		//if (PlayerDataManager.getPlayerData().catOrderEnabled) {
		//	showSushiIcon ();
		//}
		hideBubble ();
		toWalkFront ();
		lookingForFoodState.actualSpeed = speedScale (2.0f);


	}

	private void toWalkFront() {
		animator.SetInteger("State", 1);
	}

	private void toWalkBack() {
		animator.SetInteger("State", 2);
	}

	private void toJumpFront() {
		animator.SetInteger("State", 3);
	}

	private void toJumpBack() {
		animator.SetInteger("State", 4);
	}

	private void toStand() {
		animator.SetInteger ("State", 0);
	}

	private void updateAnim() {
		Vector2 drct = Utilities.getDirection (new Vector2 (transform.position.x, transform.position.y), targetPosition);
		float movex = drct.x;
		float movey = drct.y;
		if (movex == 0 && movey == 0) {
			toStand ();
		} else {
			// Moving right: right, back
			if (movex > 0) {
				if (!facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = true;
				}
				if (towardsFood) {
					toJumpBack ();
				} else if (jumping) {
					toJumpBack ();
				} else {
					toWalkBack ();
				}
				// Moving up: left, back
			} else if (movey > 0) {
				if (facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = false;
				}
				if (towardsFood) {
					toJumpBack ();
				} else if (jumping) {
					toJumpBack ();
				} else {
					toWalkBack ();
				}
				// Moving left: left, front
			} else if (movex < 0) {
				if (facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = false;
				}
				if (towardsFood) {
					toJumpFront ();
				} else if (jumping) {
					toJumpFront ();
				} else {
					toWalkFront ();
				}
				// Moving down: right, front
			} else if (movey < 0) {
				if (!facingRight) {
					transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
					facingRight = true;
				}
				if (towardsFood) {
					toJumpFront ();
				} else if (jumping) {
					toJumpFront ();
				} else {
					toWalkFront ();
				}
			}
		}

	}


	void Update() {
		currState.UpdateState ();
		if (hpsubj != null) {
			if (hpsubj.HP <= 0) {
				gameManager.messagingCenter.eventHappened (new CatDefeatedEvent (catTypeId));
				if (hasDeath) {
					gameManager.catManager.decreaseCatPopularity (Cat.catPopValDecr, catTypeId, 1);
					GameObject.Destroy (gameObject);
				} else {
					if (PlayerDataManager.getPlayerData().catMoodIconEnabled) {
						showMoodIcon (3);
					}
					currState.ToEscaping ();
				}
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
		if (other.tag == "Location") {
			if (currLoc.locType == other.GetComponent<Location> ().locType) {
				jumping = false;
			} else {
				jumping = true;
			}	
			currLoc = other.GetComponent<Location> ();
		}
		if (other.tag == "Shield") {
			Shield shield = other.GetComponent<Shield> ();
			if (shield.active) {
				int avoidingStartTime = gameManager.getCurrTimeInMinute ();
				avoidingState.avoidingEndTime = avoidingStartTime + shield.power;
				avoidingState.avoidingDirection = transform.position - other.transform.position;
				currState.ToAvoiding ();
			}
		}
		if (other.tag == "Treat") {
			if (!((Toy)other.gameObject.GetComponent<Toy> ()).finished) {
				showMoodIcon (2);
				setTargetSushiPlate (other.gameObject);
				currState.ToGoTowardsFood ();
			}
		}
		if (other.tag == "Weapon") {
			gameManager.playSFX (GameManager.grumpyMeowSFX);
			GameObject.Destroy (other.gameObject);
			if (!hasDeath) {
				gameManager.messagingCenter.eventHappened (new CatDefeatedEvent (catTypeId));
				if (PlayerDataManager.getPlayerData().catMoodIconEnabled) {
					showMoodIcon (3);
				}
				currState.ToEscaping ();
			} else {
				hpsubj.beAttacked (GameManager.getGameManager ().calculateDamage (0));
				if (attacking) {
					attackingState.target = GameObject.Find ("player");
					currState.ToAttacking ();
				}

				// knockback
				transform.Translate(transform.position - other.transform.position);
			}
		}
		currState.OnTriggerEnter2D (other);
	}

	public void pet() {
		if (!isPet) {
			if (PlayerDataManager.getPlayerData().catMoodIconEnabled) {
				showMoodIcon (2);
			}
			Debug.Log ("Cat being pet!");
			lookingForFoodState.resetSearchingStartTime ();
			gameManager.catManager.increaseCatPopularity (gameManager.calculatePettingPower(), catTypeId, 1);
			isPet = true;
		}
	}
		
}
