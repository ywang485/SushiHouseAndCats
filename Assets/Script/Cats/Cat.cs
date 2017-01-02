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

	public bool facingRight = false;

	private GameObject targetSushiPlate;
	private bool isPet;

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

	void Update() {
		currState.UpdateState ();
		if (hpsubj != null) {
			if (hpsubj.HP <= 0) {
				gameObject.SetActive(false);
			}
		}

		Debug.Log ("Cat facing right: ");
		Debug.Log (facingRight);
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