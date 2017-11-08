using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatObsolete : MonoBehaviour {

	private static int defActPopThUp = 999;
	private static int defActPopThDown = -999;
	private float speed = 1f;
	private static int catPopValDecr = 2;
	private static int eatingPower = 20;
	private static int eatingSpeed = 1;
	private int searchingTime = 20;
	public bool attacking = false;

	// Appearance
	private Color body_color;
	private Color eye_color;
	private Color mark_color;
	private GameObject head;
	private GameObject body;
	private GameObject eyes;
	private GameObject head_mark;
	private GameObject back_mark;
	private GameObject tail_mark;
	private GameObject belly_mark;

	private HPSubject hpsubj;

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

	private bool hasDeath;

	public CatState currState;

	public float getSpeed() {
		return speed;
	}

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
		return searchingTime;
	}

	public virtual int getCatPopValDecr() {
		return CatObsolete.catPopValDecr;
	}

	public virtual int getEatingPower() {
		return CatObsolete.eatingPower;
	}

	public virtual int getEatingSpeed() {
		return CatObsolete.eatingSpeed;
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

		/*lookingForFoodState = new CatLookingForFoodState (this);
		goTowardsFoodState = new CatGoTowardsFoodState (this);
		eatingState = new CatEatingState (this);
		leavingState = new CatLeavingState (this);
		escapingState = new CatEscapingState (this);
		playState = new CatPlayState (this);
		avoidingState = new CatAvoidingState (this);
		attackingState = new CatAttackingState (this);*/

		bubble = gameObject.GetComponentInChildren<Bubble> ();
		icon = gameObject.GetComponentInChildren<Icon> ();

		head = transform.Find("head").gameObject;
		body = transform.Find("body").gameObject;
		eyes = transform.Find("eyes").gameObject;
		head_mark = transform.Find("head-mark").gameObject;
		back_mark = transform.Find("back-mark").gameObject;
		tail_mark = transform.Find("tail-mark").gameObject;
		belly_mark = transform.Find ("belly-mark").gameObject;

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

		int r;
		int r2;
		float rf;
		CatManager catManager = gameManager.catManager;

		// stats
		// speed
		speed = Random.Range(LevelManager.getBaseSpeed() -0.3f, LevelManager.getBaseSpeed() +0.3f);
		//transform.localScale = new Vector3 (6f - speed*2, 6f - speed*2, 1f);

		// Searching Time
		searchingTime = Random.Range(LevelManager.getBaseSearchingTime() - 10, LevelManager.getBaseSearchingTime() + 10);

		// eating power
		eatingPower = Random.Range(eatingPower - 5, eatingPower + 15);
		if (eatingPower < 1) {
			eatingPower = 1;
		}

		// Use of HP Bar
		int baseHP = LevelManager.getBaseHP();
		if (baseHP == 0) {
			hpsubj.enabled = false;
		} else {
			hpsubj.enabled = true;

			// Max HP
			hpsubj.maxHP = Random.Range(baseHP - 10, baseHP + 10);
			if (hpsubj.maxHP <= 0) {
				hpsubj.maxHP = 5;
			}
			hpsubj.HP = hpsubj.maxHP;

			hasDeath = LevelManager.getHasDeath ();

			// Attacking Behaviour
			int prob = LevelManager.getAttackingProbability();
			r = Random.Range (0, 100);
			if (r <= prob) {
				attacking = true;
			}

		}

		// Appearance
		Animator animator;

		// Color
		List<Color> body_colors = LevelManager.getBodyColors();
		body_color = body_colors[Random.Range(0, body_colors.Count)];
		eye_color = catManager.eye_colors[Random.Range(0, catManager.eye_colors.Length)];
		mark_color = body_color;

		// Body
		List<CatComponent> bodies = LevelManager.getCatBodies();
		r = Random.Range (0, bodies.Count);
		body.GetComponent<Animator> ().runtimeAnimatorController = Resources.Load(bodies[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
		body.GetComponent<SpriteRenderer> ().color = body_color;

		// Eyes
		List<CatComponent> eye_list = LevelManager.getCatEyes();
		r = Random.Range (0, eye_list.Count);
		eyes.GetComponent<Animator> ().runtimeAnimatorController = Resources.Load(eye_list[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
		eyes.GetComponent<SpriteRenderer> ().color = eye_color;

		// Head
		List<CatComponent> heads = LevelManager.getCatHeads();
		r = Random.Range (0, heads.Count);
		head.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(heads[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
		head.GetComponent<SpriteRenderer> ().color = body_color;

		// Back Mark
		r = Random.Range (0, 2);
		if (r == 0) {
			List<CatComponent> back_marks = LevelManager.getCatBackMarks();
			r2 = Random.Range (0, back_marks.Count);
			back_mark.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(back_marks[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
			back_mark.GetComponent<SpriteRenderer> ().color = mark_color;
		} else {
			back_mark.SetActive (false);
		}

		// Belly Mark
		r = Random.Range (0, 2);
		if (r == 0) {
			List<CatComponent> belly_marks = LevelManager.getCatBellyMarks();
			r2 = Random.Range (0, belly_marks.Count);
			belly_mark.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(belly_marks[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
			belly_mark.GetComponent<SpriteRenderer> ().color = mark_color;
		} else {
			belly_mark.SetActive (false);
		}

		// Head Mark
		r = Random.Range (0, 2);
		if (r == 0) {
			List<CatComponent> head_marks = LevelManager.getCatHeadMarks();
			r2 = Random.Range (0, head_marks.Count);
			head_mark.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(head_marks[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
			head_mark.GetComponent<SpriteRenderer> ().color = mark_color;
		} else {
			head_mark.SetActive (false);
		}

		// Tail Mark
		r = Random.Range (0, 2);
		if (r == 0) {
			List<CatComponent> tail_marks = LevelManager.getCatTailMarks();
			r2 = Random.Range (0, tail_marks.Count);
			tail_mark.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(tail_marks[r].getResourcePath(), typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
			tail_mark.GetComponent<SpriteRenderer> ().color = mark_color;
		} else {
			tail_mark.SetActive (false);
		}

		// Attacking Behaviour
		//if (attacking) {
		//	gameObject.AddComponent<AggressiveCat>();
		//	this.enabled = false;
		//}

	}

	public void init() {

		hpsubj = GetComponent<HPSubject> ();
		if (hpsubj != null) {
			hpsubj.HP = hpsubj.maxHP;
		}

		generateCat ();

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
		head.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		body.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		head.GetComponent<Animator>().SetBool("Front", true);
		body.GetComponent<Animator>().SetInteger("State", 0);
		eyes.GetComponent<SpriteRenderer> ().enabled = true;
		head_mark.GetComponent<SpriteRenderer> ().enabled = true;
		back_mark.GetComponent<Animator>().SetBool("Front", true);
		tail_mark.GetComponent<Animator>().SetInteger("State", 0);
		belly_mark.GetComponent<Animator>().SetBool("Front", true);
	}

	private void toWalkBack() {
		head.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		body.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		head.GetComponent<Animator>().SetBool("Front", false);
		body.GetComponent<Animator>().SetInteger("State", 1);
		eyes.GetComponent<SpriteRenderer> ().enabled = false;
		head_mark.GetComponent<SpriteRenderer> ().enabled = false;
		back_mark.GetComponent<Animator>().SetBool("Front", false);
		tail_mark.GetComponent<Animator>().SetInteger("State", 2);
		belly_mark.GetComponent<Animator>().SetBool("Front", false);
	}

	private void toJumpFront() {
		head.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		body.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		head.GetComponent<Animator>().SetBool("Front", true);
		body.GetComponent<Animator>().SetInteger("State", 3);
		eyes.GetComponent<SpriteRenderer> ().enabled = true;
		head_mark.GetComponent<SpriteRenderer> ().enabled = true;
		back_mark.GetComponent<Animator>().SetBool("Front", true);
		tail_mark.GetComponent<Animator>().SetInteger("State", 1);
		belly_mark.GetComponent<Animator>().SetBool("Front", true);
	}

	private void toJumpBack() {
		head.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		body.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		head.GetComponent<Animator>().SetBool("Front", false);
		body.GetComponent<Animator>().SetInteger("State", 2);
		eyes.GetComponent<SpriteRenderer> ().enabled = false;
		head_mark.GetComponent<SpriteRenderer> ().enabled = false;
		back_mark.GetComponent<Animator>().SetBool("Front", false);
		tail_mark.GetComponent<Animator>().SetInteger("State", 2);
		belly_mark.GetComponent<Animator>().SetBool("Front", false);
	}

	private void updateAnim() {
		Vector2 drct = Utilities.getDirection (new Vector2 (transform.position.x, transform.position.y), targetPosition);
		float movex = drct.x;
		float movey = drct.y;
		if (movex == 0 && movey == 0) {
			//animator.SetInteger ("animNo", 3);
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
				if (hasDeath) {
					gameManager.catManager.decreaseCatPopularity (CatObsolete.catPopValDecr, "", 0);
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
		if (other.tag == "Weapon") {
			gameManager.playSFX (GameManager.grumpyMeowSFX);
			GameObject.Destroy (other.gameObject);
			if (hpsubj == null || hpsubj.enabled == false) {
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
			gameManager.catManager.increaseCatPopularity (gameManager.calculatePettingPower(), "", 0);
			isPet = true;
		}
	}

}